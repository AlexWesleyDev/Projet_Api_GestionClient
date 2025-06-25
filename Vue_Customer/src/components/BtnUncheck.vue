<style scoped>
button {
  width: 35px;
  height:  40px;
  border-radius: 10px;
  border: 0;
  background-color: white;
  letter-spacing: 1.5px;
  font-size: 15px;
  transition: all 0.3s ease;
  /*box-shadow: rgb(201, 46, 70) 0px 10px 0px 0px;*/
  color: hsl(0, 0%, 100%);
  cursor: pointer;
}

button:hover {
  /*box-shadow: rgb(201, 46, 70) 0px 7px 0px 0px;*/
  transform: translateY(3px);
}

button:active {
  background-color: rgb(255, 56, 86);
  /*50, 168, 80*/
  box-shadow: rgb(201, 46, 70) 0px 0px 0px 0px;
  transform: translateY(10px);
  transition: 200ms;
}

svg{
  width: 100%;
  height:  100%;
}

</style>

<template>
  <button v-if="selectedIds.length > 0" @click="$emit('clearSelection')">
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 384 512"><!--!Font Awesome Free 6.7.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2025 Fonticons, Inc.--><path fill="#ff3856" d="M342.6 150.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0L192 210.7 86.6 105.4c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L146.7 256 41.4 361.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0L192 301.3 297.4 406.6c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L237.3 256 342.6 150.6z"/></svg>
  </button>
</template>

<script setup lang="ts">
defineProps<{
  selectedIds: string[]
}>()

defineEmits(['clearSelection'])

</script>
