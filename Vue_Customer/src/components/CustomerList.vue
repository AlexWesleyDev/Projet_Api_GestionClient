<template>
  <div class="main">
    <div class="content">
      <div class="Searchbar">
        <SearchBar v-model="searchQuery"/>
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
        <tr v-for="customer in paginatedCustomers" :key="customer.id" >
          <td> {{ customer.name }}</td>
          <td> {{ customer.firstname }}</td>
          <td> {{ customer.email }}</td>
          <td> {{ customer.phonenumber }}</td>
          <td> {{ customer.adress }}</td>
          <td> {{ customer.city }}</td>
          <td> {{ customer.adresscode }}</td>
          <td> {{ formatDate(customer.datecreation) }}</td>
        </tr>
        </tbody>
      </table>
      </div>

      <!-- NO RESULT -->
      <div v-if="filteredCustomers.length === 0" class="NoClient" >
        Aucun client trouvé !
      </div>
    </div>
    <!-- Paging -->
    <div class="mt-6 flex justify-center gap-2 paging">
      <!-- PREVIOUS PAGE BUTTON -->
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

      <!-- NEXT PAGE BUTTON -->
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
import SearchBar from "./SearchBar.vue";

interface Customer {
  id: string
  name: string
  firstname: string
  email: string
  phonenumber: string
  adress: string
  city: string
  adresscode: string
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

// Function of search bar
const searchQuery = ref('')

const filteredCustomers = computed(() =>
    customers.value.filter(c =>
        c.name.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
        c.firstname.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
        c.email.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
        c.phonenumber.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
        c.city.toLowerCase().includes(searchQuery.value.toLowerCase())
    )
)

// Script of the date format displayed in the grid
const formatDate = (rawDate: string) => {
  const date = new Date(rawDate)
  return date.toLocaleDateString('fr-FR')
}

// Paging
const currentPage = ref(1)
const itemsPerPage = 13  // NUMBER OF LINES BY PAGES

const totalPages = computed(() => {
  return Math.ceil(filteredCustomers.value.length / itemsPerPage)
})

const paginatedCustomers = computed(() => {
  const start = (currentPage.value - 1) * itemsPerPage
  return filteredCustomers.value.slice(start, start + itemsPerPage)
})

// To return to page 1 and see the first results

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

.Searchbar {
  margin-bottom: 20px;
}

.content {
  padding: 20px;
}

/* No client found */
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

/* Table */

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

th, td {
  text-align: left;
  padding: 10px;
  border-bottom: 1px solid #ddd;
}

/* Pagination Part */

.paging{
  padding-inline:  200px;
}

.paging button {
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
