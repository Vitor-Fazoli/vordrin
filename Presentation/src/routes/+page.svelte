<script>
	import { goto } from '$app/navigation';
	import Mushroom from '$lib/assets/mushroom_login.png';
	import Sparks from '$lib/components/vfx/Sparks.svelte';

	let email;
	let username;
	let password;

	function handleRegister() {
		// Prevent the default form submission
		event.preventDefault();

		// Perform registration logic here (e.g., API call)
		console.log('Registering:', { email, username, password });

		// Redirect to the login page after successful registration
		window.location.href = '/login';
	}
</script>

<div class="flex h-screen w-screen flex-col items-center justify-center gap-5 bg-black text-white">
	<Sparks />
	<div class="z-10 flex flex-row items-center justify-center gap-[100px]">
		<img src={Mushroom} alt="" />
		<div>
			<form class="m-0 flex flex-col gap-2 p-0">
				<h1 class="text-primary text-5xl font-black underline">Vordrin</h1>
				<p class="text-xs">Username</p>
				<input
					class="border-primary border pl-2"
					type="text"
					name="username"
					required
					bind:value={username}
				/>
				<p class="text-xs">Password</p>
				<input
					class="border-primary border pl-2"
					type="password"
					name="password"
					required
					bind:value={password}
				/>
				<button
					class="text-primary border-primary cursor-pointer border duration-150 hover:border-white hover:bg-white hover:text-black"
					type="submit">Sign-in</button
				>
			</form>
			<button
				onclick={goto('/register')}
				class="hover:bg-primary hover:none w-full cursor-pointer text-sm underline duration-150"
				>Create your account</button
			>
		</div>
	</div>
	<a href="https://github.com/Vitor-Fazoli" class="cursor-pointer text-xs text-white underline"
		>Developed by Vitor Fazoli</a
	>
</div>
