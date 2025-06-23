<style scoped>

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
  font-size: 13px;
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


input[type="checkbox"] {
  width: 16px;
  height: 16px;
  accent-color: #059669;
}

.NbLigneSelected{
  font-family:  "Century Gothic";
  color: #15803d;
  font-weight: 600;
  font-size:  20px;
  padding-top: 8px;
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

.btn-decocher{
  display: flex;
  gap:  10px;
}
.MargeActive-btn-decocher{
  margin-left: 270px;
}

.mainPrincipal{
  display: flex;
  background-color: white;
  width: 97%;
  height: 85vh;
  border-radius: 25px;
  margin-left: 22px;/*
    -webkit-box-shadow: -6px -4px 7px -2px rgba(0,0,0,0.15);
    box-shadow: -6px -4px 7px -2px rgba(0,0,0,0.15);*/

  -webkit-box-shadow:
      -6px 0px 7px -2px rgba(0,0,0,0.15),   /* gauche */
      6px 0px 7px -2px rgba(0,0,0,0.15),   /* droite */
      0px -6px 7px -2px rgba(0,0,0,0.15),  /* haut */
      0px 6px 7px -2px rgba(0,0,0,0.15);   /* bas */

  box-shadow:
      -6px 0px 7px -2px rgba(0,0,0,0.15),   /* gauche */
      6px 0px 7px -2px rgba(0,0,0,0.15),   /* droite */
      0px -6px 7px -2px rgba(0,0,0,0.15),  /* haut */
      0px 6px 7px -2px rgba(0,0,0,0.15);   /* bas */
}

.CustomerList{
  width: 100%;
  border-radius: 25px;
  background-color: white;
  margin: 10px;
  margin-bottom: 50px;
}

.afficherformulaireCustomer{
  width: 41%;
}

.TheadActionsDelUpdate{
  text-align: center;
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

.ligne-inactive {
  pointer-events: none;
  opacity: 0.5;
}

.ligne-active {
  background-color: lightblue !important;
  font-weight: 600;
}

.ligne-active-suppression{
  background-color: rgba(255, 0, 0, 0.35) !important;
  font-weight: 600;
 }

/* ON REND TOUS LES COMPOSANTS (Boutons, Input, ligne, etc...)
 INACTIFS ( PAS DE REACTIONS AU SURVOL ET AU CLICK LORS D'UNE MODIFICATION*/
.ActionsDenied {
  pointer-events: none;
  cursor: not-allowed;
}

/* AFFICHAGE POPUP DE CONFIRMATION*/
.overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: rgba(0, 0, 0, 0.4); /* Fond noir transparent */
  display: flex;
  justify-content: center;
}


</style>

<template>
  <div class="mainPrincipal">
  <div class="CustomerList">
    <div class="content">

      <div class="ZoneRechSupprModif" :class="{ ActionsDenied: formulaireActif }">
        <div>
          <BarreRecherche v-model="searchQuery" :disabled="formulaireActif"/>
        </div>

        <div>
          <BtnAjouter :disabled="formulaireActif"/>
        </div>

        <div class="btn-supprimer-modifier" v-if="selectedIds.length > 0">
          <!--Bouton Supprimer -->
         <BtnSupprMain :selectedIds="selectedIds" @confirmDelete="demanderSuppressionMultiple" :disabled="formulaireActif"/>

          <!--BOUTON DECOCHER-->
          <div class="btn-decocher" :class="{'MargeActive-btn-decocher': showPopupDelete}" >
            <BtnDecocher :selectedIds="selectedIds" @clearSelection="selectedIds = []" :disabled="formulaireActif" />
            <!-- DIV NOMBRE DE LIGNES SELECTIONNEES -->
            <div class="NbLigneSelected" >
              {{ selectedIds.length }} ligne(s) sélectionnée(s)
            </div>
          </div>
        </div>

      </div>


      <!-- Table de la liste de donées -->
      <div>
        <table>
          <thead>
          <tr>
            <th>
              <input type="checkbox" :checked="allPageSelected" @change="toggleSelectAllPage" :disabled="formulaireActif" />
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
                'selected': selectedIds.includes(client.id),
                'ligne-inactive': formulaireActif && client.id!=selectedId,
                'ActionsDenied': formulaireActif,
                'ligne-active' : formulaireActif && client.id==selectedId,
                'ligne-active-suppression': showPopupDelete && client.id == selectedId
              }"
              class="cursor-pointer" >
            <td>
              <input
                  type="checkbox"
                  :checked="selectedIds.includes(client.id)"
                  :disabled="formulaireActif && client.id === selectedId"
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
                  @modifier="lancerModification" />
              </div>

              <div> <BtnSupprLigne
                  :clientId="client.id" :disabled="formulaireActif"
                  @supprimerLigne="demanderSuppressionLigne"/>
              </div>
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
    <div class="mt-6 flex justify-center gap-2 pagination" :class="{ ActionsDenied: formulaireActif }">
      <!-- BOUTON PAGE PRECEDENTE -->
      <button
          :disabled="currentPage === 1 || formulaireActif"
          @click="currentPage--"
      >
        <svg xmlns="http://www.w3.org/2000/svg" width="15" viewBox="0 0 320 512"><!--!Font Awesome Free 6.7.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2025 Fonticons, Inc.--><path d="M41.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L109.3 256 246.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-160 160z"/></svg>
      </button>

      <div class="numeroPage">
        <button v-for="page in totalPages" :key="page" @click="currentPage = page"
                :class="['page-button', { active: page === currentPage }]" :disabled="formulaireActif"
        >
          {{ page }}
        </button>
      </div>

      <!-- BOUTON PAGE SUIVANTE -->
     <button
         :disabled="currentPage === totalPages || formulaireActif"
         @click="currentPage++"
     >
       <svg xmlns="http://www.w3.org/2000/svg" width="15" viewBox="0 0 320 512"><!--!Font Awesome Free 6.7.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2025 Fonticons, Inc.--><path d="M278.6 233.4c12.5 12.5 12.5 32.8 0 45.3l-160 160c-12.5 12.5-32.8 12.5-45.3 0s-12.5-32.8 0-45.3L210.7 256 73.4 118.6c-12.5-12.5-12.5-32.8 0-45.3s32.8-12.5 45.3 0l160 160z"/></svg>
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

    <!-- FOND ASSOMBRI + POPUP -->
    <div v-if="showPopupDelete" class="overlay">
      <ConfimDelete
          v-if="showPopupDelete"
          @annuler="annulerSuppression"
          @confirmer="validerSuppression"
          :message="messageSuppression"
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
const itemsPerPage = 9 // NOMBRE DE LIGNES PAR PAGES

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
  if (formulaireActif.value) {
    // Interdiction de modifier la sélection pendant une modif
    return
  }

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
}

