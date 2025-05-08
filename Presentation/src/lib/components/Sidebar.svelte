<script>
	import Inventory from './Inventory.svelte';
	import BackpackIcon from '$lib/assets/icons/backpack.png';

	let activeMenu = null;
	let activeComponent = null;

	const menus = [{ id: 1, label: 'Inventory', tag: 'Página Inicial', component: Inventory }];

	function showComponent(menu) {
		if (activeMenu === menu.id) {
			// Se o menu já estiver ativo, desativa-o
			activeMenu = null;
			activeComponent = null;
		} else {
			// Caso contrário, ativa o menu e exibe o componente
			activeMenu = menu.id;
			activeComponent = menu.component;
		}
	}
</script>

<div class="relative flex overflow-hidden">
	<div class="bg-background border-primary w-13 flex-shrink-0 border-r p-2">
		{#each menus as menu}
			<div class="size-16">
				<button
					class={activeMenu === menu.id
						? 'outline-primary cursor-pointer border outline outline-offset-2'
						: 'cursor-pointer'}
					on:click={() => showComponent(menu)}
				>
					<img src={BackpackIcon} alt="Backpack Icon" width="32px" height="32px" />
				</button>
			</div>
		{/each}
	</div>
	<div class="w-auto overflow-y-auto">
		{#if activeComponent}
			<svelte:component this={activeComponent} />
		{/if}
	</div>
</div>
