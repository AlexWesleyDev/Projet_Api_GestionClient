import axios from 'axios'

const api = axios.create({
    baseURL: 'http://localhost:5034', // 👈 ton API ASP.NET Core
    headers: {
        'Content-Type': 'application/json'
    }
})

export default api
