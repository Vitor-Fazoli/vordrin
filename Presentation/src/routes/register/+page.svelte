<script>
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
	<div class="flex flex-col items-center justify-center gap-5">
		<h1 class="text-xl">No gods. No second chances, Enter if you can.</h1>
		<form class="m-0 flex flex-col gap-2 p-0" on:submit={handleRegister}>
			<p class="text-xs">Email</p>
			<input
				class="border-primary border pl-2"
				type="email"
				name="email"
				required
				bind:value={email}
			/>
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
				type="submit">Register</button
			>
		</form>
	</div>
</div>
