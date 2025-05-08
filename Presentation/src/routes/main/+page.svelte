<script>
	import { goto } from '$app/navigation';

	let { data } = $props();

	const characterId = data.content;

	if (!characterId) {
		goto('/main/characters');
	} else {
		goto(`/main/characters/${characterId}`);
	}
</script>

<div>
	<main class="flex h-full w-full flex-grow">
		<div class="h-full w-auto flex-grow p-5">
			<p>Loading...</p>
		</div>
	</main>
</div>
