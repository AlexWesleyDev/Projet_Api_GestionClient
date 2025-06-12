<template>
  <div class="flex">
    <div class="ConteneurList">
      <div class="content">
        <div class="btn-modifier">

          <input type="text" placeholder="Rechercher ..." class="barreRecherche" v-model="searchQuery" />

          <!--Bouton Modifier -->
          <button class="edit-button" v-if="selectedId" @click="confirmerEtAfficherFormulaire">
            <svg class="edit-svgIcon" viewBox="0 0 512 512">
              <path d="M410.3 231l11.3-11.3-33.9-33.9-62.1-62.1L291.7 89.8l-11.3 11.3-22.6 22.6L58.6 322.9c-10.4 10.4-18 23.3-22.2 37.4L1 480.7c-2.5 8.4-.2 17.5 6.1 23.7s15.3 8.5 23.7 6.1l120.3-35.4c14.1-4.2 27-11.8 37.4-22.2L387.7 253.7 410.3 231zM160 399.4l-9.1 22.7c-4 3.1-8.5 5.4-13.3 6.9L59.4 452l23-78.1c1.4-4.9 3.8-9.4 6.9-13.3l22.7-9.1v32c0 8.8 7.2 16 16 16h32zM362.7 18.7L348.3 33.2 325.7 55.8 314.3 67.1l33.9 33.9 62.1 62.1 33.9 33.9 11.3-11.3 22.6-22.6 14.5-14.5c25-25 25-65.5 0-90.5L453.3 18.7c-25-25-65.5-25-90.5 0zm-47.4 168l-144 144c-6.2 6.2-16.4 6.2-22.6 0s-6.2-16.4 0-22.6l144-144c6.2-6.2 16.4-6.2 22.6 0s6.2 16.4 0 22.6z"></path>
            </svg>
          </button>

        </div>

        <table>
          <thead>
          <tr>
            <th></th>
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
            <tr v-for="client in paginatedCustomers" :key="client.id" @click="ligneSelection(client.id)"
                :class="selectedId === client.id ? 'selected' : ''" class="cursor-pointer" >
              <td>
                <input type="checkbox" :value="client.id" :checked="selectedId === client.id" @click.stop="ligneSelection(client.id)"
                />
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
    </div>

    <!-- PARTIE FORMUALIRE DE MODIFICATION -->

    <div class="afficherformulaireCustomer" v-if="afficherFormulaire" >
      <form class="form" @submit.prevent="validerModification">
        <p class="title">Modifier un client </p>
        <p class="message">Vous pouvez modifier les informations du client avant de les valider. </p>
        <div class="flex">
          <label>
            <input required type="text" class="input" v-model="form.nom">
            <span>Nom</span>
          </label>

          <label>
            <input required id="prenom" type="text" class="input" v-model="form.prenom">
            <span>Prénom</span>
          </label>
        </div>

        <label>
          <input required type="email" class="input" v-model="form.email">
          <span>Email</span>
        </label>

        <label>
          <input required type="tel" class="input" v-model="form.telephone">
          <span>Téléphone</span>
        </label>
        <label>
          <input required type="text" class="input" v-model="form.adresse">
          <span>Adresse</span>
        </label>
        <label>
          <input required type="text" class="input" v-model="form.ville">
          <span>Ville</span>
        </label>
        <label>
          <input v-model="form.codepostal" required class="input" type="text" maxlength="5" pattern="\d{5}">
          <span>Code Postal</span>
        </label>
        <button class="submit">Confirmer les modifications</button>
      </form>
    </div>
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

// CONSTANTES DECLAREES
const customers = ref<Customer[]>([])

const form = ref<Customer>({
  id: '',
  nom: '',
  prenom: '',
  email: '',
  telephone: '',
  adresse: '',
  ville: '',
  codepostal: ''
})

// Chargement liste des clients
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

// SELECTION
const selectedId = ref<string | null>(null)
const ligneSelection = (id: string) => {
  selectedId.value = selectedId.value === id ? null : id
}

// AFFICHAGE DU FORMULAIRE
const afficherFormulaire = ref(false)

const confirmerEtAfficherFormulaire = () => {
  if (selectedId.value) {
    const client = customers.value.find(c => c.id === selectedId.value)
    if (client && confirm('Voulez-vous modifier ce client ?')) {
      form.value = { ...client }
      afficherFormulaire.value = true
    }
  }
}

