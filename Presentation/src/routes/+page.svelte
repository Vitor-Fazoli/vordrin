<script>
	import { goto } from '$app/navigation';
</script>

<div class="flex h-screen w-screen flex-col items-center justify-center bg-black text-white">
	<main class="flex flex-col items-center justify-center gap-5 p-2">
		<h1 class=" text-9xl font-black text-rose-700 underline">VORDRIN</h1>
		<div class="flex gap-5">
			<button on:click={goto('/login')}>I'm know this lands</button>
			<button on:click={goto('/register')}>try now!</button>
		</div>
	</main>
</div>
