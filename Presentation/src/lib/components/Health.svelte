<script>
	export let health = 20; // Current health
	export let maxHealth = 100; // Maximum health
	export let widthBar = 20;

	$: healthPercentage = (health / maxHealth) * 100;
</script>

<div
	class="outline-primary outline-offset-3 outline transition-all duration-150 ease-in-out"
	style="width: {widthBar}%;"
>
	<div class="bg-primary p-0.5" style="width: {healthPercentage}%;"></div>
</div>
