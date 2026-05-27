# 🖥️ GUIDE WINDOWS SERVER - DÉPLOIEMENT & LANCEMENT
## Préparation environnement E6 - À VÉRIFIER AVANT 9H

---

## ⚠️ CHECKLIST PRÉ-EXAM (À FAIRE LA VEILLE)

```
À faire MERCREDI 27 MAI avant 20h :

☐ Télécharger ce guide (sur clé USB ou imprimé)
☐ Vérifier .NET 9 SDK installé
   → dotnet --version (doit afficher 9.X.X)
   
☐ Vérifier Node.js / npm installés
   → node --version (18+ requis)
   → npm --version (10+ requis)
   
☐ Vérifier Git installé
   → git --version
   
☐ Tester démarrage projet
   → dotnet run (backend)
   → npm run dev (frontend)
   
☐ Vérifier accès URLs
   → http://localhost:5173 (frontend)
   → https://localhost:5001 (backend)
   → https://localhost:5001/swagger (API docs)
   
☐ Vérifier Database existe
   → Customer.db dans dossier projet
   
☐ Nettoyer fichiers temporaires
   → Supprimer dossiers bin/ obj/ node_modules/
   → Supprimer fichiers cache
```

---

## 📍 STRUCTURE SERVEUR WINDOWS

### Chemins importants

```
Lecteur C: (racine)
├─ Users/
│  └─ [VotreUser]/
│     ├─ RiderProjects/
│     │  └─ PROJETS STAGE/
│     │     └─ Projet_Api_GestionClient/  ← ICI LE PROJET
│     └─ .dotnet/          ← SDK .NET
│
├─ Program Files/
│  ├─ JetBrains/Rider/    ← IDE C#
│  ├─ Git/                 ← Versioning
│  └─ nodejs/              ← Node/npm
│
└─ Windows/               ← Système
```

---

## 🚀 DÉMARRAGE RAPIDE (5 MINUTES)

### Terminal PowerShell (Admin)

```powershell
# 1. Naviguer au projet
cd "$env:USERPROFILE\RiderProjects\PROJETS STAGE\Projet_Api_GestionClient"

# 2. Nettoyer caches (IMPORTANT si ça marche pas)
dotnet clean CustomerApi
Remove-Item -Recurse -Force Vue_Customer\node_modules -ErrorAction SilentlyContinue

# 3. Restaurer dépendances
dotnet restore CustomerApi
cd Vue_Customer
npm install
cd ..

# 4. Démarrer backend (Terminal 1)
cd CustomerApi
dotnet run
# Attendre : "Now listening on: https://localhost:5001"

# 5. Démarrer frontend (Terminal 2)
cd Vue_Customer
npm run dev
# Attendre : "Local: http://localhost:5173"

# 6. Ouvrir navigateur
# Frontend: http://localhost:5173
# API Docs: https://localhost:5001/swagger
```

---

## 🔧 COMMANDES ESSENTIELLES

### Démarrage

```powershell
# Terminal 1 - Backend
cd .\CustomerApi
dotnet run

# Terminal 2 - Frontend
cd .\Vue_Customer
npm run dev
```

### Arrêt

```powershell
# Dans chaque terminal :
Ctrl+C
```

### Rebuild complet

```powershell
# Si quelque chose ne marche pas
dotnet clean CustomerApi
Remove-Item -Recurse -Force Vue_Customer\node_modules
dotnet restore CustomerApi
cd Vue_Customer && npm install && cd ..
```

---

## 🌐 ACCÈS URLS

### Développement Local

```
Frontend UI
├─ URL: http://localhost:5173
├─ Port: 5173 (Vite dev server)
├─ Cache: Auto-reload à chaque save
└─ État: DEV avec hot reload

Backend API
├─ URL: https://localhost:5001
├─ Port: 5001 (Kestrel HTTPS)
├─ État: DEV avec logs
└─ Database: SQLite local

API Documentation
├─ URL: https://localhost:5001/swagger
├─ Tous endpoints visibles
└─ Testable directement (Try it out)
```

### Production (Windows Server réel)

```
Frontend UI
├─ URL: http://192.168.124.15:5034
├─ Deployé: Statique files + reverse proxy

Backend API
├─ URL: https://192.168.124.15:5034
├─ Deployé: IIS ou service Windows

Database
├─ Local: C:\CustomerApi\Customer.db
└─ Ou: SQL Server (si configuré)
```

---

## 📊 VÉRIFICATION ENVIRONNEMENT

### Vérifier installations

```powershell
# .NET SDK
dotnet --version
# Résultat attendu: 9.0.x

# .NET Runtime
dotnet --list-runtimes
# Résultat attendu: Microsoft.AspNetCore.App 9.0.x

# Node.js
node --version
# Résultat attendu: v18.x.x ou plus récent

# npm
npm --version
# Résultat attendu: 10.x.x ou plus récent

# Git
git --version
# Résultat attendu: git version 2.x.x
```

