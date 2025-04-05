import tailwindcss from '@tailwindcss/vite';
import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vite';

export default defineConfig({
	plugins: [tailwindcss(), sveltekit()],
	server: {
		proxy: {
			'/gameHub': {
				target: process.env.VITE_BACKEND_URL || 'http://localhost:5189',
				changeOrigin: true,
				ws: true,
				secure: false,
			},
			'/api': {
				target: process.env.VITE_BACKEND_URL || 'http://localhost:5189',
				changeOrigin: true,
				secure: false
			}
		},
	}
});
