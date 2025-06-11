<style scoped>
.form {
  display: flex;
  flex-direction: column;
  gap: 10px;
  min-width: 500px;
  background-color: palegoldenrod;
  padding: 20px;
  border-radius: 20px;
  position: relative;
  margin-top: 50px;
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

.flex label:nth-child(2) {
  margin-left: 25px;
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

.afficherListe{
  width: 54vw;
  height: 63vh;
}

</style>

<template>
  <div class="flex justify-center items-center">
    <form class="form" @submit.prevent="submitClient">
      <p class="title">Ajouter un client </p>
      <p class="message">Veuillez saisir les informations du client avant de valider. </p>
      <div class="flex">
        <label>
          <input required type="text" class="input" v-model="client.nom">
          <span>Nom</span>
        </label>

        <label>
          <input required id="prenom" type="text" class="input" v-model="client.prenom">
          <span>Prénom</span>
        </label>
      </div>

      <label>
        <input required type="email" class="input" v-model="client.email">
        <span>Email</span>
      </label>

      <label>
        <input required type="tel" class="input" v-model="client.telephone">
        <span>Téléphone</span>
      </label>
      <label>
        <input required type="text" class="input" v-model="client.adresse">
        <span>Adresse</span>
      </label>
      <label>
        <input required type="text" class="input" v-model="client.ville">
        <span>Ville</span>
      </label>
      <label>
        <input v-model="client.codepostal" required class="input" type="text" maxlength="5" pattern="\d{5}">
        <span>Code Postal</span>
      </label>
      <button class="submit">Ajouter</button>
      <p class="signin">Vous souhaitez vérifiez les informations du client ajouté ?
        <a href="#" @click.prevent="afficherListe=true"> Cliquez ici</a> </p>
    </form>

    <div v-if="afficherListe" class="afficherListe">
      <CustomerList v-if="afficherListe" />
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue'
import axios from 'axios'
import CustomerList from './CustomerList.vue';// Permettre de visualiser le client après l'avoir ajouté

const client = ref({
  nom: '',
  prenom: '',
  email: '',
  telephone: '',
  adresse: '',
  ville: '',
  codepostal: ''
})

const afficherListe = ref(false)// Constante d'affichage liste à droite au click du lien pour voir si client ajouté

// Action de la soumision d'envoi des informations des clients
const submitClient = async () => {
  const confirmed = confirm("Confirmez-vous l'ajout de ce client ?")
  if (!confirmed) {
    return // Annule l'enregistrement si la personne clique sur "Annuler"
  }

  try {
    await axios.post('http://localhost:5034/Customer', client.value)
    alert('Client ajouté avec succès !')

    // Reset du formulaire
    client.value = {
      nom: '',
      prenom: '',
      email: '',
      telephone: '',
      adresse: '',
      ville: '',
      codepostal: ''
    }

  } catch (error) {
    alert("Erreur lors de l'ajout du client.")
    console.error('Erreur lors de l’ajout :', error)
  }
}

</script>