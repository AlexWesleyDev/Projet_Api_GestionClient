<template>
  <div class="content">
    <input type="text" placeholder="Rechercher ..." class="barreRecherche" v-model="searchQuery" />


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
      <tr v-for="client in filteredCustomers" :key="client.id" >
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
</template>

<script setup lang="ts">
import {ref, onMounted, computed} from 'vue'
import axios from 'axios'

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

// LES ACTIONS CRUD

/*
const deleteClient = async (id: string) => {
  if (confirm('Supprimer ce client ?')) {
    await axios.delete(`http://localhost:5034/Customer/${id}`)
    await fetchCustomers()
  }
}

const editClient = (client: Customer) => {
  alert(`Modification en cours pour : ${client.nom} ${client.prenom}`)
}
 */


onMounted(fetchCustomers)
</script>
