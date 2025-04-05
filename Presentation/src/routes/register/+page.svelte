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
		<h1>Register</h1>
		<form class="m-0 flex flex-col gap-2 p-0" on:submit={handleRegister}>
			<input type="email" name="email" placeholder="Email" required bind:value={email} />
			<input type="text" name="username" placeholder="Username" required bind:value={username} />
			<input
				type="password"
				name="password"
				placeholder="Password"
				required
				bind:value={password}
			/>
			<button type="submit">Register</button>
		</form>
	</div>
</div>
