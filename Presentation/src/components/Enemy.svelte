<script>
	import Health from './Health.svelte';

	export let imageSrc = 'mecha-assassin.png'; // Path to the enemy image
	export let health = 60; // Current health of the enemy
	export let maxHealth = 100; // Maximum health of the enemy
	export let name = 'Mecha Assassin'; // Name of the enemy
</script>

<div class="flex w-full flex-col items-center justify-center gap-2 p-5">
	<p class="text-primary">{name}</p>
	<Health {health} {maxHealth} widthBar="50" />
	<img class=" w-1/10" src="/enemies/{imageSrc}" alt="Enemy" />
</div>