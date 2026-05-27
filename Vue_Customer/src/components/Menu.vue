<script setup lang="ts">
defineProps<{ activeMenu: string }>()
const emit = defineEmits(['update:activeMenu'])

const menuItems = [
  {
    label: 'ACCUEIL',
    value: 'home',
    icon: `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512" width="20"><path d="M575.8 255.5c0 18-15 32.1-32 32.1l-32 0 .7 160.2c0 2.7-.2 5.4-.5 8.1l0 16.2c0 22.1-17.9 40-40 40l-16 0c-1.1 0-2.2 0-3.3-.1c-1.4 .1-2.8 .1-4.2 .1L416 512l-24 0c-22.1 0-40-17.9-40-40l0-24 0-64c0-17.7-14.3-32-32-32l-64 0c-17.7 0-32 14.3-32 32l0 64 0 24c0 22.1-17.9 40-40 40l-24 0-31.9 0c-1.5 0-3-.1-4.5-.2c-1.2 .1-2.4 .2-3.6 .2l-16 0c-22.1 0-40-17.9-40-40l0-112c0-.9 0-1.9 .1-2.8l0-69.7-32 0c-18 0-32-14-32-32.1c0-9 3-17 10-24L266.4 8c7-7 15-8 22-8s15 2 21 7L564.8 231.5c8 7 12 15 11 24z"/></svg>`
  },
  {
    label: 'AJOUTER',
    value: 'add',
    icon: `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" width="20"><path d="M64 32C28.7 32 0 60.7 0 96L0 416c0 35.3 28.7 64 64 64l320 0c35.3 0 64-28.7 64-64l0-320c0-35.3-28.7-64-64-64L64 32zM200 344l0-64-64 0c-13.3 0-24-10.7-24-24s10.7-24 24-24l64 0 0-64c0-13.3 10.7-24 24-24s24 10.7 24 24l0 64 64 0c13.3 0 24 10.7 24 24s-10.7 24-24 24l-64 0 0 64c0 13.3-10.7 24-24 24s-24-10.7-24-24z"/></svg>`
  }
]

</script>

<template>
  <div class="sidebar">
    <h1>Findis Customer</h1>
    <ul>
      <li v-for="item in menuItems" :key="item.value" :class="{ active: item.value === activeMenu }"
          @click="emit('update:activeMenu', item.value)" >
        <span class="icon" v-html="item.icon"></span>
        {{ item.label }}
      </li>
    </ul>
  </div>
</template>

<style scoped>

.sidebar {
  width: 200px;
  background-color: #edf2f9;
  color: #343434;
  /*height: 100vh;*/
  text-align: center;
  padding: 0;
}

.sidebar h1 {
  text-align: center;
}

.sidebar ul {
  list-style: none;
  padding: 0;
}

.sidebar ul li {
  display: flex;
  justify-content: center;
  gap: 25px;
  padding: 10px 20px;
  font-weight: bold;
  cursor: pointer;
}

.sidebar ul li.active , .sidebar ul li:hover {
  background-color: rgba(228, 223, 223, 0.51);
  color: black;
}

</style>