### Vérifier ports disponibles

```powershell
# Vérifier port 5173 (frontend)
netstat -ano | findstr :5173
# Si rien → Port libre OK

# Vérifier port 5001 (backend)
netstat -ano | findstr :5001
# Si rien → Port libre OK

# Si port occupé → Arrêter processus
# taskkill /PID [PID] /F
```

---

## 📁 STRUCTURE DOSSIERS IMPORTANTS

### Backend

```
CustomerApi/
├─ bin/
│  └─ Debug/
│     └─ net9.0/
│        └─ CustomerApi.dll   ← Application compilée
│
├─ obj/
│  └─ Debug/                  ← Fichiers build (ignore)
│
├─ Controllers/
│  └─ CustomerController.cs   ← ENDPOINTS REST
│
├─ Services/
│  ├─ ICustomerService.cs    ← Interface
│  └─ CustomerService.cs     ← Implémentation
│
├─ Models/
│  └─ Customer.cs            ← Entity
│
├─ Data/
│  └─ CustomerContext.cs     ← DbContext
│
├─ appsettings.json          ← Configuration ⭐
├─ Program.cs                ← Configuration DI/Middleware
└─ CustomerApi.csproj        ← File projet C#
```

### Frontend

```
Vue_Customer/
├─ node_modules/             ← Dépendances npm (ignore)
│
├─ dist/                      ← Build output (production)
│  └─ index.html
│
├─ src/
│  ├─ api.ts                 ← Client HTTP Axios
│  ├─ App.vue                ← Racine
│  ├─ main.ts                ← Point entrée
│  ├─ components/            ← Composants
│  ├─ router/
│  │  └─ index.ts            ← Routing
│  └─ assets/                ← Ressources statiques
│
├─ vite.config.ts            ← Config bundler ⭐
├─ tsconfig.json             ← Config TypeScript
├─ tailwind.config.js        ← Config CSS
├─ package.json              ← Dépendances npm
└─ index.html                ← HTML entry point
```

### Database

```
Projet_Api_GestionClient/
└─ Customer.db               ← SQLite database (LOCAL)
                              ← Créée automatiquement au 1er run
                              ← Contient: table Customers
```

---

## 🐛 TROUBLESHOOTING COURANT

### Problème: "Port already in use"

```
Erreur: "Address already in use" ou "Cannot bind to port"

Solution:
1. Trouver processus utilisant port
   netstat -ano | findstr :5001
   
2. Tuer processus
   taskkill /PID [numéro] /F
   
3. Relancer
   dotnet run
```

---

### Problème: "dotnet: command not found"

```
Erreur: Commande dotnet non reconnue

Solution:
1. Vérifier installation .NET
   dotnet --version
   
2. Si erreur → Réinstaller
   https://dotnet.microsoft.com/download/dotnet/9.0
   
3. Ajouter à PATH si nécessaire
   Paramètres → Variables d'environnement → Path
   → Ajouter C:\Program Files\dotnet
```

---

### Problème: "npm not found"

```
Erreur: npm command not found

Solution:
1. Vérifier installation Node.js
   node --version
   
2. Si erreur → Réinstaller
   https://nodejs.org (LTS 18.x+)
   
3. Après install, redémarrer terminal
```

---

### Problème: "Database not found"

```
Erreur: "Cannot open database file"

Solution:
1. Vérifier fichier existe
   ls Customer.db
   
2. Si n'existe pas → 1er run crée
   dotnet run (dans CustomerApi)
   → Attend quelques secondes
   
3. Si persiste → Supprimer migrations
   Remove-Item -Recurse -Force .\CustomerApi\obj\
   dotnet clean CustomerApi
   dotnet run
```

---

### Problème: "HTTPS certificate issue"

```
Erreur: "The SSL connection could not be established"

Solution:
1. Accepter certificat de développement
   dotnet dev-certs https --trust
   
2. Relancer backend
   dotnet run
   
3. Navigateur → Continue despite warning (1ère fois)
```

---

### Problème: "Frontend can't reach backend"

```
Erreur: Network error, CORS denied, 404 Not Found

Causes:
1. Backend pas lancé
   ✅ Vérifier Terminal 1 → "Now listening on"
   
2. Wrong URL in api.ts
   ✅ Vérifier http://localhost:5034 (dev: 5001)
   
3. CORS bloqu​é
   ✅ Vérifier Program.cs → CORS config
   
4. Endpoint typo
   ✅ Vérifier GET /Customer vs /customer
```

---

## 🧪 VÉRIFICATION FONCTIONNALITÉ

### Checklist démarrage OK

