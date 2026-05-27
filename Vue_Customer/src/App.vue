<template>
  <div class="flex h-screen">
    <!-- (Sidebar) -->
    <Menu :activeMenu="activeMenu" @update:activeMenu="activeMenu = $event" />

    <!-- Main Principal -->
    <div class="flex flex-col flex-1">
      <!-- Header -->
      <Header />

      <!-- Contenu des informations -->
      <div class="p-4 overflow-y-auto">
        <Home v-if="activeMenu === 'home'" />
        <AddNewCustomer @clientAjoute="activeMenu = 'accueil'" v-if="activeMenu === 'add'" />
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import Header from './components/Header.vue'
import Menu from './components/Menu.vue'
import AddNewCustomer from './components/AddNewCustomer.vue'
import Home  from "./components/Home.vue";

// Default display of API data view

import {ref} from "vue";
const activeMenu = ref('home') // Defaults to "Home"

</script>