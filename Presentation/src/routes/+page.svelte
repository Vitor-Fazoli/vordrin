<script>
	import { goto } from '$app/navigation';
	import { _ } from 'svelte-i18n';
</script>

<div class="flex h-screen w-screen flex-col items-center justify-center bg-black text-white">
	<main class="flex flex-col items-center justify-center gap-5 p-2">
		<h1 class=" text-9xl font-black text-rose-700 underline">VORDRIN</h1>
		<div class="flex gap-5">
			<button class="cursor-pointer hover:underline" on:click={goto('/login')}>{$_('login')}</button
			>
			<button class="cursor-pointer hover:underline" on:click={goto('/register')}
				>{$_('register')}</button
			>
		</div>
	</main>
</div>
