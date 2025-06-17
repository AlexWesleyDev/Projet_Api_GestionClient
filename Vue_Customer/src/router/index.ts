// src/router/index.ts
import { createRouter, createWebHistory } from 'vue-router'
import AddCustomer from '../components/AddCustomerNew.vue'

const routes = [
    { path: '/ajouter-client', name: 'AjouterClient', component: AddCustomer }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router