// SOUMISSION D'ENVOI
const validerModification = async () => {
  if (confirm('Confirmer les modifications ?')) {
    try {
      await axios.put(`http://localhost:5034/Customer/${selectedId.value}`, form.value)
      alert('Modifications enregistrées')
      afficherFormulaire.value = false
      fetchCustomers()
    } catch (err) {
      console.error('Erreur lors de la modification :', err)
    }
  }
}

</script>

<style scoped>

/* STYLE DU FORMULAIRE */

.form {
  display: flex;
  flex-direction: column;
  gap: 10px;
  min-width: 100px;
  min-height: 700px;
  background-color: palegoldenrod;
  padding: 20px;
  border-radius: 20px;
  position: relative;
}

.title {
  font-size: 28px;
  color: tomato;
  font-weight: 900;
  letter-spacing: -1px;
  position: relative;
  display: flex;
  align-items: center;
  padding-left: 30px;
}

.title::before,.title::after {
  position: absolute;
  content: "";
  height: 16px;
  width: 16px;
  border-radius: 50%;
  left: 0px;
  background-color: tomato;
}

.title::before {
  width: 18px;
  height: 18px;
  background-color: tomato;
}

.title::after {
  width: 18px;
  height: 18px;
  animation: pulse 1s linear infinite;
}

.message, .signin {
  color: rgba(88, 87, 87, 0.822);
  font-size: 14px;
}

.message {
  font-size: 17px;
  font-weight: 600;
  font-family: "Century Gothic";
}

.signin {
  font-size: 16px;
  font-weight: 600;
  font-family: Century Gothic;
}

.signin, .message {
  text-align: center;
}

.signin a {
  color: royalblue;
  font-weight: bold;
}

.signin a:hover {
  text-decoration: underline royalblue;
}

.flex {
  display: flex;
  width: 100%;
  gap: 6px;
}

.form label {
  position: relative;
}

.form label .input {
  width: 90%;
  padding: 10px 10px 20px 10px;
  outline: 0;
  border: 1px solid rgba(105, 105, 105, 0.397);
  border-radius: 20px;
}

.form label .input + span {
  position: absolute;
  left: 10px;
  top: 15px;
  color: grey;
  font-size: 0.9em;
  cursor: text;
  transition: 0.3s ease;
}

.form label .input:placeholder-shown + span {
  top: 15px;
  font-size: 0.9em;
}

.form label .input:focus + span,.form label .input:valid + span {
  top: 30px;
  font-size: 0.7em;
  font-weight: 600;
}

.form label .input:valid + span {
  color: green;
}

.flex label:nth-child(1) {
  width: 40%;
}

.flex label:nth-child(2) {
  margin-left: 22px;
  width: 50%;
}

.submit {
  font-family: "Century Gothic";
  font-weight: 900;
  border: none;
  outline: none;
  background-color: tomato;
  padding: 10px;
  border-radius: 10px;
  color: #fff;
  font-size: 16px;
  transform: .3s ease;
}

.submit:hover {
  background-color: rgb(56, 90, 194);
}

@keyframes pulse {
  from {
    transform: scale(0.9);
    opacity: 1;
  }

  to {
    transform: scale(1.8);
    opacity: 0;
  }
}

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

/* STYLE DU BOUTON MODIFIER */

.edit-button {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  background-color: #059669;
  border: none;
  font-weight: 600;
  display: flex;
  align-items: center;
  justify-content: center;
  box-shadow: 0px 0px 20px rgba(0, 0, 0, 0.164);
  cursor: pointer;
  transition-duration: 0.3s;
  overflow: hidden;
  position: relative;
  text-decoration: none !important;
}

.edit-svgIcon {
  width: 17px;
  transition-duration: 0.3s;
}

.edit-svgIcon path {
  fill: white;
}

.edit-button:hover {
  width: 120px;
  border-radius: 50px;
  transition-duration: 0.3s;
  background-color: #059669;
  align-items: center;
}

.edit-button:hover .edit-svgIcon {
  width: 20px;
  transition-duration: 0.3s;
  transform: translateY(60%);
  -webkit-transform: rotate(360deg);
  -moz-transform: rotate(360deg);
  -o-transform: rotate(360deg);
  -ms-transform: rotate(360deg);
  transform: rotate(360deg);
}

.edit-button::before {
  display: none;
  content: "Modifier";
  color: white;
  transition-duration: 0.3s;
  font-size: 2px;
}

.edit-button:hover::before {
  display: block;
  padding-right: 10px;
  font-size: 18px;
  opacity: 1;
  transform: translateY(0px);
  transition-duration: 0.3s;
  font-family: "Century Gothic";
}
</style>
