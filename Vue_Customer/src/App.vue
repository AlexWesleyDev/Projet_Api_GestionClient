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
        <CustomerList v-if="activeMenu === 'voir'" />
        <CustomerList v-if="activeMenu === 'ajouter'" />
        <CustomerList v-if="activeMenu === 'modifier'" />
        <CustomerList v-if="activeMenu === 'supprimer'" />


      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import CustomerList from './components/CustomerList.vue'
import Header from './components/Header.vue'
import Menu from './components/Menu.vue'
import {ref} from "vue";
// import AddCustomer from './components/AddCustomer.vue' //

// Affichage par défaut des données de l'API
const activeMenu = ref('voir') // par défaut sur "VOIR TOUT"

</script>
