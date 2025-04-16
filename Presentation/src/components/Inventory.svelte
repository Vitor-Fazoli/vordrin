<script>
	import { onMount } from 'svelte';
	import Tooltip from './Tooltip.svelte';

	// Configuração do inventário
	let gridWidth = 5; // Número de células na horizontal
	let gridHeight = 15; // Número de células na vertical
	let cellSize = 50; // Tamanho em pixels de cada célula

	// Lista de itens no inventário
	export let items = [];

	// Matriz que representa a grade do inventário
	let grid = [];

	// Item sendo arrastado
	let draggedItem = null;
	let draggedItemInitialPos = { x: 0, y: 0 };
	let dragStartPos = { x: 0, y: 0 };
	let dragOffset = { x: 0, y: 0 };
	let isDragging = false;

	// Para reatividade do Svelte
	$: itemPositions = items.map((item) => ({ ...item }));

	onMount(() => {
		// Inicializa a grade vazia
		initializeGrid();

		// Posiciona os itens iniciais
		placeInitialItems();
	});

	// Inicializa a grade do inventário
	function initializeGrid() {
		grid = Array(gridHeight)
			.fill()
			.map(() => Array(gridWidth).fill(null));
	}

	// Coloca os itens na grade inicial
	function placeInitialItems() {
		// Limpa a grade primeiro
		initializeGrid();

		// Tenta colocar cada item no inventário
		items.forEach((item) => {
			if (!item.position) {
				// Se o item não tem posição, encontre uma disponível
				item.position = findAvailablePosition(item.width, item.height);
			}

			// Se encontrou uma posição válida, ocupe-a na grade
			if (item.position) {
				occupyGridCells(item);
			}
		});

		// Atualiza as posições para reatividade
		itemPositions = [...items];
	}

	// Encontra uma posição disponível para um item de tamanho específico
	function findAvailablePosition(width, height) {
		for (let y = 0; y <= gridHeight - height; y++) {
			for (let x = 0; x <= gridWidth - width; x++) {
				if (isPositionAvailable(x, y, width, height)) {
					return { x, y };
				}
			}
		}
		return null; // Retorna null se não houver espaço disponível
	}

	// Verifica se uma posição está disponível para um item de tamanho específico
	function isPositionAvailable(x, y, width, height) {
		if (x < 0 || y < 0 || x + width > gridWidth || y + height > gridHeight) {
			return false;
		}

		for (let j = y; j < y + height; j++) {
			for (let i = x; i < x + width; i++) {
				if (grid[j][i] !== null) {
					return false;
				}
			}
		}

		return true;
	}

	// Ocupa as células da grade com um item
	function occupyGridCells(item) {
		const { x, y } = item.position;

		for (let j = y; j < y + item.height; j++) {
			for (let i = x; i < x + item.width; i++) {
				grid[j][i] = item.id;
			}
		}
	}

	// Limpa as células ocupadas por um item
	function clearGridCells(item) {
		const { x, y } = item.position;

		for (let j = y; j < y + item.height; j++) {
			for (let i = x; i < x + item.width; i++) {
				if (i >= 0 && j >= 0 && i < gridWidth && j < gridHeight) {
					grid[j][i] = null;
				}
			}
		}
	}

	// Inicia o arrasto de um item
	function startDrag(e, item) {
		e.preventDefault();

		if (event.button !== 0) return;

		isDragging = true;
		draggedItem = item;
		draggedItemInitialPos = { ...item.position };
		dragStartPos = { x: e.clientX, y: e.clientY };
		dragOffset = { x: 0, y: 0 };

		// Remove o item da grade
		clearGridCells(item);

		// Adiciona event listeners para o movimento e fim do arrasto
		window.addEventListener('mousemove', handleDrag);
		window.addEventListener('mouseup', endDrag);
		window.addEventListener('touchmove', handleTouchDrag, { passive: false });
		window.addEventListener('touchend', endDrag);
	}

	// Manipula o arrasto de um item com mouse
	function handleDrag(e) {
		if (!draggedItem || !isDragging) return;
		e.preventDefault();

		// Calcula o offset de arrasto
		dragOffset = {
			x: Math.floor((e.clientX - dragStartPos.x) / cellSize),
			y: Math.floor((e.clientY - dragStartPos.y) / cellSize)
		};

		updateDraggedPosition();
	}

	// Manipula o arrasto de um item com toque
	function handleTouchDrag(e) {
		if (!draggedItem || !isDragging) return;
		e.preventDefault();

		const touch = e.touches[0];

		// Calcula o offset de arrasto
		dragOffset = {
			x: Math.floor((touch.clientX - dragStartPos.x) / cellSize),
			y: Math.floor((touch.clientY - dragStartPos.y) / cellSize)
		};

		updateDraggedPosition();
	}

	// Atualiza a posição do item sendo arrastado
	function updateDraggedPosition() {
		// Calcula a nova posição do item
		const newPos = {
			x: draggedItemInitialPos.x + dragOffset.x,
			y: draggedItemInitialPos.y + dragOffset.y
		};

		// Limita a nova posição dentro dos limites da grade
		newPos.x = Math.max(0, Math.min(gridWidth - draggedItem.width, newPos.x));
		newPos.y = Math.max(0, Math.min(gridHeight - draggedItem.height, newPos.y));

		// Atualiza a posição temporária
		draggedItem.position = newPos;

		// Força atualização para Svelte reagir
		itemPositions = [...items];
	}

	// Finaliza o arrasto de um item
	function endDrag() {
		if (!draggedItem || !isDragging) return;

		isDragging = false;

		// Verifica se a nova posição está disponível
		if (
			isPositionAvailable(
				draggedItem.position.x,
				draggedItem.position.y,
				draggedItem.width,
				draggedItem.height
			)
		) {
			// Se disponível, ocupa as células
			occupyGridCells(draggedItem);
		} else {
			// Se não disponível, volta para a posição inicial
			draggedItem.position = draggedItemInitialPos;
			occupyGridCells(draggedItem);
		}

		// Força atualização para Svelte reagir
		itemPositions = [...items];

		// Limpa as variáveis de arrasto
		draggedItem = null;
		dragOffset = { x: 0, y: 0 };

		// Remove os event listeners
		window.removeEventListener('mousemove', handleDrag);
		window.removeEventListener('mouseup', endDrag);
		window.removeEventListener('touchmove', handleTouchDrag);
		window.removeEventListener('touchend', endDrag);
	}

	// Calcula a posição em pixels com base na posição da grade
	function getItemStyle(item) {
		return `
      width: ${item.width * cellSize}px;
      height: ${item.height * cellSize}px;
      left: ${item.position.x * cellSize}px;
      top: ${item.position.y * cellSize}px;
      background-image: url(${item.image});
      z-index: ${draggedItem && draggedItem.id === item.id ? 1000 : 1};
      will-change: transform, left, top; /* Otimização para animações */
    `;
	}

	function itemClick(e, item) {
		e.preventDefault();
		if (isDragging) return;

		window.alert('Item clicked:', item);
	}
