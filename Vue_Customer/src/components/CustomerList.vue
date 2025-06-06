<template>
  <div class="flex min-h-screen">
    <!-- Sidebar -->
    <aside class="w-60 bg-gray-800 text-white py-6 px-4 michel bg-toto" >
      <h2 class="text-2xl font-bold text-center mb-6">Findis Customer</h2>
      <ul class="space-y-4">
        <li
            v-for="item in menuItems"
            :key="item.label"
            :class="['px-4 py-2 rounded cursor-pointer', activeMenu === item.action ? 'bg-gray-700' : 'hover:bg-gray-700']"
            @click="activeMenu = item.action"
        >
          {{ item.label }}
        </li>
      </ul>
    </aside>

    <!-- Main Content -->
    <div class="flex-1">
      <!-- Header -->
      <header class="bg-green-600 text-white px-6 py-4 flex justify-between items-center">
        <span class="text-lg font-semibold">Gestion des clients</span>
        <span class="font-medium">wesleysabine09</span>
      </header>

      <!-- Main -->
      <main class="p-6">
        <!-- Barre de recherche -->
        <div class="flex justify-between items-center mb-6">
          <input
              type="text"
              v-model="searchQuery"
              placeholder="Recherche rapide..."
              class="w-80 px-4 py-2 border rounded shadow-sm"
          />
        </div>

        <!-- Liste des clients -->
        <div class="space-y-4">
          <div
              v-for="client in filteredCustomers"
              :key="client.id"
              class="bg-white border rounded-xl p-4 shadow hover:shadow-md transition"
          >
            <div class="grid grid-cols-2 gap-2 text-sm text-gray-700">
              <p><strong>Nom :</strong> {{ client.nom }}</p>
              <p><strong>Prénom :</strong> {{ client.prenom }}</p>
              <p><strong>Email :</strong> {{ client.email }}</p>
              <p><strong>Téléphone :</strong> {{ client.telephone }}</p>
              <p><strong>Adresse :</strong> {{ client.adresse }}</p>
              <p><strong>Ville :</strong> {{ client.ville }}</p>
              <p><strong>Code postal :</strong> {{ client.codepostal }}</p>
              <p><strong>Créé le :</strong> {{ formatDate(client.datecreation) }}</p>
            </div>

            <div class="mt-4 flex gap-4">
              <button
                  v-if="activeMenu === 'edit'"
                  @click="editClient(client)"
                  class="bg-blue-600 hover:bg-blue-700 text-white px-4 py-1 rounded"
              >
                ✏️ Modifier
              </button>
              <button
                  v-if="activeMenu === 'delete'"
                  @click="deleteClient(client.id)"
                  class="bg-red-600 hover:bg-red-700 text-white px-4 py-1 rounded"
              >
                🗑️ Supprimer
              </button>
              <button
                  v-if="activeMenu === 'add'"
                  @click="addClient(client)"
                  class="bg-green-600 hover:bg-green-700 text-white px-4 py-1 rounded"
              >
                ✅ Ajouter (fictif)
              </button>
            </div>
          </div>
        </div>
      </main>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
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
const searchQuery = ref('')
const activeMenu = ref<'view' | 'add' | 'edit' | 'delete'>('view')

const menuItems = [
  { label: 'Voir', action: 'view' },
  { label: 'Ajouter', action: 'add' },
  { label: 'Modifier', action: 'edit' },
  { label: 'Supprimer', action: 'delete' },
]

const fetchCustomers = async () => {
  try {
    const res = await axios.get('http://localhost:5034/Customer')
    customers.value = res.data
  } catch (err) {
    console.error('Erreur chargement clients :', err)
  }
}

const deleteClient = async (id: string) => {
  if (confirm('Supprimer ce client ?')) {
    await axios.delete(`http://localhost:5034/Customer/${id}`)
    await fetchCustomers()
  }
}

const editClient = (client: Customer) => {
  alert(`Modification en cours pour : ${client.nom} ${client.prenom}`)
}

const addClient = (client: Customer) => {
  alert(`Ajout fictif du client : ${client.nom} ${client.prenom}`)
}

const formatDate = (rawDate: string) => {
  const date = new Date(rawDate)
  return date.toLocaleDateString('fr-FR')
}

const filteredCustomers = computed(() => {
  const q = searchQuery.value.toLowerCase()
  return customers.value.filter(c =>
      c.nom.toLowerCase().includes(q) ||
      c.prenom.toLowerCase().includes(q) ||
      c.email.toLowerCase().includes(q)
  )
})

onMounted(fetchCustomers)
</script>