<!-- Game.svelte -->
<script>
	import { onMount, onDestroy } from 'svelte';
	import * as signalR from '@microsoft/signalr';
	import { browser } from '$app/environment';

	// Estado do jogo
	let connection;
	let player = { username: '', health: 100, isDefending: false, isEvading: false };
	let enemies = [];
	let messages = [];
	let gameStarted = false;
	let playerDead = false;

	// Conectar ao hub do SignalR
	async function connectToHub() {
		connection = new signalR.HubConnectionBuilder()
			.withUrl('/gameHub', {
				transport: signalR.HttpTransportType.WebSockets,
				withCredentials: true
			})
			.withAutomaticReconnect()
			.build();

		// Manipuladores de eventos para atualizar o estado do jogo
		setupEventHandlers();

		try {
			await connection.start();
			console.log('Conectado ao SignalR Hub!');
			gameStarted = true;
			addMessage('Conectado ao jogo!');
		} catch (err) {
			console.error('Erro ao conectar:', err);
			addMessage('Falha na conexão com o servidor!', 'error');
			setTimeout(connectToHub, 5000);
		}
	}

	// Configurar manipuladores de eventos do SignalR
	function setupEventHandlers() {
		connection.on('PlayerJoined', (p) => {
			player = p;
			addMessage(`Bem-vindo, ${p.username}!`);
		});

		connection.on('UpdateEnemies', (e) => {
			enemies = e;
			addMessage('Inimigos atualizados!');
		});

		connection.on('NewEnemy', (enemy) => {
			enemies = [...enemies, enemy];
			addMessage(`Um novo inimigo apareceu: ${enemy.name}!`);
		});

		connection.on('UpdateEnemy', (enemy) => {
			const index = enemies.findIndex((e) => e.id === enemy.id);
			if (index !== -1) {
				enemies[index] = enemy;
				enemies = [...enemies];
			}
		});

		connection.on('EnemyDefeated', (enemyId) => {
			const enemy = enemies.find((e) => e.id === enemyId);
			if (enemy) {
				addMessage(`${enemy.name} foi derrotado!`);
				enemies = enemies.filter((e) => e.id !== enemyId);
			}
		});

		connection.on('EnemyAttacking', (enemyId, targetPlayerId) => {
			const enemy = enemies.find((e) => e.id === enemyId);
			if (enemy) {
				enemy.isAttacking = true;
				enemies = [...enemies];

				if (targetPlayerId === connection.connectionId) {
					addMessage(
						`${enemy.name} está te atacando! Defenda (botão direito) ou esquive (Shift)!`,
						'warning'
					);
				} else {
					addMessage(`${enemy.name} está atacando outro jogador!`);
				}
			}
		});

		connection.on('EnemyStoppedAttacking', (enemyId) => {
			const enemy = enemies.find((e) => e.id === enemyId);
			if (enemy) {
				enemy.isAttacking = false;
				enemies = [...enemies];
			}
		});

		connection.on('PlayerDefending', (playerId) => {
			if (playerId === connection.connectionId) {
				player.isDefending = true;
				addMessage('Você está defendendo!');
			}
		});

		connection.on('PlayerEvading', (playerId) => {
			if (playerId === connection.connectionId) {
				player.isEvading = true;
				addMessage('Você está esquivando!');
			}
		});

		connection.on('PlayerStoppedDefending', (playerId) => {
			if (playerId === connection.connectionId) {
				player.isDefending = false;
			}
		});

		connection.on('PlayerStoppedEvading', (playerId) => {
			if (playerId === connection.connectionId) {
				player.isEvading = false;
			}
		});

		connection.on('PlayerTookDamage', (playerId, damage) => {
			if (playerId === connection.connectionId) {
				player.health -= damage;
				addMessage(`Você recebeu ${damage} de dano!`, 'error');
			}
		});

		connection.on('PlayerTookReducedDamage', (playerId, damage) => {
			if (playerId === connection.connectionId) {
				player.health -= damage;
				addMessage(`Você bloqueou parte do dano! Recebeu apenas ${damage}!`, 'warning');
			}
		});

		connection.on('PlayerEvadedAttack', (playerId) => {
			if (playerId === connection.connectionId) {
				addMessage('Você esquivou do ataque!', 'success');
			}
		});

		connection.on('UpdatePlayer', (updatedPlayer) => {
			if (updatedPlayer.connectionId === connection.connectionId) {
				player = { ...player, health: updatedPlayer.health };
			}
		});

		connection.on('YouDied', () => {
			playerDead = true;
			addMessage('Você morreu! Revivendo...', 'error');
			setTimeout(() => {
				playerDead = false;
				addMessage('Você reviveu!', 'success');
			}, 3000);
		});
	}

	// Adicionar mensagem ao log
	function addMessage(text, type = 'info') {
		messages = [...messages, { text, type, timestamp: new Date() }];
		if (messages.length > 10) {
			messages = messages.slice(messages.length - 10);
		}
	}

	// Atacar inimigo (clique esquerdo)
	async function attackEnemy(enemy) {
		if (!connection || playerDead) return;

		try {
			await connection.invoke('AttackEnemy', enemy.id);
			addMessage(`Você atacou ${enemy.name}!`);
		} catch (err) {
			console.error('Erro ao atacar:', err);
			addMessage('Falha ao atacar!', 'error');
		}
	}

	// Defender (clique direito)
	async function defend() {
		if (!connection || playerDead) return;

		try {
			await connection.invoke('Defend');
		} catch (err) {
			console.error('Erro ao defender:', err);
			addMessage('Falha ao defender!', 'error');
		}
	}

	// Esquivar (pressionar shift)
	async function evade() {
		if (!connection || playerDead) return;

		try {
			await connection.invoke('Evade');
		} catch (err) {
			console.error('Erro ao esquivar:', err);
			addMessage('Falha ao esquivar!', 'error');
		}
	}

	// Manipuladores de eventos para teclado
	function handleKeyDown(e) {
		if (e.key === 'Shift') {
			evade();
		}
	}

	// Manipulador para clique do mouse
	function handleClick(e, enemy) {
		if (e.button === 0) {
			// Botão esquerdo
			attackEnemy(enemy);
		} else if (e.button === 2) {
			// Botão direito
			defend();
		}
	}

	// Impedir menu contextual no clique direito
	function preventDefault(e) {
		e.preventDefault();
	}

	onMount(() => {
		connectToHub();

		if (browser) {
			document.addEventListener('keydown', handleKeyDown);
			document.addEventListener('contextmenu', preventDefault);
		}
	});

	onDestroy(() => {
		if (connection) {
			connection.stop();
		}

		if (browser) {
			document.removeEventListener('keydown', handleKeyDown);
			document.removeEventListener('contextmenu', preventDefault);
		}
	});
