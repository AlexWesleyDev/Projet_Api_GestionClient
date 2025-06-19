<template>
  <div class="main">
    <div class="content">
      <div class="barreRecherche">
        <BarreRecherche v-model="searchQuery"/>
      </div>
      <div class="ListTableCustomer">
      <table>
        <thead>
        <tr>
          <th>NOM</th>
          <th>PRENOM</th>
          <th>EMAIL</th>
          <th>TELEPHONE</th>
          <th>ADRESSE</th>
          <th>VILLE</th>
          <th>CODE POSTAL</th>
          <th> DATE DE CREATION</th>
        </tr>
        </thead>
        <tbody id="printerTable">
        <tr v-for="client in paginatedCustomers" :key="client.id" >
          <td> {{ client.nom }} </td>
          <td> {{ client.prenom }}</td>
          <td> {{ client.email }}</td>
          <td> {{ client.telephone }}</td>
          <td> {{ client.adresse }}</td>
          <td> {{ client.ville }}</td>
          <td> {{ client.codepostal }}</td>
          <td> {{ formatDate(client.datecreation) }}</td>
        </tr>
        </tbody>
      </table>
      </div>

      <!-- Aucun résultat -->
      <div v-if="filteredCustomers.length === 0" class="NoClient" >
        Aucun client trouvé !
      </div>
    </div>
    <!-- Pagination -->
    <div class="mt-6 flex justify-center gap-2 pagination">
      <!-- BOUTON PAGE PRECEDENTE -->
      <button
          :disabled="currentPage === 1"
          @click="currentPage--"
      >
        <svg xmlns="http://www.w3.org/2000/svg" width="15" viewBox="0 0 320 512"><!--!Font Awesome Free 6.7.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2025 Fonticons, Inc.--><path d="M41.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L109.3 256 246.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-160 160z"/></svg>
      </button>

      <button v-for="page in totalPages" :key="page" @click="currentPage = page"
              :class="['page-button', { active: page === currentPage }]"
      >
        {{ page }}
      </button>

      <!-- BOUTON PAGE SUIVANTE -->
      <button
          :disabled="currentPage === totalPages"
          @click="currentPage++"
      >
        <svg xmlns="http://www.w3.org/2000/svg" width="15" viewBox="0 0 320 512"><!--!Font Awesome Free 6.7.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2025 Fonticons, Inc.--><path d="M278.6 233.4c12.5 12.5 12.5 32.8 0 45.3l-160 160c-12.5 12.5-32.8 12.5-45.3 0s-12.5-32.8 0-45.3L210.7 256 73.4 118.6c-12.5-12.5-12.5-32.8 0-45.3s32.8-12.5 45.3 0l160 160z"/></svg>
      </button>
    </div>
  </div>

</template>

<script setup lang="ts">
import {ref, onMounted, computed, watch} from 'vue'
import axios from 'axios'
import BarreRecherche from "./BarreRecherche.vue";

interface Customer {
  id: string
  nom: string
  prenom: string
  email: string
  telephone: string
  adresse: string
  ville: string
  codepostal: string
  datecreation: string
}

const customers = ref<Customer[]>([])

const fetchCustomers = async () => {
  try {
    const res = await axios.get('http://localhost:5034/Customer')
    customers.value = res.data
  } catch (err) {
    console.error('Erreur chargement clients :', err)
  }
}

// Gestion de la barre de recherche
const searchQuery = ref('')

const filteredCustomers = computed(() =>
    customers.value.filter(c =>
        c.nom.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
        c.prenom.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
        c.email.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
        c.telephone.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
        c.ville.toLowerCase().includes(searchQuery.value.toLowerCase())
    )
)

// Script de format de la date affichée dans la grille
const formatDate = (rawDate: string) => {
  const date = new Date(rawDate)
  return date.toLocaleDateString('fr-FR')
}

// Pagination
const currentPage = ref(1)
const itemsPerPage = 13 // NOMBRE DE LIGNES PAR PAGES

const totalPages = computed(() => {
  return Math.ceil(filteredCustomers.value.length / itemsPerPage)
})

const paginatedCustomers = computed(() => {
  const start = (currentPage.value - 1) * itemsPerPage
  return filteredCustomers.value.slice(start, start + itemsPerPage)
})

// Afin de revenir à la page 1 pour voir les premiers résultats

watch(searchQuery, () => {
  currentPage.value = 1
})

onMounted(fetchCustomers)
</script>

<style scoped>

.main {
  flex: 1;
  width: 100%;
}

.barreRecherche{
  margin-bottom: 20px;
}

.content {
  padding: 20px;
}

/* Aucun résultat*/
.NoClient{
  display: flex;
  font-weight: bold;
  justify-content: center;
  align-items: center;
  width: 200px;
  height: 40px;
  font-family: "Century Gothic";
  border:  1px solid black;
  margin-top: 20px;
  text-align: center;
  border-radius: 30px;
  border: 4px solid orange;
  background-color: khaki;
}

/* Tableau */

table {
  width: 100%;
  border-collapse: collapse;
}

thead{
  background-color: #edf2f9;
  font-size: 14px;
}

th{
  color: midnightblue;
}

td{
  font-family: "Century Gothic";
}

tr {
  transition: background-color 0.2s ease;
  font-size: 12px;
}

tbody tr:hover{
  background-color: lightblue;
}

tr.selected {
  background-color: #d1fae5; /* Vert très clair */
  font-weight: 600;
}

th, td {
  text-align: left;
  padding: 10px;
  border-bottom: 1px solid #ddd;
}

/* Partie Pagination */

.pagination{
  padding-inline:  200px;
}

.pagination button {
  background-color: white;
  color: black;
  margin-right: 15px;
  cursor: pointer;
  font-size: 12px;
  width: 35px;
  height: 35px;
  opacity: 0.75;
  border: 1px solid #e7eae8;
  border-radius: 8px;
  transition: background-color 0.2s ease;
}

.page-button:hover {
  background-color: #f0f0f0;
}

.page-button.active {
  border-color: #2569c3;
  color: #2569c3;
}
</style>
