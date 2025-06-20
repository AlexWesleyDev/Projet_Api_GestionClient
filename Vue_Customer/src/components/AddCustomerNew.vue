<style scoped>


.form {
  display: flex;
  flex-direction: column;
  gap: 10px;
  width: 529px;
  background-color: #edf2f9;
  padding: 20px;
  border-radius: 20px;
  position: relative;
  -webkit-box-shadow: -6px -4px 7px -2px rgba(0,0,0,0.15);
  box-shadow: -6px -4px 7px -2px rgba(0,0,0,0.15);
}

.title {
  font-size: 28px;
  color: #2569c3;
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
  background-color: #2569c3;
}

.title::before {
  width: 18px;
  height: 18px;
  background-color: #2569c3;
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
  margin-top: 0;
  font-size: 17px;
  font-weight: 600;
  font-family: "Century Gothic";
}

.signin {
  font-size: 16px;
  font-weight: 600;
  font-family: Century Gothic;
  /*margin-bottom: 50px;*/
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
  width: 97%;
  height: 85vh;
  border-radius: 25px;
  background-color: white;
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

.form label {
  position: relative;
}

.form label .input {
  width: 90%;
  padding: 10px 10px 20px 10px;
  margin-bottom: 5px; /* A adapter */
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

.submit {
  font-family: "Century Gothic";
  font-weight: 900;
  border: none;
  outline: none;
  background-color: #2569c3;
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
  width: 64%;
  height:  97%;
}

</style>

<template>
  <div class="flex justify-center items-center">
    <div class="formulaire">
      <form class="form" @submit.prevent="submitClient">
        <p class="title">Ajouter un client </p>
        <p class="message">Veuillez saisir les informations du client avant de valider. </p>
          <label>
            <input required type="text" class="input" v-model="client.nom">
            <span>Nom</span>
          </label>

          <label>
            <input required id="prenom" type="text" class="input" v-model="client.prenom">
            <span>Prénom</span>
          </label>

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
    </div>

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

const emit = defineEmits(['clientAjoute'])

const afficherListe = ref(false)// Constante d'affichage liste à droite au click du lien pour voir si client ajouté

// Action de la soumision d'envoi des informations des clients
const submitClient = async () => {
  try {
    await axios.post('http://localhost:5034/Customer', client.value)

    emit('clientAjoute') // <- nouveau

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