</script>

<div
	class=" bg-background relative"
	style="width: {gridWidth * cellSize}px; height: {gridHeight * cellSize}px;"
>
	<!-- Grade do inventário -->
	<div class="absolute">
		{#each Array(gridHeight) as _, y}
			{#each Array(gridWidth) as _, x}
				<div
					class=" border-background absolute border bg-stone-900"
					style="left: {x * cellSize}px; top: {y *
						cellSize}px; width: {cellSize}px; height: {cellSize}px;"
				></div>
			{/each}
		{/each}
	</div>

	<!-- Itens do inventário -->
	{#each itemPositions as item (item.id)}
		{#if item.position}
			<button
				on:contextmenu={(e) => itemClick(e, item)}
				class="bg-primary duration-50 border-background absolute border hover:border-white"
				style={getItemStyle(item)}
				on:mousedown={(e) => startDrag(e, item)}
				on:touchstart={(e) => startDrag(e, item)}
				class:is-dragging={isDragging && draggedItem && draggedItem.id === item.id}
			>
				<div class="item-name">{item.name}</div>
			</button>
		{/if}
	{/each}
</div>

<style>
	.inventory-item:hover {
		transform: scale(1.02);
		border-color: #9e9e9e;
	}

	.inventory-item.is-dragging {
		transform: scale(1.05);
		border-color: #be123c;
		z-index: 1000;
		opacity: 0.8;
		box-shadow: 0 0 10px rgba(255, 47, 227, 0.5);
	}

	.item-name {
		color: white;
		padding: 2px 5px;
		font-size: 0.8em;
		width: 100%;
		text-align: center;
		overflow: hidden;
		text-overflow: ellipsis;
		white-space: nowrap;
	}
</style>
