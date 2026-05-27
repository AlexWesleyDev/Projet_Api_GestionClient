<style scoped>

.content {
  padding: 20px;
}
/* No result */
.NoCustomer{
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

.NbLineSelected{
  font-family:  "Century Gothic";
  color: #15803d;
  font-weight: 600;
  font-size:  20px;
  padding-top: 8px;
}

/* Paging part*/

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

.btn-uncheck{
  display: flex;
  gap:  10px;
}
.MargeActive-btn-uncheck{
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

.showformCustomer{
  width: 45%;
}

.TheadActionsDelUpdate{
  text-align: center;
}

.actionsUpdateSupprLine{
  display:  flex;
  justify-content: space-around;
}

.AreaSearchDelModify {
  display: flex;
  gap: 30px ;
  margin-bottom: 25px;
}

.btn-delete-modify{
  display: flex;
  gap:  30px ;
}

.line-inactive {
  pointer-events: none;
  opacity: 0.5;
}

.line-active {
  background-color: lightblue !important;
  font-weight: 600;
}

.line-active-to-delete{
  background-color: rgba(255, 0, 0, 0.35) !important;
  font-weight: 600;
 }

/* MAKE ALL COMPONENTS INACTIVES (Buttons, Input, lines, etc...) ( NO ACTION during an editing (hover or click) */

.ActionsDenied {
  pointer-events: none;
  cursor: not-allowed;
}

/* DISPLAY CONFIRMATION POPUP */
.overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: rgba(0, 0, 0, 0.4);
  display: flex;
  justify-content: center;
}


</style>

<template>
  <div class="mainPrincipal">
  <div class="CustomerList">
    <div class="content">

      <div class="AreaSearchDelModify" :class="{ ActionsDenied: formActive }">
        <div>
          <SearchBar v-model="searchQuery" :disabled="formActive"/>
        </div>

        <div>
          <BtnAddCustomer :disabled="formActive"/>
        </div>

        <div class="btn-delete-modify" v-if="selectedIds.length > 0">
          <!-- BUTTON DELETE -->
         <BtnDeleteMain :selectedIds="selectedIds" @confirmDelete="requestDeletionLineMultiple" :disabled="formActive"/>

          <!-- BUTTON UNCHECK -->
          <div class="btn-uncheck" :class="{'MargeActive-btn-uncheck': showPopupDelete}" >
            <BtnUncheck :selectedIds="selectedIds" @clearSelection="selectedIds = []" :disabled="formActive" />
            <!-- DIV OF SELECTED LINES NUMBER -->
            <div class="NbLineSelected" >
              {{ selectedIds.length }} ligne(s) sélectionnée(s)
            </div>
          </div>
        </div>

      </div>


      <!-- DATA LIST TABLE -->
      <div>
        <table>
          <thead>
          <tr>
            <th>
              <input type="checkbox" :checked="allPageSelected" @change="toggleSelectAllPage" :disabled="formActive" />
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
          <tr v-for="customerRegistred in paginatedCustomers" :key="customerRegistred.id"
              @click="toggleSelection(customerRegistred.id)"
              :class="{
                'selected': selectedIds.includes(customerRegistred.id),
                'line-inactive': formActive && customerRegistred.id!=selectedId,
                'ActionsDenied': formActive,
                'line-active' : formActive && customerRegistred.id==selectedId,
                'line-active-to-delete': showPopupDelete && customerRegistred.id == selectedId
              }"
              class="cursor-pointer" >
            <td>
              <input
                  type="checkbox"
                  :checked="selectedIds.includes(customerRegistred.id)"
                  :disabled="formActive && customerRegistred.id === selectedId"
                  @click.stop="toggleSelection(customerRegistred.id)"
              />
            </td>
            <td> {{ customerRegistred.name }}</td>
            <td> {{ customerRegistred.firstname }}</td>
            <td> {{ customerRegistred.email }}</td>
            <td> {{ customerRegistred.phonenumber }}</td>
            <td> {{ customerRegistred.adress }}</td>
            <td> {{ customerRegistred.city }}</td>
            <td> {{ customerRegistred.adresscode }}</td>
            <td> {{ formatDate(customerRegistred.datecreation) }}</td>
            <td class="actionsUpdateSupprLine">
              <div> <BtnEditLine
                  :customer="customerRegistred"
                  :disabled="formActive"
                  @modify="startModification" />
              </div>

              <div> <BtnDeleteLine
                  :customer-id="customerRegistred.id" :disabled="formActive"
                  @deleteLine="requestDeletionLine"/>
              </div>
            </td>
          </tr>
          </tbody>
        </table>
      </div>

      <!-- NO RESULT -->
      <div v-if="filteredCustomers.length === 0" class="NoCustomer" >
        Aucun client trouvé !
      </div>

    </div>
    <!-- PAGING -->
    <div class="mt-6 flex justify-center gap-2 paging" :class="{ ActionsDenied: formActive }">
      <!-- PREVIOUS PAGE BUTTON -->
      <button
          :disabled="currentPage === 1 || formActive"
          @click="currentPage--"
      >
        <svg xmlns="http://www.w3.org/2000/svg" width="15" viewBox="0 0 320 512"><!--!Font Awesome Free 6.7.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2025 Fonticons, Inc.--><path d="M41.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L109.3 256 246.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-160 160z"/></svg>
      </button>

      <div>
        <button v-for="page in totalPages" :key="page" @click="currentPage = page"
                :class="['page-button', { active: page === currentPage }]" :disabled="formActive"
        >
          {{ page }}
        </button>
      </div>

      <!-- NEXT PAGE BUTTON -->
     <button
         :disabled="currentPage === totalPages || formActive"
         @click="currentPage++"
     >
       <svg xmlns="http://www.w3.org/2000/svg" width="15" viewBox="0 0 320 512"><!--!Font Awesome Free 6.7.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2025 Fonticons, Inc.--><path d="M278.6 233.4c12.5 12.5 12.5 32.8 0 45.3l-160 160c-12.5 12.5-32.8 12.5-45.3 0s-12.5-32.8 0-45.3L210.7 256 73.4 118.6c-12.5-12.5-12.5-32.8 0-45.3s32.8-12.5 45.3 0l160 160z"/></svg>
     </button>
    </div>

  </div>

  <!-- FOM PART OF UPDATING CUSTOMER -->
  <div class="showformCustomer" v-if="formActive">
    <FormEditCustomer
        v-if="formActive"
        :form="form"
        @confirm="validateModification"
        @cancel="cancelModification"
    />
  </div>

    <!-- Dark Backround + POPUP -->
    <div v-if="showPopupDelete" class="overlay">
      <PopupConfirmDelete
          v-if="showPopupDelete"
          @cancel="cancelDeleting"
          @confirm="validateDeletion"
          :message="deletionMessage"
      />
    </div>

  </div>
