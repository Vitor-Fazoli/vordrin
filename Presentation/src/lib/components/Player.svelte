<script>
	import Health from './Health.svelte';

	let health = 5; // Current health
	let maxHealth = 5; // Maximum health
	let name = 'Oyura'; // Player name
</script>

<div class="flex h-auto w-40 flex-col items-center gap-1">
	<div class="h-full w-full">
		<p class="w-full text-center text-white">{name}</p>
		<div class="border-primary flex w-full flex-col items-center gap-2 border p-2">
			<div class="flex w-full flex-col items-center gap-1">
				<p class="text-xs text-white">health: {health} / {maxHealth}</p>
				<Health {health} {maxHealth} widthBar={100} />
			</div>
		</div>
	</div>
	<div class="flex w-full flex-wrap content-center gap-0.5">
		<div class="border-primary size-4 border bg-sky-950"></div>
		<div class="border-primary size-4 border bg-sky-950"></div>

		<div class="border-primary size-4 border bg-sky-950"></div>

		<div class="border-primary size-4 border bg-sky-950"></div>
		<div class="border-primary size-4 border bg-sky-950"></div>
		<div class="border-primary size-4 border bg-sky-950"></div>
		<div class="border-primary size-4 border bg-sky-950"></div>
		<div class="border-primary size-4 border bg-sky-950"></div>
		<div class="border-primary size-4 border bg-sky-950"></div>
	</div>
</div>
