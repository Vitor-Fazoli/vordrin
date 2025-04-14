<script>
	import { onMount, onDestroy } from 'svelte';

	export let text = '';

	let tooltip;
	let visible = false;
	let mouseX = 0;
	let mouseY = 0;
	let position = 'top';
	let isClient = false;

	onMount(() => {
		isClient = true;
	});

	function handleMouseMove(event) {
		if (!isClient) return; // Não executa no servidor

		mouseX = event.clientX;
		mouseY = event.clientY;

		if (tooltip) {
			const { offsetHeight, offsetWidth } = tooltip;
			const tooltipRect = tooltip.getBoundingClientRect();

			// Verifique se o tooltip está saindo da tela
			const willOverflowTop = mouseY - offsetHeight - 10 < 0;
			const willOverflowBottom = mouseY + offsetHeight + 10 > window.innerHeight;
			const willOverflowLeft = mouseX - offsetWidth - 10 < 0;
			const willOverflowRight = mouseX + offsetWidth + 10 > window.innerWidth;

			// Se o topo não couber, vamos posicionar o tooltip em baixo
			position = willOverflowTop ? 'bottom' : 'top';

			// Se sair da tela à esquerda ou direita, ajustamos a posição
			if (willOverflowLeft) {
				tooltip.style.left = '20px';
			} else if (willOverflowRight) {
				tooltip.style.left = `${window.innerWidth - offsetWidth - 20}px`;
			} else {
				tooltip.style.left = `${mouseX + 20}px`;
			}

			tooltip.style.top =
				position === 'top' ? `${mouseY - offsetHeight - 10}px` : `${mouseY + 20}px`;

			// Se sair da tela no topo, reposicionamos embaixo
			if (willOverflowBottom) {
				tooltip.style.top = `${mouseY - offsetHeight - 10}px`;
			}
		}
	}

	function handleMouseEnter() {
		if (!isClient) return; // Não executa no servidor
		visible = true;
		window.addEventListener('mousemove', handleMouseMove);
	}

	function handleMouseLeave() {
		if (!isClient) return; // Não executa no servidor
		visible = false;
		window.removeEventListener('mousemove', handleMouseMove);
	}

	onDestroy(() => {
		if (!isClient) return; // Não executa no servidor
		window.removeEventListener('mousemove', handleMouseMove);
	});
</script>

<span
	role="button"
	aria-describedby="tooltip"
	on:mouseenter={handleMouseEnter}
	on:mouseleave={handleMouseLeave}
	tabindex=""
>
	<slot></slot>
</span>
{#if visible}
	<div
		id="tooltip"
		role="tooltip"
		bind:this={tooltip}
		class="tooltip bg-background border-primary visible z-10 border p-4 text-sm text-white"
	>
		{text}
	</div>
{/if}

<style>
	.tooltip {
		position: fixed;
		pointer-events: none;
		transition: opacity 0.2s ease;
	}

	.tooltip.visible {
		opacity: 1;
	}
</style>
