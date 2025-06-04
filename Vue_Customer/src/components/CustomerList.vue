<template>
  <div class="p-6 max-w-4xl mx-auto">
    <h1 class="text-2xl font-bold mb-4">Liste des clients</h1>
    <br>

    <div v-for="client in customers" :key="client.id" class="bg-white p-4 rounded shadow mb-4">
      <p><strong>Nom :</strong> {{ client.nom }}</p>
      <p><strong>Prénom :</strong> {{ client.prenom }}</p>
      <p><strong>Email :</strong> {{ client.email }}</p>
      <div class="mt-2 flex gap-2">
        <button class="bg-blue-500 text-white px-4 py-1 rounded hover:bg-blue-600" @click="editClient(client)">
          Modifier
        </button>

        <button class="bg-red-500 text-white px-4 py-1 rounded hover:bg-red-600"  @click="deleteClient(client.id)" >
          Supprimer
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import axios from 'axios'

interface Customer {
  id: string
  nom: string
  prenom: string
  email: string
  adresse: string
  ville: string
  codepostal: string
  datecreation: string
}

const customers = ref<Customer[]>([])

const fetchCustomers = async () => {
  const res = await axios.get('http://localhost:5034/Customer')
  customers.value = res.data
}

const deleteClient = async (id: string) => {
  if (confirm('Supprimer ce client ?')) {
    await axios.delete(`http://localhost:5034/Customer/${id}`)
    fetchCustomers()
  }
}

const editClient = (client: Customer) => {
  alert(`Fonction de modification en cours (ID : ${client.id})`)
  // Tu pourras appeler un autre composant ou remplir un formulaire ici
}

onMounted(fetchCustomers)
</script>











<!--
<template>
  <div>
    <h2>Liste des clients</h2>
    <ul v-if="customers.length > 0">
      <li v-for="client in customers" :key="client.id">
        <strong>{{ client.nom }} {{ client.prenom }}</strong> – {{ client.email }} – {{ client.ville }} ({{ client.datecreation }})
      </li>
    </ul>
    <p v-else>Aucun client trouvé.</p>
  </div>
</template>


<script lang="ts" setup>
import api from '../api';
import { ref, onMounted } from 'vue'

interface Customer {
  id: string;
  nom: string;
  prenom: string;
  email: string;
  adresse: string;
  ville: string;
  codepostal: string;
  datecreation: string;
}


const customers = ref<Customer[]>([])

onMounted(async () => {
  try {
    const response = await api.get('/customer')
    customers.value = response.data
  } catch (error) {
    console.error('Erreur lors du chargement des clients :', error)
  }
})

</script>

<style scoped>
h2 {
  color: #3b82f6;
}
ul {
  list-style: none;
  padding: 0;
}
li {
  margin-bottom: 0.5rem;
  border-bottom: 1px solid #ddd;
  padding-bottom: 0.5rem;
}
</style>

-->