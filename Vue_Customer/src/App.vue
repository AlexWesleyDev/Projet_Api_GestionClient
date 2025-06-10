<template>
  <div class="flex h-screen">
    <!-- Sidebar -->
    <Menu />

    <!-- Main Content Wrapper -->
    <div class="flex flex-col flex-1">
      <!-- Header -->
      <Header />

      <!-- Main Content -->
      <div class="p-4 overflow-y-auto">
        <CustomerList />
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import CustomerList from './components/CustomerList.vue'
import Header from './components/Header.vue'
import Menu from './components/Menu.vue'
</script>
