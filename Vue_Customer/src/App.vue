<template>
  <div class="flex h-screen">
    <!-- Menu latéral (Sidebar) -->
    <Menu :activeMenu="activeMenu" @update:activeMenu="activeMenu = $event" />


    <!-- Main Principal -->
    <div class="flex flex-col flex-1">
      <!-- Header -->
      <Header />

      <!-- Contenu des informations -->
      <div class="p-4 overflow-y-auto">
        <Accueil v-if="activeMenu === 'accueil'" />
        <AddCustomer v-if="activeMenu === 'ajouter'" />
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import Header from './components/Header.vue'
import Menu from './components/Menu.vue'
import AddCustomer from './components/AddCustomer.vue'
import Accueil  from "./components/Accueil.vue";

// Affichage par défaut des données de l'API
import {ref} from "vue";
const activeMenu = ref('accueil') // par défaut sur "VOIR TOUT"

</script>
