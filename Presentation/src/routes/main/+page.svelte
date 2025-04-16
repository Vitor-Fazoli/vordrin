<script>
	import Inventory from '../../components/Inventory.svelte';

	// Dados dos itens para o inventário
	const inventoryItems = [
		{
			id: 1,
			name: 'Poção de Cura',
			width: 1,
			height: 1,
			position: { x: 0, y: 0 }
		},
		{
			id: 2,
			name: 'Espada Longa',
			width: 1,
			height: 2,
			position: { x: 1, y: 0 }
		},
		{
			id: 3,
			name: 'Escudo',
			width: 2,
			height: 2,
			position: { x: 2, y: 0 }
		},
		{
			id: 4,
			name: 'Granada',
			width: 1,
			height: 1,
			position: { x: 4, y: 0 }
		},
		{
			id: 5,
			name: 'Rifle Sniper',
			width: 2,
			height: 1,
			position: { x: 0, y: 2 }
		},
		{
			id: 6,
			name: 'Kit Médico',
			width: 2,
			height: 2,
			position: { x: 3, y: 5 }
		},
		{
			id: 7,
			name: 'Munição',
			width: 1,
			height: 1,
			position: { x: 4, y: 8 }
		},
		{
			id: 8,
			name: 'Colete',
			width: 2,
			height: 3,
			position: { x: 3, y: 10 }
		}
	];
</script>

<div class="bg-background flex h-screen w-screen">
	<div class="inventory-wrapper overflow-auto pr-4">
		<Inventory
			class="overflow-scroll"
			items={inventoryItems}
			gridWidth={5}
			gridHeight={15}
			cellSize={60}
		/>
	</div>
	<main></main>
</div>

<style>
	h1 {
		color: #333;
		text-align: center;
	}

	p {
		text-align: center;
		margin-bottom: 20px;
	}

	.inventory-wrapper {
		display: flex;
		justify-content: center;
		margin-bottom: 20px;
	}

	.instructions {
		background-color: #f7f7f7;
		padding: 15px;
		border-radius: 5px;
		margin-top: 20px;
	}

	.instructions h3 {
		margin-top: 0;
	}

	.instructions ul {
		margin-bottom: 0;
	}
</style>