```powershell
# Terminal 1 - Vérifier backend lancé
Invoke-WebRequest https://localhost:5001 -SkipCertificateCheck

# Terminal 2 - Vérifier frontend lancé
Invoke-WebRequest http://localhost:5173

# Navigateur - Tester API
https://localhost:5001/swagger/index.html

# Navigateur - Tester UI
http://localhost:5173/
```

### Tests API Swagger

```
1. Ouvrir https://localhost:5001/swagger
2. Chercher "Customer" section
3. Cliquer GET /Customer
4. Cliquer "Try it out"
5. Cliquer "Execute"
6. Vérifier Réponse 200 OK
7. Vérifier Array de clients retourné
```

### Tests Frontend

```
1. Ouvrir http://localhost:5173/
2. Voir "Espace de gestion des clients"
3. Voir tableau clients
4. Voir bouton "Ajouter un client"
5. Cliquer pagination → Change page OK?
6. Chercher "dupont" → Filter OK?
```

---

## 📝 LOGS DEBUG

### Voir logs backend

```
Dans terminal du backend, vous verrez :
- Requêtes HTTP (GET /Customer)
- Erreurs SQL
- Exceptions
- Timings

Exemple:
Microsoft.AspNetCore.Hosting.Diagnostics[1]
Request starting HTTP/1.1 POST https://192.168.124.15:5001/Customer
```

### Voir logs frontend

```
1. Ouvrir http://localhost:5173/
2. F12 → Onglet Console
3. Voir erreurs/logs
4. Erreurs API affichées
```

---

## ⚙️ CONFIGURATION ESSENTIELLES

### appsettings.json (Backend)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=Customer.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

**À modifier si besoin** :
- Connection string (base de données)
- Logging levels
- CORS policy
- JWT secrets (si authentification ajoutée)

---

### vite.config.ts (Frontend)

```typescript
export default defineConfig({
  plugins: [vue()],
  server: {
    port: 5173,
    host: 'localhost'
  }
})
```

**À vérifier** :
- Port 5173 libre
- API proxy configuré correctement

---

## 🎯 CHECKLIST JOUR E6 - 9H

```
✅ 30 min avant (8:30)
   ☐ Terminal PowerShell prêt
   ☐ Navigateur ouvert
   ☐ VS Code/Rider accessible
   
✅ 15 min avant (8:45)
   ☐ Lancer backend: dotnet run
   ☐ Attendre "Now listening on: https://localhost:5001"
   
✅ 10 min avant (8:50)
   ☐ Lancer frontend: npm run dev
   ☐ Attendre "Local: http://localhost:5173"
   
✅ 5 min avant (8:55)
   ☐ Vérifier http://localhost:5173 OK
   ☐ Vérifier https://localhost:5001/swagger OK
   ☐ Vérifier table clients visible
   
✅ GO! (9:00)
   ☐ Tous terminals affichant "Listening"
   ☐ UI affichée correctement
   ☐ Prêt à coder!
```

---

## 💾 SAUVEGARDE AVANT E6

### Créer backup

```powershell
# Créer dossier backup
mkdir "$env:USERPROFILE\Desktop\E6_Backup"

# Copier projet entier
Copy-Item -Recurse -Force `
  "$env:USERPROFILE\RiderProjects\PROJETS STAGE\Projet_Api_GestionClient" `
  "$env:USERPROFILE\Desktop\E6_Backup\"
```

### Restaurer si problème

```powershell
# Si projet cassé pendant exam
Copy-Item -Recurse -Force `
  "$env:USERPROFILE\Desktop\E6_Backup\Projet_Api_GestionClient" `
  "$env:USERPROFILE\RiderProjects\PROJETS STAGE\" -Confirm:$false
```

---

## 📱 ACCÈS DISTANT (Si sur autre serveur)

### Modif pour remote

```csharp
// Dans Program.cs - Bind all interfaces
.UseUrls("https://0.0.0.0:5001", "http://0.0.0.0:5000")
```

```typescript
// Dans api.ts - URL distante
const api = axios.create({
  baseURL: 'https://192.168.124.15:5001'  // IP Serveur
})
```

---

## 🎓 EN CAS DE PROBLÈME EXAM

### Si rien marche

```
1. Prendre respiration (normal avoir stress!)
2. Consulter CETTE section troubleshooting
3. Relancer complètement
   - Arrêter tout (Ctrl+C)
   - dotnet clean CustomerApi
   - Relancer
4. Si persiste → Demander examinator
5. Avoir backup prêt
```

### Jury demande URL spécifique

```
"Ouvrez l'application à l'adresse X"

Réponse:
http://localhost:5173          (local DEV)
http://192.168.124.15:5034     (production Windows Server)
https://localhost:5001/swagger (API documentation)
```

---

**Guide Windows Server E6**

*À imprimer et avoir physiquement jour de l'exam*

*Testé & validé avant 9h = Exam sans stress!* 💪
