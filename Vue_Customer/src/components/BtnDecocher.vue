<style scoped>
button {
  padding: 17px 40px;
  border-radius: 10px;
  border: 0;
  background-color: rgb(255, 56, 86);
  letter-spacing: 1.5px;
  font-size: 15px;
  transition: all 0.3s ease;
  box-shadow: rgb(201, 46, 70) 0px 10px 0px 0px;
  color: hsl(0, 0%, 100%);
  cursor: pointer;
}

button:hover {
  box-shadow: rgb(201, 46, 70) 0px 7px 0px 0px;
  transform: translateY(3px);
}

button:active {
  background-color: rgb(255, 56, 86);
  /*50, 168, 80*/
  box-shadow: rgb(201, 46, 70) 0px 0px 0px 0px;
  transform: translateY(10px);
  transition: 200ms;
}
</style>

<template>
  <button v-if="selectedIds.length > 0" @click="$emit('clearSelection')">
    Tout décocher
  </button>
</template>

<script setup lang="ts">
defineProps<{
  selectedIds: string[]
}>()

defineEmits(['clearSelection'])
</script>
