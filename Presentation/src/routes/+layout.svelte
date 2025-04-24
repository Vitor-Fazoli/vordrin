<script>
	import { onMount } from 'svelte';
	import { setupI18n } from '$lib/i18n';
	import '../app.css';

	var ready = $state(false);

	onMount(async () => {
		await setupI18n();
		$: ready = true;
	});

	let { children } = $props();
</script>

{#if ready}
	{@render children()}
{:else}
	<div class=" flex h-screen w-screen items-center justify-center bg-black">
		<div class="border-primary size-10 animate-spin border"></div>
	</div>
{/if}
