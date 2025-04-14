<script>
	import Tooltip from './Tooltip.svelte';

	export let icon;
	export let name;
	export let description;
</script>

<Tooltip text={description} position="top">
	<div class="border-primary flex size-20 flex-col items-center justify-center border text-xl">
		{#if icon != null}
			<i class={icon}></i>
		{/if}
		<p>{name}</p>
	</div>
</Tooltip>