</template>

<script setup lang="ts">
import {ref, onMounted, computed, watch} from 'vue'
import axios from 'axios'
import SearchBar from "./SearchBar.vue";

/* ACTIONS BUTTONS IMPORTS */
import BtnDeleteMain from "./BtnDeleteMain.vue";
import BtnDeleteLine from "./BtnDeleteLine.vue";
import BtnUncheck from "./BtnUncheck.vue";
import BtnEditLine from "./BtnEditLine.vue";
import BtnAddCustomer from "./BtnAddCustomer.vue";

/* EDI FORM IMPORT */
import FormEditCustomer from './FormEditCustomer.vue'

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

// LOADING OF DATA IN THE TABLE
const fetchCustomers = async () => {
  try {
    const res = await axios.get('http://localhost:5034/Customer')
    customers.value = res.data
  } catch (err) {
    console.error('Erreur chargement clients :', err)
  }
}

onMounted(fetchCustomers)

// PART OF SEARCH BAR
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
const itemsPerPage = 9 // NUMBER OF LINES BY PAGES

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

// FUNCTION PART OF DATA LINES SELECTION

// 1 - MULTIPLE SELECTION
const selectedIds = ref<string[]>([])

const toggleSelection = (id: string) => {
  if (formActive.value) // We cannot change the selection while editing
    return

  if (selectedIds.value.includes(id))
    selectedIds.value = selectedIds.value.filter(item => item !== id)
  else
    selectedIds.value.push(id)
}

// * ALL SELECTED PAGES
const allPageSelected = computed(() => {
  return paginatedCustomers.value.every(c => selectedIds.value.includes(c.id));
});

const toggleSelectAllPage = () => {
  const idsThisPage = paginatedCustomers.value.map(c => c.id);

  if (allPageSelected.value) {
    // Delete only the lines of current page
    selectedIds.value = selectedIds.value.filter(id => !idsThisPage.includes(id));
  }
  else {
    // Add those which are unselected
    idsThisPage.forEach(id => {
      if (!selectedIds.value.includes(id))
        selectedIds.value.push(id);
    });
  }
}
// CONFIRMATION POPUP WINDOW FOR DELETE

import PopupConfirmDelete from "./PopupConfirmDelete.vue";

const showPopupDelete = ref(false)
const clientsASupprimer = ref<string[]>([])

const deletionMessage = ref('') // DELETION MESSAGE

const requestDeletionLine = (id: string) => {
  deletionMessage.value = "ce client"
  clientsASupprimer.value = [id]
  showPopupDelete.value = true
  selectedId.value = id       // IDENTIFY THE CONCERNED LINE
}

const requestDeletionLineMultiple = () => {
  if ( selectedIds.value.length === 0)
    return
  const nb = selectedIds.value.length
  deletionMessage.value = `${nb} client${nb > 1 ? 's' : ''}`
  clientsASupprimer.value = [...selectedIds.value]
  showPopupDelete.value = true
}

const validateDeletion = async () => {
  for (const id of clientsASupprimer.value) {
    await axios.delete(`http://localhost:5034/Customer/${id}`)
  }
  clientsASupprimer.value = []
  selectedIds.value = []
  selectedId.value = null
  formActive.value = false
  await fetchCustomers()
  showPopupDelete.value = false
}

const cancelDeleting = () => {
  showPopupDelete.value = false
  selectedId.value = null
}

// FORM PROCEDURES : DISPLAY, SUBMITTING AND CONTROL OF ACTIONS WHILE EDITING
const formActive = ref(false)
const selectedId = ref<string | null>(null)

const form = ref<Customer>({
  id: '',
  name: '',
  firstname: '',
  email: '',
  phonenumber: '',
  adress: '',
  city: '',
  adresscode: '',
  datecreation: ''
})

const startModification = (client: Customer) => {
  formActive.value = true
  selectedId.value = client.id
  form.value = { ...client }
}

const cancelModification = () => {
  formActive.value = false
  selectedId.value = null
}

const validateModification = async () => {
  if (!selectedId.value)
    return
  try {
    await axios.put(`http://localhost:5034/Customer/${selectedId.value}`, form.value)

    formActive.value = false

    selectedId.value = null

    await fetchCustomers()
  } catch (error) {
    console.error("Erreur lors de la mise à jour :", error)
    alert("Une erreur est survenue lors de la modification.")
  }
}

</script>