<script>
	export let item = null;


</script>

<Tooltip
	text={`<p class="font-bold text-lg ">${item.name}</p> <br/> ${item.description}`}
	position="top"
	class="absolute"
>
	<button
		on:contextmenu={(e) => itemClick(e, item)}
		class="bg-primary duration-50 border-background absolute border hover:border-white"
		style={getItemStyle(item)}
		on:mousedown={(e) => startDrag(e, item)}
		on:touchstart={(e) => startDrag(e, item)}
		class:is-dragging={isDragging && draggedItem && draggedItem.id === item.id}
	>
		<img
			src={'/items/' + item.image}
			alt={item.name}
			class="bg-background border-primary flex h-full w-full items-center justify-center border text-white"
		/>
	</button>
</Tooltip>
