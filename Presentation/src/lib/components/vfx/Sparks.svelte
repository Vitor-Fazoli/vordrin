<!-- Sparks.svelte -->
<script>
	import { onMount, onDestroy } from 'svelte';

	let canvas;
	let ctx;
	let animationFrameId;
	let particles = [];

	const fireWidth = 1000;
	const fireHeight = 150;

	class Spark {
		constructor() {
			const centerX = canvas.width / 2;
			const bottomY = canvas.height;

			// Posição inicial (concentrada em uma área específica)
			this.x = centerX + (Math.random() * fireWidth - fireWidth / 2);
			this.y = bottomY;

			this.size = 3;

			this.speedY = Math.random() * 2 + 0.5;

			this.wobbleX = 0;
			this.wobbleSpeed = (Math.random() * 1 - 0.25) * 0.2;

			this.lifespan = Math.random() * 80 + 50;
			this.age = 0;

			this.color = '#be123c';

			this.opacity = 1.0;
		}

		// Atualizar posição e propriedades
		update() {
			// Envelhecer a faísca
			this.age++;

			// Movimento para cima (mais estável)
			this.y -= this.speedY;

			// Movimento lateral muito sutil
			this.wobbleX += this.wobbleSpeed;
			this.x += Math.sin(this.wobbleX) * 0.5;

			this.opacity = Math.max(0, 1 - (this.age - this.lifespan * 0.7) / (this.lifespan * 0.3));
		}

		// Desenhar a faísca
		draw(ctx) {
			const pixelX = Math.round(this.x);
			const pixelY = Math.round(this.y);

			ctx.fillStyle = this.color;
			ctx.globalAlpha = this.opacity;
			ctx.fillRect(pixelX, pixelY, this.size, this.size);
			ctx.globalAlpha = 1;
		}

		isFinished() {
			const centerX = canvas.width / 2;
			const bottomY = canvas.height - 50;

			return (
				this.opacity <= 0.05 ||
				this.age >= this.lifespan ||
				this.y < bottomY - fireHeight ||
				this.x < centerX - fireWidth ||
				this.x > centerX + fireWidth
			);
		}
	}

	// Função de animação
	function animate() {
		// Limpar o canvas com fundo preto completo para manter o fundo totalmente preto
		ctx.fillStyle = 'rgba(0, 0, 0, 0.9)';
		ctx.fillRect(0, 0, canvas.width, canvas.height);

		// Adicionar novas faíscas ocasionalmente (menos frequentemente para simular um fogo morrendo)
		if (Math.random() > 0.95) {
			const sparkCount = Math.random() < 0.2 ? 3 : 1; // Ocasionalmente liberar uma pequena rajada
			for (let i = 0; i < sparkCount; i++) {
				particles.push(new Spark(canvas.width, canvas.height));
			}
		}

		// Atualizar e desenhar cada faísca
		for (let i = 0; i < particles.length; i++) {
			particles[i].update();
			particles[i].draw(ctx);

			// Remover faíscas que terminaram seu ciclo
			if (particles[i].isFinished()) {
				particles.splice(i, 1);
				i--;
			}
		}

		// Continuar a animação
		animationFrameId = requestAnimationFrame(animate);
	}

	onMount(() => {
		// Inicializar o canvas
		ctx = canvas.getContext('2d');
		ctx.imageSmoothingEnabled = false;
		canvas.width = window.innerWidth;
		canvas.height = window.innerHeight;

		// Iniciar a animação
		animate();

		// Retornar função de limpeza
		return () => {
			cancelAnimationFrame(animationFrameId);
		};
	});

	onDestroy(() => {
		if (animationFrameId) {
			cancelAnimationFrame(animationFrameId);
		}
	});
</script>

<canvas class="absolute overflow-clip" bind:this={canvas}></canvas>
