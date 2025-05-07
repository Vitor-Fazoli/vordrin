<script>
	import { goto } from '$app/navigation';

	let username;

	let attributes = [
		{ name: 'Ferocity', value: 3 },
		{ name: 'Precision', value: 3 },
		{ name: 'Rhythm', value: 3 },
		{ name: 'Vigor', value: 3 },
		{ name: 'Wisdom', value: 3 }
	];

	const pointsMax = 20;

	$: totalPoints = attributes.reduce((sum, attr) => sum + attr.value, 0);
	$: remainingPoints = pointsMax - totalPoints;

	function decreaseValue(index) {
		if (attributes[index].value > 1) {
			attributes[index].value -= 1;
		}
	}

	function increaseValue(index) {
		const totalSum = attributes.reduce((sum, attr) => sum + attr.value, 0);

		if (totalSum < pointsMax) {
			attributes[index].value += 1;
		}
	}
</script>

<div class="bg-background h-screen w-screen text-white">
	<div class="flex h-full w-full flex-row items-center justify-center gap-5 p-5">
		<div class="flex h-full w-2/3 flex-col gap-5">
			<div class="flex h-4/6 gap-5">
				<div class="flex w-3/5 flex-col gap-5">
					<div class="flex w-full items-center justify-start gap-2">
						<button
							on:click={goto('/main/characters')}
							class="bg-primary hover:bg-background border-primary flex size-10 cursor-pointer items-center justify-center border text-2xl text-white duration-150"
						>
							<i class="fa-solid fa-arrow-left"></i>
						</button>
						<h2 class="text-4xl">Create your character</h2>
					</div>
					<div class="border-primary flex w-full flex-col items-center border p-5">
						<p class="border-primary w-full border-b text-center">Name</p>
						<input
							type="text"
							class="w-full py-1 focus:outline-none"
							placeholder="Write your name"
							bind:value={username}
						/>
					</div>
					<div class="border-primary flex h-full flex-col border p-5">
						<p class="border-primary w-full border-b text-center text-2xl">Biography</p>
						<textarea
							placeholder="Write your past"
							class="h-full w-full resize-none py-1 focus:outline-none"
							name=""
							id=""
						></textarea>
					</div>
				</div>
				<div class="border-primary flex h-full w-2/5 flex-col items-center gap-1 border p-5">
					<p class="border-primary w-full border-b text-center text-2xl">Attributtes</p>
					<div class="flex w-full flex-col gap-2">
						{#each attributes as attribute, i}
							<div class="border-primary flex w-full justify-between gap-1 border p-1">
								<p class="text-xl">{attribute.name}</p>
								<div class="flex w-full items-end justify-end gap-1">
									<div class="bg-primary flex size-7 items-center justify-center">
										{attribute.value}
									</div>
									<button
										class="bg-primary flex size-7 cursor-pointer items-center justify-center"
										on:click={() => decreaseValue(i)}
									>
										<i class="fa-solid fa-minus"></i>
									</button>
									<button
										class="bg-primary flex size-7 cursor-pointer items-center justify-center"
										on:click={() => increaseValue(i)}
									>
										<i class="fa-solid fa-plus"></i>
									</button>
								</div>
							</div>
						{/each}
					</div>
					<p class="m-3 text-sm">
						Remaining points: {remainingPoints} / {pointsMax}
					</p>
				</div>
			</div>
			<div class="border-primary flex h-2/6 items-center gap-2 border-y">
				<h1
					class="border-primary flex h-full w-1/6 items-center justify-center border-x px-2 text-3xl"
				>
					Heirloom
				</h1>
				<div class=" flex h-full w-2/6 flex-col gap-2 overflow-auto p-2 text-sm">
					<button
						class="bg-primary border-primary hover:text-primary cursor-pointer border duration-150 hover:bg-transparent"
						>Balistic Shield</button
					>
					<button
						class="bg-primary border-primary hover:text-primary cursor-pointer border duration-150 hover:bg-transparent"
						>Plastic Vampire Teeth</button
					>
					<button
						class="bg-primary border-primary hover:text-primary cursor-pointer border duration-150 hover:bg-transparent"
						>Sunscreen</button
					>
					<button
						class="bg-primary border-primary hover:text-primary cursor-pointer border duration-150 hover:bg-transparent"
						>Political Flag</button
					>
					<button
						class="bg-primary border-primary hover:text-primary cursor-pointer border duration-150 hover:bg-transparent"
						>Hephaestus's Briefcase</button
					>
					<button
						class="bg-primary border-primary hover:text-primary cursor-pointer border duration-150 hover:bg-transparent"
						>Accelerated Boots</button
					>
					<button
						class="bg-primary border-primary hover:text-primary cursor-pointer border duration-150 hover:bg-transparent"
						>Surgical Mask</button
					>
				</div>
				<div
					class=" border-primary flex h-full w-3/6 flex-col items-center justify-center border-r p-5"
				>
					<p class="text-gray-500">Nothing selected</p>
				</div>
			</div>
		</div>
		<div class="border-primary flex h-full w-1/3 flex-col gap-5 border p-5">
			<p class="border-primary w-full border-b text-center text-2xl">Weapon</p>
			<div class="border-primary h-2/3 w-full border"></div>
			<button
				class="bg-primary border-primary hover:text-primary cursor-pointer border duration-150 hover:bg-transparent"
				>Rusted Greatsword</button
			>
			<button
				class="bg-primary border-primary hover:text-primary cursor-pointer border duration-150 hover:bg-transparent"
				>Stinger Model K</button
			>
			<button
				class="bg-primary border-primary hover:text-primary cursor-pointer border duration-150 hover:bg-transparent"
				>Silence R17</button
			>
		</div>
		<div
			class="bg-primary border-primary hover:bg-background flex h-full w-12 cursor-pointer items-center justify-center border duration-150"
		>
			<i class="fa-solid fa-arrow-right fa-2xl text-white"></i>
		</div>
	</div>
</div>
