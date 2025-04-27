<script>
	import Sidebar from '../../components/Sidebar.svelte';
	import Enemy from '../../components/Enemy.svelte';
</script>

<div class="bg-background flex h-screen w-screen">
	<main class="flex h-full w-full flex-grow">
		<Sidebar></Sidebar>
		<div class="h-full w-auto flex-grow p-5"><Enemy></Enemy></div>
	</main>
</div>
