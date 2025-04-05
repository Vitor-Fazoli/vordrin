<script>
	import { page } from '$app/stores';
</script>

<div class="flex h-screen w-screen flex-col items-center justify-center gap-5 bg-black text-white">
	<div class="flex flex-col items-center justify-center gap-1">
		<p class="text-9xl font-bold text-rose-700">{$page.status}</p>
		<p class="">{$page.error.message}</p>
	</div>
	<a class="underline" href="/">Go to home</a>
</div>
