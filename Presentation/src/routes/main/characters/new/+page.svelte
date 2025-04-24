<script>
	let weaponsTypes = [
		{
			name: 'Greatsword',
			damage: 2.5,
			critical_chance: 0.1,
			critical_damage: 1.5
		},
		{
			name: 'Longbow',
			damage: 3.5,
			critical_chance: 0.2,
			critical_damage: 2.0
		},
		{
			name: 'Injecteur',
			damage: 1.5,
			critical_chance: 0.15,
			critical_damage: 1.8,
			left_click: {
				name: 'Percer',
				description: ''
			},
			right_click: {
				name: 'Vaccin',
				description: ''
			}
		}
	];
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
				<div class="flex flex-col items-center justify-center gap-2">
					<h2 class="text-xl">Choose your weapon</h2>
					<p class="text-primary text-sm">Select your weapon to define your combat style</p>
					<div class="mb-6 flex gap-4" id="weapon-selector">
						{#each as}{/each}
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
