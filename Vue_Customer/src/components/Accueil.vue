<template>
  <div>
    <div class="content">

      <div class="ZoneRechSupprModif">
        <div>
          <input type="text" placeholder="Rechercher ..." class="barreRecherche" v-model="searchQuery" />
        </div>
        <!-- Element 2-->

        <div class="btn-supprimer-modifier" v-if="selectedIds.length > 0">
          <!--Bouton Supprimer -->
         <BtnSupprMain :selectedIds="selectedIds" @confirmDelete="supprimerClientsSelected"/>

          <!-- DIV NOMBRE DE LIGNES SELECTIONNEES -->
          <div class="NbLigneSelected" >
            {{ selectedIds.length }} ligne(s) sélectionnée(s)
          </div>

          <!--BOUTON DESELECTIONNER -->
          <div class="btn-decocher" >
            <BtnDecocher :selectedIds="selectedIds" @clearSelection="selectedIds = []" />
          </div>
        </div>

      </div>


      <!-- Element 3 -->
      <div class="CustomerList">
        <table>
          <thead>
          <tr>
            <th>
              <input type="checkbox" :checked="allPageSelected" @change="toggleSelectAllPage" />
            </th>
            <th>NOM</th>
            <th>PRENOM</th>
            <th>EMAIL</th>
            <th>TELEPHONE</th>
            <th>ADRESSE</th>
            <th>VILLE</th>
            <th>CODE POSTAL</th>
            <th> DATE DE CREATION</th>
            <th class="TheadActionsDelUpdate">ACTIONS</th>
          </tr>
          </thead>
          <tbody id="printerTable">
          <tr v-for="client in paginatedCustomers" :key="client.id"
              @click="toggleSelection(client.id)"
              :class="{
                'selected': selectedIds.includes(client.id)
              }"
              class="cursor-pointer"
          >
            <td>
              <input
                  type="checkbox"
                  :checked="selectedIds.includes(client.id)"
                  @click.stop="toggleSelection(client.id)"
              />
            </td>
            <td> {{ client.nom }}</td>
            <td> {{ client.prenom }}</td>
            <td> {{ client.email }}</td>
            <td> {{ client.telephone }}</td>
            <td> {{ client.adresse }}</td>
            <td> {{ client.ville }}</td>
            <td> {{ client.codepostal }}</td>
            <td> {{ formatDate(client.datecreation) }}</td>
            <td class="actionsUpdateSupprLigne">
              <div> <BtnModifLigne
                  :client="client"
                  :disabled="formulaireActif"
                  @modifier="lancerModification"
              />
              </div>
              <div> <BtnSupprLigne :clientId="client.id" @supprimer="supprimerLigne"/>  </div>
            </td>
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
      <button v-for="page in totalPages" :key="page" @click="currentPage = page"
              :class="[ 'px-3 py-1 border rounded', page === currentPage ? 'bg-green-600 text-white' : 'bg-white hover:bg-gray-100' ]"
      >
        {{ page }}
      </button>
    </div>
  </div>

  <!-- PARTIE FORMUALIRE DE MODIFICATION -->

  <div class="afficherformulaireCustomer">
    <FormModifClient
        :form="form"
        @valider="validerModification"
        @annuler="annulerModification"
    />
  </div>

</template>

<script setup lang="ts">
/* IMPORTS DES BOUTONS D'ACTIONS*/
import BtnSupprMain from "./BtnSupprMain.vue";
import BtnSupprLigne from "./BtnSupprLigne.vue";
import BtnDecocher from "./BtnDecocher.vue";
import BtnModifLigne from "./BtnModifLigne.vue";

/* IMPORTS DU FORMULAIRE DE MODIFICATION*/
import FormModifClient from './FormModifClient.vue'


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

// CHARGEMENT DES DONNEES DANS LA TABLE
const fetchCustomers = async () => {
  try {
    const res = await axios.get('http://localhost:5034/Customer')
    customers.value = res.data
  } catch (err) {
    console.error('Erreur chargement clients :', err)
  }
}

onMounted(fetchCustomers)

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


// PARTIE GESTION DE SELECTION DE DONNEES

// 1 - SELECTION MULTIPLE
// SELECTION EN PLUSIEURS
const selectedIds = ref<string[]>([])

const toggleSelection = (id: string) => {
  if (selectedIds.value.includes(id)) {
    selectedIds.value = selectedIds.value.filter(item => item !== id)
  } else {
    selectedIds.value.push(id)
  }
}

// TOUTES PAGES SELECTIONNEES
const allPageSelected = computed(() => {
  return paginatedCustomers.value.every(c => selectedIds.value.includes(c.id));
});

const toggleSelectAllPage = () => {
  const idsThisPage = paginatedCustomers.value.map(c => c.id);

  if (allPageSelected.value) {
    // Supprime uniquement ceux de la page actuelle
    selectedIds.value = selectedIds.value.filter(id => !idsThisPage.includes(id));
  } else {
    // Ajoute ceux qui ne sont pas encore dans la sélection
    idsThisPage.forEach(id => {
      if (!selectedIds.value.includes(id)) {
        selectedIds.value.push(id);
      }
    });
  }
};

// GESTION FORMULAIRE : AFFICHAGE, SOUMMISSION D'ENVOI ET CONTROLE D'ACTION LORS DE MODIFICATION

const formulaireActif = ref(false)
const selectedId = ref<string | null>(null)

const form = ref<Customer>({
  id: '',
  nom: '',
  prenom: '',
  email: '',
  telephone: '',
  adresse: '',
  ville: '',
  codepostal: '',
  datecreation: ''
})

const lancerModification = (client: Customer) => {
  formulaireActif.value = true
  selectedId.value = client.id

  form.value = {
    ...client // copie toutes les infos dans le formulaire
  }

  // Optionnel : sélectionne visuellement la ligne
  selectedIds.value = [client.id]
}






// METHODE DE SUPPRESSION
// SUPPRESSION SIMPLE D'UNE LIGNE
const supprimerLigne = async (id: string) => {
  try {
    await axios.delete(`http://localhost:5034/Customer/${id}`)
    customers.value = customers.value.filter(c => c.id !== id)
    alert("Client supprimé avec succès.")
  } catch (err) {
    console.error("Erreur suppression :", err)
    alert("Échec de la suppression.")
  }
}

// SUPPRESSION DE PLUSIEURS LIGNES
const supprimerClientsSelected = async () => {
  if (selectedIds.value.length === 0) {
    alert("Aucun client sélectionné.")
    return
  }
  if (confirm(`Vous confirmez la suppression de ${selectedIds.value.length} client(s) ?`)) {
    for (const id of selectedIds.value) {
      await axios.delete(`http://localhost:5034/Customer/${id}`)
    }
    selectedIds.value = []
    await fetchCustomers()
  }
}

</script>

<style scoped>

.afficherformulaireCustomer{
  width: 35vw;
  height: 800px;
  margin-left: 10px;
  margin-top: 100px;
  margin-right: 50px;
}

/* STYLE DU CONTENEUR DE LA TABLE DE LISTE DES DONNEES */
.ConteneurList{
  width: 100%;
}

.TheadActionsDelUpdate{
  text-align: center;
  width: 161px;
}

.actionsUpdateSupprLigne{
  display:  flex;
  justify-content: space-around;
}

.ZoneRechSupprModif {
  display: flex;
  gap: 30px ;
  margin-bottom: 25px;
}

.btn-supprimer-modifier{
  display: flex;
  gap:  30px ;
}

</style>
