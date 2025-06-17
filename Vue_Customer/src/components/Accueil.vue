<style scoped>

.btn-decocher{
  display: flex;
  gap:  10px;
}

.mainPrincipal{
  display: flex;
  justify-content: flex-start;
  background-color: white;
  width: 98%;
  border-radius: 25px;
  -webkit-box-shadow: -6px -4px 7px -2px rgba(0,0,0,0.15);
  box-shadow: -6px -4px 7px -2px rgba(0,0,0,0.15);
}

.CustomerList{
  width: 100%;
  border-radius: 25px;
  background-color: white;
  margin: 10px;
  margin-bottom: 50px;
}

.afficherformulaireCustomer{
  width: 27%;
  height: 800px;
  margin-left: 10px;
  margin-right: 50px;
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

<template>
  <div class="mainPrincipal">
  <div class="CustomerList">
    <div class="content">

      <div class="ZoneRechSupprModif">
        <div>
          <BarreRecherche v-model="searchQuery"/>
        </div>

        <div>
          <BtnAjouter/>
        </div>

        <div class="btn-supprimer-modifier" v-if="selectedIds.length > 0">
          <!--Bouton Supprimer -->
         <BtnSupprMain :selectedIds="selectedIds" @confirmDelete="supprimerClientsSelected"/>

          <!--BOUTON DESELECTIONNER -->
          <div class="btn-decocher" >
            <BtnDecocher :selectedIds="selectedIds" @clearSelection="selectedIds = []" />
            <!-- DIV NOMBRE DE LIGNES SELECTIONNEES -->
            <div class="NbLigneSelected" >
              {{ selectedIds.length }} ligne(s) sélectionnée(s)
            </div>
          </div>
        </div>

      </div>


      <!-- Element 3 -->
      <div>
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
      <!-- BOUTON PAGE PRECEDENTE -->
      <button
          :disabled="currentPage === 1"
          @click="currentPage--"
      >
        <svg xmlns="http://www.w3.org/2000/svg" width="25" viewBox="0 0 320 512"><!--!Font Awesome Free 6.7.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2025 Fonticons, Inc.--><path d="M41.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L109.3 256 246.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-160 160z"/></svg>
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
       <svg xmlns="http://www.w3.org/2000/svg" width="25" viewBox="0 0 320 512"><!--!Font Awesome Free 6.7.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2025 Fonticons, Inc.--><path d="M278.6 233.4c12.5 12.5 12.5 32.8 0 45.3l-160 160c-12.5 12.5-32.8 12.5-45.3 0s-12.5-32.8 0-45.3L210.7 256 73.4 118.6c-12.5-12.5-12.5-32.8 0-45.3s32.8-12.5 45.3 0l160 160z"/></svg>
     </button>

    </div>

  </div>

  <!-- PARTIE FORMUALIRE DE MODIFICATION -->

  <div class="afficherformulaireCustomer" v-if="formulaireActif">
    <FormModifClient
        v-if="formulaireActif"
        :form="form"
        @valider="validerModification"
        @annuler="annulerModification"
    />
  </div>

  </div>

</template>

<script setup lang="ts">
import {ref, onMounted, computed, watch} from 'vue'
import axios from 'axios'


import BarreRecherche from "./BarreRecherche.vue";

/* IMPORTS DES BOUTONS D'ACTIONS*/
import BtnSupprMain from "./BtnSupprMain.vue";
import BtnSupprLigne from "./BtnSupprLigne.vue";
import BtnDecocher from "./BtnDecocher.vue";
import BtnModifLigne from "./BtnModifLigne.vue";
import BtnAjouter from "./BtnAjouter.vue";

/* IMPORTS DU FORMULAIRE DE MODIFICATION*/
import FormModifClient from './FormModifClient.vue'

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

// METHODE DE SUPPRESSION
// SUPPRESSION SIMPLE D'UNE LIGNE
const supprimerLigne = async (id: string) => {
  try {
    await axios.delete(`http://localhost:5034/Customer/${id}`)
    customers.value = customers.value.filter(c => c.id !== id)
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
  form.value = { ...client }
}

/* Avec ATTENTE DE VALIDATION
const annulerModification = () => {
  const confirmation = confirm("Annuler la modification en cours ?")
  if (confirmation) {
    formulaireActif.value = false
    selectedId.value = null
  }
}*/

const annulerModification = () => {
  formulaireActif.value = false
  selectedId.value = null
}

const validerModification = async () => {
  if (!selectedId.value) return

  try {
    await axios.put(`http://localhost:5034/Customer/${selectedId.value}`, form.value)

    formulaireActif.value = false
    selectedId.value = null
    await fetchCustomers()
  } catch (error) {
    console.error("Erreur lors de la mise à jour :", error)
    alert("Une erreur est survenue lors de la modification.")
  }
}

</script>