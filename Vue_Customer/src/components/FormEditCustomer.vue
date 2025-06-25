<script setup lang="ts">
    defineProps<{
      form: {
        name: string
        firstname: string
        email: string
        phonenumber: string
        adress: string
        city: string
        adresscode: string
      }
    }>()

defineEmits(['confirm', 'cancel'])

</script>

<template>
  <form class="form" @submit.prevent="$emit('confirm')">
    <p class="title">Modifier un client </p>
    <p class="message">Vous pouvez modifier les informations du client avant de les valider. </p>
      <label>
        <input required type="text" class="input" v-model="form.name">
        <span>Nom</span>
      </label>

      <label>
        <input required id="prenom" type="text" class="input" v-model="form.firstname">
        <span>Prénom</span>
      </label>

    <label>
      <input required type="email" class="input" v-model="form.email">
      <span>Email</span>
    </label>

    <label>
      <input required type="tel" class="input" v-model="form.phonenumber">
      <span>Téléphone</span>
    </label>
    <label>
      <input required type="text" class="input" v-model="form.adress">
      <span>Adresse</span>
    </label>
    <label>
      <input required type="text" class="input" v-model="form.city">
      <span>Ville</span>
    </label>
    <label>
      <input v-model="form.adresscode" required class="input" type="text" maxlength="5" pattern="\d{5}">
      <span>Code Postal</span>
    </label>
    <button class="submit" type="submit">Confirmer les modifications</button>
    <button class="cancel" type="button" @click="$emit('cancel')">Annuler</button>
  </form>
</template>

<style scoped>
.form {
  display: flex;
  flex-direction: column;
  /*gap: 10px;*/
  width: 22vw;
  background-color: #edf2f9;
  padding-inline: 20px;
  border-radius: 20px;
  position: relative;
  margin-top: 25px;
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

.form label {
  position: relative;
}

.form label .input {
  width: 90%;
  padding-top: 10px;
  padding-right: 10px;
  padding-bottom: 20px;
  padding-left: 10px;
  margin-bottom: 15px;
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


.submit , .cancel{
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

.submit:hover , .cancel:hover {
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

.cancel{
  margin-top: 25px;
  margin-bottom: 25px;
}

</style>