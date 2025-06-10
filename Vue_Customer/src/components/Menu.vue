<script setup lang="ts">
defineProps<{ activeMenu: string }>()
const emit = defineEmits(['update:activeMenu'])

const menuItems = [
  { label: 'VOIR TOUT', value: 'voir' },
  { label: 'AJOUTER', value: 'ajouter' },
  { label: 'MODIFIER', value: 'modifier' },
  { label: 'SUPPRIMER', value: 'supprimer' }
]
</script>

<template>
  <div class="sidebar">
    <h1>Findis Customer</h1>
    <ul>
      <li v-for="item in menuItems" :key="item.value" :class="{ active: item.value === activeMenu }"
          @click="emit('update:activeMenu', item.value)" >
        {{ item.label }}
      </li>
    </ul>
  </div>
</template>

<style scoped>

</style>

