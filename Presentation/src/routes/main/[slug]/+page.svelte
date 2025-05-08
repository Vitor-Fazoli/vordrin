<script>
	import { page } from '$app/stores';
	import { get } from 'svelte/store';
	import Sidebar from '$lib/components/Sidebar.svelte';
	import Enemy from '$lib/components/Enemy.svelte';
	import { goto } from '$app/navigation';
	import { onMount } from 'svelte';
	import Battle from '$lib/components/Battle.svelte';

	const character_id = get(page).params.slug;

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

	onMount(() => {
		const validIds = characters.map((c) => c.id);
		if (!validIds.includes(Number(character_id))) {
			goto('/main/characters');
		} else {
			console.log('Character found:', character_id);
		}
	});
</script>

<div class="bg-background flex h-screen w-screen">
	<main class="flex h-full w-full flex-grow">
		<Sidebar></Sidebar>
		<div class="h-full w-auto flex-grow">
			<Battle></Battle>
		</div>
	</main>
</div>
