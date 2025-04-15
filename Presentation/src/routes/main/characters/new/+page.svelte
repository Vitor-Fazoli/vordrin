<script>
	let age = 40;

	function getRoast() {
		if (age <= 15) return 'Bad idea, born now!';
		if (age <= 20) return "You're either broke or confused. Probably both.";
		if (age <= 25) return 'The quarter-life crisis is calling.';
		if (age <= 30) return "You're an adult... technically.";
		if (age <= 35) return 'Knees? What knees?';
		if (age <= 40) return "You complain about bills like it's a hobby.";
		if (age <= 50) return "You're the 'back in my day' generation now.";
		if (age <= 60) return 'You now grunt when sitting down.';
		if (age <= 70) return "You'll die soon. But at least you're wise?";
	}

	function getAgeBuff() {
		if (age <= 15) return '-2 attribute points, but 2/10 more experience.';
		if (age <= 25) return '+1 Ferocity and +1 Precision, but -1 Wisdom.';
		if (age <= 35) return 'you have 1/10 more health, but -1 Precision.';
		if (age <= 50) return '+2 Wisdom and but -1 Ferocity.';
		if (age <= 70) return '1/10 less experience, but +1 Wisdom.';
	}
</script>

<div class="bg-background h-screen w-screen text-white">
	<div class="flex h-full w-full flex-col items-center justify-center gap-5 p-5">
		<div class="flex w-full flex-col items-center justify-center gap-5">
			<h1 class="text-3xl font-bold">Create your character</h1>
		</div>

		<div class="flex w-full flex-col items-center justify-center gap-5">
			<div class="flex w-full flex-col items-center justify-center gap-5">
				<h2 class="text-xl">Character name</h2>
				<p class="text-primary text-sm">Your name will be displayed in the game</p>
				<input class="border-primary w-2/4 border" type="text" />
				<div class="mx-10 flex w-full flex-col items-center justify-center gap-2">
					<h2 class="text-xl">Character age</h2>
					<p class="text-primary text-sm">
						Age isn't just a number — it shapes your role in this world.
					</p>
					<div class="h-50 flex w-2/4 gap-2 text-center">
						<div
							class="border-primary flex w-2/3 flex-col items-center justify-center gap-1 border p-4"
						>
							<input
								type="range"
								bind:value={age}
								min="10"
								max="70"
								class="text-primary bg-primary w-8/10 h-1 cursor-pointer appearance-none border-0"
							/>
							<p>{age}</p>
							<p>{getRoast()}</p>
						</div>
						<div class="border-primary text-md flex w-1/3 items-center justify-center border p-2">
							<p>{getAgeBuff()}</p>
						</div>
					</div>
				</div>
				<button
					class="bg-primary text-whit3 outline-offset-3 outline-primary cursor-pointer px-4 py-2 duration-150 hover:outline-1"
					>Next step</button
				>
			</div>
		</div>
	</div>
</div>

<style>
	input[type='range']::-webkit-slider-thumb {
		-webkit-appearance: none;
		height: 0.5rem; /* adjust size as needed */
		width: 0.5rem;
		background-color: #be123c; /* Tailwind blue-500 */
		cursor: pointer;
		transform: translateY(-25%);
	}

	input[type='range']::-moz-range-thumb {
		height: 0.5rem;
		width: 0.5rem;
		background-color: white;
		cursor: pointer;
	}
</style>
