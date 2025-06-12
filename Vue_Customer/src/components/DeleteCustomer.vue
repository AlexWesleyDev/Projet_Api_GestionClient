<template>
  <div class="content">
    <div class="btn-supprimer">

      <input type="text" placeholder="Rechercher ..." class="barreRecherche" v-model="searchQuery" />

      <!--Bouton Supprimer -->
      <button type="button" class="button"  @click="supprimerSelection">
        <span class="button__text">Supprimer</span>
        <span class="button__icon"><svg xmlns="http://www.w3.org/2000/svg" width="512" viewBox="0 0 512 512" height="512" class="svg"><title></title><path style="fill:none;stroke:#323232;stroke-linecap:round;stroke-linejoin:round;stroke-width:32px" d="M112,112l20,320c.95,18.49,14.4,32,32,32H348c17.67,0,30.87-13.51,32-32l20-320"></path><line y2="112" y1="112" x2="432" x1="80" style="stroke:#323232;stroke-linecap:round;stroke-miterlimit:10;stroke-width:32px"></line><path style="fill:none;stroke:#323232;stroke-linecap:round;stroke-linejoin:round;stroke-width:32px" d="M192,112V72h0a23.93,23.93,0,0,1,24-24h80a23.93,23.93,0,0,1,24,24h0v40"></path><line y2="400" y1="176" x2="256" x1="256" style="fill:none;stroke:#323232;stroke-linecap:round;stroke-linejoin:round;stroke-width:32px"></line><line y2="400" y1="176" x2="192" x1="184" style="fill:none;stroke:#323232;stroke-linecap:round;stroke-linejoin:round;stroke-width:32px"></line><line y2="400" y1="176" x2="320" x1="328" style="fill:none;stroke:#323232;stroke-linecap:round;stroke-linejoin:round;stroke-width:32px"></line></svg></span>
      </button>

      <div class="NbLigneSelected" v-if="selectedIds.length > 0">
        {{ selectedIds.length }} ligne(s) sélectionnée(s)
      </div>

    </div>

    <table>
      <thead>
      <tr>
        <th>
          <input type="checkbox" :checked="allPageSelected" @change="toggleSelectAllPage"

          />
        </th>
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
      <tr v-for="client in paginatedCustomers" :key="client.id" @click="toggleSelection(client.id)"
          :class="selectedIds.includes(client.id) ? 'selected' : ''" class="cursor-pointer" >

        <td>
          <input type="checkbox" :value="client.id" v-model="selectedIds" @click.stop/>
        </td>

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

    <!-- Aucun résultat -->
    <div v-if="filteredCustomers.length === 0" class="NoClient" >
      Aucun client trouvé !
    </div>
  </div>
  <!-- Pagination -->
  <div class="mt-6 flex justify-center gap-2 pagination">
    <button v-for="page in totalPages" :key="page" @click="currentPage = page"
            :class="[ 'px-3 py-1 border rounded', page === currentPage ? 'bg-green-600 text-white' : 'bg-white hover:bg-gray-100' ]"
    >
      {{ page }}
    </button>
  </div>

</template>

<script setup lang="ts">
import {ref, onMounted, computed, watch} from 'vue'
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

// Partie Suppression de données sélectionnées
const selectedIds = ref<string[]>([])

const toggleSelection = (id: string) => {
  if (selectedIds.value.includes(id)) {
    selectedIds.value = selectedIds.value.filter(item => item !== id)
  } else {
    selectedIds.value.push(id)
  }
}

// Méthode de suppression
const supprimerSelection = async () => {
  if (selectedIds.value.length === 0) {
    alert("Aucun client sélectionné.")
    return
  }
  alert(selectedIds.value.length + " client(s) sélectionné(s) à supprimer.")

  if (confirm(`Vous confirmez la suppression de ${selectedIds.value.length} client(s) ?`)) {
    for (const id of selectedIds.value) {
      await axios.delete(`http://localhost:5034/Customer/${id}`)
    }
    selectedIds.value = []
    await fetchCustomers()
    alert("Suppression réussie.")
  }
}

// Selection A revoir  avec chatgpt










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

<style scoped>

.button {
  --main-focus: #2d8cf0;
  --font-color: #323232;
  --bg-color-sub: #dedede;
  --bg-color: #eee;
  --main-color: #323232;
  position: relative;
  width: 150px;
  height: 40px;
  cursor: pointer;
  display: flex;
  align-items: center;
  border: 2px solid var(--main-color);
  box-shadow: 4px 4px var(--main-color);
  background-color: var(--bg-color);
  border-radius: 10px;
  overflow: hidden;
  font-family: "Century Gothic";
  font-size: 14px;
}

.button, .button__icon, .button__text {
  transition: all 0.3s;
}

.button .button__text {
  transform: translateX(33px);
  color: var(--font-color);
  font-weight: 600;
}

.button .button__icon {
  position: absolute;
  transform: translateX(109px);
  height: 100%;
  width: 39px;
  background-color: var(--bg-color-sub);
  display: flex;
  align-items: center;
  justify-content: center;
}

.button .svg {
  width: 20px;
  fill: var(--main-color);
}

.button:hover {
  background: var(--bg-color);
}

.button:hover .button__text {
  color: transparent;
}

.button:hover .button__icon {
  width: 148px;
  transform: translateX(0);
}

.button:active {
  transform: translate(3px, 3px);
  box-shadow: 0px 0px var(--main-color);
}
</style>