// FENETRE POPUP DE CONFIRMATION DE SUPPRESSION APRES UNE MODIFICTAION

import ConfimDelete from "./ConfirmDelete.vue";

const showPopupDelete = ref(false)
const clientsASupprimer = ref<string[]>([])

const messageSuppression = ref('') // MESSAGE DE SUPPRESSSION

const demanderSuppressionLigne = (id: string) => {
  messageSuppression.value = "ce client"
  clientsASupprimer.value = [id]
  showPopupDelete.value = true
  selectedId.value = id       // identifie la ligne concernée
}

const demanderSuppressionMultiple = () => {
  if ( selectedIds.value.length === 0) return
  const nb = selectedIds.value.length
  messageSuppression.value = `${nb} client${nb > 1 ? 's' : ''}`
  clientsASupprimer.value = [...selectedIds.value]
  showPopupDelete.value = true
}

const validerSuppression = async () => {
  for (const id of clientsASupprimer.value) {
    await axios.delete(`http://localhost:5034/Customer/${id}`)
  }
  clientsASupprimer.value = []
  selectedIds.value = []
  selectedId.value = null
  formulaireActif.value = false
  await fetchCustomers()
  showPopupDelete.value = false
}

const annulerSuppression = () => {
  showPopupDelete.value = false
  selectedId.value = null
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