</script>

<svelte:head>
	<title>Jogo Clicker Multiplayer</title>
</svelte:head>

<div class="game-container">
	{#if !gameStarted}
		<div class="loading">
			<h1>Conectando ao servidor...</h1>
		</div>
	{:else}
		<div class="hud">
			<div class="player-info">
				<h2>{player.username}</h2>
				<div class="health-bar">
					<div class="health-fill" style="width: {player.health}%"></div>
					<span class="health-text">{player.health} / 100</span>
				</div>
				<div class="status">
					{#if player.isDefending}
						<span class="defending">Defendendo</span>
					{/if}
					{#if player.isEvading}
						<span class="evading">Esquivando</span>
					{/if}
				</div>
			</div>
		</div>

		<div class="battlefield" class:player-dead={playerDead}>
			{#if playerDead}
				<div class="death-overlay">
					<h1>VOCÊ MORREU</h1>
					<p>Revivendo...</p>
				</div>
			{/if}

			<div class="enemies-container">
				{#each enemies as enemy (enemy.id)}
					<div
						class="enemy"
						class:attacking={enemy.isAttacking}
						on:mousedown={(e) => handleClick(e, enemy)}
					>
						<h3>{enemy.name}</h3>
						<div class="enemy-health-bar">
							<div
								class="enemy-health-fill"
								style="width: {(enemy.health / enemy.maxHealth) * 100}%"
							></div>
							<span class="enemy-health-text">{enemy.health} / {enemy.maxHealth}</span>
						</div>
					</div>
				{/each}
			</div>
		</div>

		<div class="message-log">
			<h3>Mensagens</h3>
			<ul>
				{#each messages as message}
					<li class="message {message.type}">
						<span class="timestamp">{message.timestamp.toLocaleTimeString()}</span>
						<span class="message-text">{message.text}</span>
					</li>
				{/each}
			</ul>
		</div>

		<div class="controls-help">
			<p><strong>Controles:</strong></p>
			<ul>
				<li>Clique Esquerdo: Atacar inimigo</li>
				<li>Clique Direito: Defender (durante ataque do inimigo)</li>
				<li>Shift: Esquivar (durante ataque do inimigo)</li>
			</ul>
		</div>
	{/if}
</div>

<style>
	.game-container {
		display: flex;
		flex-direction: column;
		height: 100vh;
		background-color: #222;
		color: white;
		font-family: 'Arial', sans-serif;
	}

	.loading {
		display: flex;
		justify-content: center;
		align-items: center;
		height: 100%;
	}

	.hud {
		padding: 10px;
		background-color: #333;
		border-bottom: 2px solid #444;
	}

	.player-info {
		display: flex;
		flex-direction: column;
	}

	.health-bar {
		width: 100%;
		height: 20px;
		background-color: #555;
		border-radius: 5px;
		overflow: hidden;
		position: relative;
	}

	.health-fill {
		height: 100%;
		background-color: #2ecc71;
		transition: width 0.3s ease;
	}

	.health-text {
		position: absolute;
		top: 0;
		left: 0;
		width: 100%;
		height: 100%;
		display: flex;
		align-items: center;
		justify-content: center;
		color: white;
		text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.5);
	}

	.status {
		margin-top: 5px;
		display: flex;
		gap: 10px;
	}

	.defending {
		background-color: #3498db;
		padding: 2px 8px;
		border-radius: 3px;
	}

	.evading {
		background-color: #9b59b6;
		padding: 2px 8px;
		border-radius: 3px;
	}

	.battlefield {
		flex-grow: 1;
		display: flex;
		justify-content: center;
		align-items: center;
		position: relative;
	}

	.player-dead {
		filter: grayscale(100%) brightness(50%);
	}

	.death-overlay {
		position: absolute;
		top: 0;
		left: 0;
		width: 100%;
		height: 100%;
		display: flex;
		flex-direction: column;
		justify-content: center;
		align-items: center;
		background-color: rgba(0, 0, 0, 0.7);
		z-index: 10;
		color: #e74c3c;
	}

	.enemies-container {
		display: flex;
		flex-wrap: wrap;
		gap: 20px;
		justify-content: center;
		padding: 20px;
	}

	.enemy {
		width: 200px;
		height: 150px;
		background-color: #444;
		border-radius: 5px;
		padding: 10px;
		display: flex;
		flex-direction: column;
		align-items: center;
		justify-content: center;
		cursor: pointer;
		transition:
			transform 0.2s ease,
			box-shadow 0.2s ease;
		user-select: none;
	}

	.enemy:hover {
		transform: scale(1.05);
		box-shadow: 0 0 10px rgba(255, 255, 255, 0.3);
	}

	.enemy:active {
		transform: scale(0.95);
	}

	.enemy.attacking {
		animation: attacking 0.5s infinite alternate;
		background-color: #c0392b;
	}

	@keyframes attacking {
		from {
			transform: scale(1);
		}
		to {
			transform: scale(1.1);
		}
	}

	.enemy-health-bar {
		width: 100%;
		height: 15px;
		background-color: #555;
		border-radius: 3px;
		overflow: hidden;
		position: relative;
		margin-top: 10px;
	}

	.enemy-health-fill {
		height: 100%;
		background-color: #e74c3c;
		transition: width 0.3s ease;
	}

	.enemy-health-text {
		position: absolute;
		top: 0;
		left: 0;
		width: 100%;
		height: 100%;
		display: flex;
		align-items: center;
		justify-content: center;
		color: white;
		font-size: 12px;
		text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.5);
	}

	.message-log {
		height: 150px;
		background-color: #333;
		border-top: 2px solid #444;
		padding: 10px;
		overflow-y: auto;
	}

	.message-log h3 {
		margin-top: 0;
		margin-bottom: 5px;
	}

	.message-log ul {
		list-style-type: none;
		padding: 0;
		margin: 0;
	}

	.message {
		padding: 3px 0;
		border-bottom: 1px solid #444;
		font-size: 14px;
	}

	.timestamp {
		color: #aaa;
		margin-right: 5px;
	}

	.info {
		color: #3498db;
	}

	.warning {
		color: #f39c12;
	}

	.error {
		color: #e74c3c;
	}

	.success {
		color: #2ecc71;
	}

	.controls-help {
		background-color: #333;
		border-top: 1px solid #444;
		padding: 10px;
	}

	.controls-help ul {
		list-style-type: none;
		padding-left: 10px;
	}

	.controls-help li {
		margin-bottom: 5px;
	}
</style>
