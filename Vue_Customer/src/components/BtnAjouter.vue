<style scoped>
.button {
  position: relative;
  width: 195px;
  height: 40px;
  cursor: pointer;
  display: flex;
  align-items: center;
  border: 1px solid #2569c3;
  background-color: #2569c3;
  font-family: "Century Gothic";
  font-size: 14px;
}

.button, .button__icon, .button__text {
  transition: all 0.3s;
}

.button .button__text {
  transform: translateX(30px);
  color: #fff;
  font-weight: 600;
}

.button .button__icon {
  position: absolute;
  transform: translateX(109px);
  height: 100%;
  width: 39px;
  background-color: #2569c3;
  display: flex;
  align-items: center;
  justify-content: center;
  margin-left: 40px;
}

.button .svg {
  width: 30px;
  stroke: #fff;
}

.button:hover {
  background: #2569c3;
}

.button:hover .button__text {
  color: transparent;
}

.button:hover .button__icon {
  width: 148px;
  transform: translateX(0);
}

.button:active .button__icon {
  background-color: #2569c3;
}

.button:active {
  border: 1px solid #2569c3;
}
</style>

<template>
  <button type="button" class="button" >
    <span class="button__text">Ajouter un client</span>
    <span class="button__icon"><svg xmlns="http://www.w3.org/2000/svg" width="24" viewBox="0 0 24 24" stroke-width="2" stroke-linejoin="round" stroke-linecap="round" stroke="currentColor" height="24" fill="none" class="svg"><line y2="19" y1="5" x2="12" x1="12"></line><line y2="12" y1="12" x2="19" x1="5"></line></svg></span>
  </button>
</template>

<script setup lang="ts">
</script>