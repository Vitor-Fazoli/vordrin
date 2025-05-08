<script>
	import { onMount } from 'svelte';
	import Tooltip from '$lib/components/Tooltip.svelte';
	import Icon from '$lib/components/ItemSlot.svelte';
	import { goto } from '$app/navigation';

	$: characters = [
		{
			id: 1,
			name: 'Oyura',
			level: 1,
			weapon: {
				name: 'Rused Greatsword',
				type: 'Greatsword',
				mastery: null,
				damage: 10,
				critical_chance: 0.1,
				critical_damage: 1.5
			},
			attributes: {
				ferocity: 3,
				precision: 5,
				rhythm: 2,
				vigor: 4,
				wisdom: 1
			}
		},
		{
			id: 2,
			name: 'Slyndra',
			level: 25,
			weapon: {
				name: 'sword',
				type: 'melee',
				damage: 10,
				critical_chance: 0.1,
				critical_damage: 1.5
			},
			attributes: {
				ferocity: 2,
				precision: 5,
				rhythm: 2,
				vigor: 4,
				wisdom: 1
			}
		},
		{
			id: 3,
			name: 'Tyrnor',
			level: 100,
			weapon: {
				name: 'sword',
				type: 'melee',
				damage: 10,
				critical_chance: 0.1,
				critical_damage: 1.5
			},
			attributes: {
				ferocity: 3,
				precision: 5,
				rhythm: 2,
				vigor: 4,
				wisdom: 1
			}
		}
	];

	$: character = characters[0];

	function selectCharacter(selectedCharacter) {
		character = selectedCharacter;
	}
</script>

<div class="bg-background text-primary flex h-screen w-screen gap-5 p-5">
	<div class="border-primary flex h-full w-1/4 flex-col gap-2 border p-2">
		<div class="h-11/12 flex flex-col gap-2 overflow-auto">
			<p>Characters: {characters.length}/4</p>
			{#each characters as char, index}
				<button
					on:click={() => (character = char)}
					class="border-primary hover:bg-primary flex cursor-pointer select-none items-center justify-between border p-2 duration-150 hover:text-white {character ===
					char
						? 'bg-primary text-white'
						: ''}"
				>
					<div>
						<p class="">{char.name}</p>
					</div>
					<div class="flex items-center gap-2">
						<p class="text-xs">Level</p>
						<p class="pr-3 text-xl">{char.level}</p>
					</div>
				</button>
			{/each}
		</div>
		<button
			class="h-1/12 bg-primary text-background w-full cursor-pointer text-lg font-bold duration-150 hover:text-white"
			on:click={() => {
				goto('/main/characters/new');
			}}
			>Create new Character
		</button>
	</div>
	<div class="border-primary h-full w-3/4 border p-2">
		{#if character}
			<div class="h-11/12 flex gap-2">
				<div class="w-1/2">
					<div>
						<h2 class="text-6xl">{character.name}</h2>
						<p class="text-2xl">Level {character.level}</p>
					</div>
					<div>
						<h3 class="text-4xl">Weapon</h3>
						<p>
							{character.weapon.name} ({character.weapon.type})<br />
						</p>
					</div>
					<div>
						<h3 class="text-4xl">Attributes</h3>
						<div class="flex flex-col gap-2">
							{#each Object.entries(character.attributes) as [key, value]}
								<div class="border-primary flex w-2/3 justify-between border p-1">
									<p class="text-lg capitalize">{key}</p>
									<div
										class="bg-primary text-background flex size-7 flex-col items-center justify-center text-xl font-black"
									>
										<p>{value}</p>
									</div>
								</div>
							{/each}
						</div>
					</div>
				</div>
				<div class="flex w-1/2 flex-col items-center justify-center">
					<div class="flex flex-col justify-center gap-2">
						<div class="flex justify-center gap-2">
							<Icon name="Head"></Icon>
						</div>
						<div class="flex justify-center gap-2">
							<Icon name="Arms"></Icon>
							<Icon name="Chest"></Icon>
							<Icon name="Back"></Icon>
						</div>
						<div class="flex justify-center gap-2">
							<Icon name="Legs"></Icon>
						</div>
					</div>
				</div>
			</div>
		{:else}
			<div class="h-11/12 flex items-center justify-center">
				<p class="text-3xl">Select character to show</p>
			</div>
		{/if}
		<div class="h-1/12 flex justify-end">
			{#if character}
				<button
					on:click={() => goto(`/main/${character.id}`)}
					class="border-primary hover:bg-primary w-40 cursor-pointer border text-xl duration-150 hover:text-white"
					>Enter</button
				>
			{/if}
		</div>
	</div>
</div>
