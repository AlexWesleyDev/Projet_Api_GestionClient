<script setup lang="ts">
defineProps<{ activeMenu: string }>()
const emit = defineEmits(['update:activeMenu'])

const menuItems = [
  { label: 'ACCUEIL', value: 'accueil' },
  { label: 'AJOUTER', value: 'ajouter' }
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
/* Partie Menu */
.sidebar {
  width: 300px;
  background-color: #edf2f9;
  color: #343434;
  /*height: 100vh;*/
  padding: 20px 0;
}

.sidebar h1 {
  text-align: center;
  margin-bottom: 30px;
}

.sidebar ul {
  list-style: none;
  padding: 0;
}

.sidebar ul li {
  padding: 10px 20px;
  font-weight: bold;
  cursor: pointer;
}

.sidebar ul li.active , .sidebar ul li:hover {
  background-color: yellow;
  color: black;
}
</style>

