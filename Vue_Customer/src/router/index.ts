// src/router/index.ts
import { createRouter, createWebHistory } from 'vue-router'
import AddNewCustomer from '../components/AddNewCustomer.vue'

const routes = [
    { path: '/ajouter-client', name: 'AjouterClient', component: AddNewCustomer }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router
