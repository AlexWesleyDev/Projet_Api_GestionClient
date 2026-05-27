# 📂 GUIDE NAVIGATION RAPIDE - FICHIERS PROJET
## Où trouver exactement quoi modifier - Référence rapide

---

## 🎯 STRUCTURE SIMPLIFIÉE

```
Projet_Api_GestionClient/
│
├─ CustomerApi/                    ← BACKEND C# .NET
│  ├─ Program.cs                   ← Configuration/Middleware (IMPORTANT)
│  ├─ appsettings.json             ← Secrets/Config
│  ├─ Controllers/
│  │  └─ CustomerController.cs     ← ENDPOINTS REST (À MODIFIER SOUVENT)
│  ├─ Services/
│  │  ├─ ICustomerService.cs       ← Interface (signatures méthodes)
│  │  └─ CustomerService.cs        ← Implémentation (logique réelle)
│  ├─ Data/
│  │  └─ CustomerContext.cs        ← DbContext Entity Framework
│  └─ Models/
│     └─ Customer.cs               ← Entité/Structure données
│
└─ Vue_Customer/                   ← FRONTEND Vue 3
   ├─ src/
   │  ├─ api.ts                    ← Client HTTP Axios (À MODIFIER SOUVENT)
   │  ├─ App.vue                   ← Racine application
   │  ├─ main.ts                   ← Point entrée JS
   │  └─ components/
   │     ├─ CustomerList.vue       ← Affichage tableau (À MODIFIER SOUVENT)
   │     ├─ AddNewCustomer.vue     ← Formulaire création
   │     ├─ FormEditCustomer.vue   ← Formulaire édition
   │     ├─ SearchBar.vue          ← Recherche
   │     ├─ Header.vue             ← En-tête
   │     └─ Menu.vue               ← Navigation
   ├─ vite.config.ts               ← Config bundler
   ├─ tsconfig.json                ← Config TypeScript
   └─ tailwind.config.js           ← Config CSS

DATABASE :
└─ Customer.db                     ← Fichier SQLite (local)
```

---

## 🔍 PAR TYPE DE MODIFICATION

### 1️⃣ AJOUTER ENDPOINT API

**Fichier à modifier** :
```
CustomerApi/Controllers/CustomerController.cs
```

**Pattern à suivre** :
```csharp
[HttpGET/POST/PUT/DELETE]
[Route("chemin")]
public async Task<ActionResult> NomFonction(parametres)
{
    try 
    {
        // Logique
        return Ok(data);
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

---

### 2️⃣ AJOUTER LOGIQUE MÉTIER

**Fichiers à modifier** :
```
1. CustomerApi/Services/ICustomerService.cs
   → Ajouter signature Task<Type> NomFonction();

2. CustomerApi/Services/CustomerService.cs
   → Ajouter implémentation
```

---

### 3️⃣ MODIFIER APPELS API (Frontend)

**Fichier à modifier** :
```
Vue_Customer/src/api.ts
```

**Pattern** :
```typescript
export const nomFonction = async (params) => {
    try {
        const response = await api.get('/endpoint', { params });
        return response.data;
    } catch (error) {
        console.error('Error:', error);
        throw error;
    }
};
```

---

### 4️⃣ AJOUTER COMPOSANT UI

**Fichier à modifier** :
```
Vue_Customer/src/components/
```

**Pattern** :
```vue
<template>
  <!-- HTML -->
</template>

<script setup lang="ts">
  // TypeScript
</script>

<style scoped>
  /* CSS */
</style>
```

---

### 5️⃣ AJOUTER CONFIGURATION

**Fichier à modifier** :
```
CustomerApi/appsettings.json
```

**Pattern** :
```json
{
  "SectionName": {
    "Key1": "value1",
    "Key2": "value2"
  }
}
```

---

## 📍 LOCALISATION FICHIERS CLÉS

### Backend Files

| Action | Fichier | Ligne approx |
|--------|---------|------------|
| Ajouter endpoint | `CustomerController.cs` | Fin classe (avant `}`) |
| Ajouter logique | `CustomerService.cs` | Fin classe (avant `}`) |
| Nouvelle interface | `ICustomerService.cs` | Avant `}` interface |
| Ajouter model | `Models/` | Créer nouveau `.cs` |
| Config globale | `Program.cs` | Avant `app.Run()` |
| Secrets/Config | `appsettings.json` | JSON valide |

---

### Frontend Files

| Action | Fichier | Ligne approx |
|--------|---------|------------|
| Nouvelle requête API | `src/api.ts` | Fin fichier |
| Modifier UI | `components/*.vue` | `<template>` section |
| Ajouter logique | `components/*.vue` | `<script>` section |
| Styling | `components/*.vue` | `<style>` section |
| Config build | `vite.config.ts` | Export config |

---

## 🔧 COMMENT OUVRIR FICHIER RAPIDEMENT

### Dans Rider (IDE .NET)

```
Ctrl+N → Nom du fichier
Exemple : Ctrl+N → "CustomerService"
→ Ouvre CustomerService.cs
```

### Dans VS Code (Frontend)

```
Ctrl+P → Nom du fichier
Exemple : Ctrl+P → "api.ts"
→ Ouvre src/api.ts
```

---

## 📋 CHECKLIST MODIFICATION

Avant de modifier, vérifier :

```
☐ Fichier ouvert (bon projet/dossier?)
☐ Bon fichier (confirmé path?)
☐ Syntaxe comprise (C# vs TypeScript)
☐ Indentation respectée
☐ Pas de typos (case sensitive!)
☐ Point virgule/accolades OK
☐ Import/Using statements présents
```

---

## 💾 AVANT/APRÈS MODIFICATION

### Sauvegarder

```
Ctrl+S (dans IDE)
```

### Rebuild Backend

```bash
cd CustomerApi
dotnet build
```

### Rebuild Frontend

```bash
cd Vue_Customer
npm run build
```

### Relancer Backend

```bash
dotnet run
```

### Relancer Frontend

```bash
npm run dev
```

---

## 🧪 TESTER MODIFICATION

### Endpoint API

```
1. Frontend relancé (npm run dev)?
2. Backend relancé (dotnet run)?
3. Ouvrir Swagger : https://localhost:5001/swagger
4. Chercher nouvel endpoint
5. Cliquer "Try it out"
6. Remplir paramètres
7. Cliquer "Execute"
8. Vérifier réponse OK?
```

### Composant Frontend

```
1. Backend sur localhost:5001?
2. Frontend sur localhost:5173?
3. Ouvrir http://localhost:5173
4. Naviguer vers le composant
5. Vérifier visuel OK?
6. Console JS OK (aucune erreur rouge)?
```

---

## 📊 STRUCTURE APPELS API

### Dans Vue Component

```typescript
// 1. Import fonction API
import { getCustomers, createCustomer } from '@/api';

// 2. Utiliser dans script
const fetchClients = async () => {
    try {
        customers.value = await getCustomers();
    } catch (err) {
        console.error('Erreur:', err);
    }
};

// 3. Appeler dans template
@click="fetchClients()"
```

---

## 🔄 FLUX DONNÉES (À COMPRENDRE)

```
Frontend (Vue)
    ↓
    → Appel fonction api.ts
           ↓
           → HTTP Request (Axios)
                  ↓
                  ↓ Network
                  ↓
Backend (C#)
    → CustomerController endpoint
           ↓
           → CustomerService logique
                  ↓
                  → DbContext (Entity Framework)
                        ↓
                        ↓ SQL
                        ↓
                  SQLite Database
                        ↓
                        ↓ résultats
                        ↓
           ← Retour données
    ← HTTP Response JSON
           ↓
Vue re-render
```

**À retenir** : Chaque modification = chaîne complète!

---

## 🚨 ERREURS COURANTES

### "Fichier pas trouvé"

```
❌ Mauvais path
✅ Vérifier exact path dans Explorer

❌ Typo nom fichier
✅ Copy-paste depuis Explorer
```

### "Compilation error"

```
❌ Oubli ; ou }
✅ Vérifier fin de ligne

❌ Class pas héritée de bonne base
✅ Vérifier : class X : BaseClass { }

❌ Import manquant
✅ Ajouter using statement
```

### "RuntimeError - Property null"

```
❌ Variable pas initialisée
✅ Initialiser avant utilisation

❌ Configuration pas chargée
✅ Vérifier appsettings.json
```

---

## 📞 RECHERCHE RAPIDE CODE

### Trouver une fonction

```
Ctrl+F → Nom fonction
Exemple : Ctrl+F → "GetAllAsync"
```

### Trouver utilisations

```
Rider : Ctrl+Alt+F7 (Find Usages)
VS Code : Ctrl+Shift+H (Find References)
```

### Remplacer partout

```
Ctrl+H → Find/Replace
Attention : vérifier avant replace all!
```

---

## 🎯 ÉTAPES MODIFICATION TYPIQUE

### Scénario : Ajouter paramètre tri

**Étape 1 - Backend** (5 min)
```
1. Ouvrir CustomerService.cs
2. Chercher GetAllAsync
3. Ajouter paramètre : string orderBy = "name"
4. Ajouter logique tri
5. Ctrl+S (sauvegarder)
6. dotnet build (compiler)
```

**Étape 2 - Frontend** (3 min)
```
1. Ouvrir api.ts
2. Trouver getCustomers
3. Ajouter paramètre orderBy
4. Passer à request: { params: { orderBy } }
5. Ctrl+S (sauvegarder)
6. Frontend reload automatique
```

**Étape 3 - Component** (2 min)
```
1. Ouvrir CustomerList.vue
2. Chercher const handleSort
3. Ajouter appel : await getCustomers(orderBy)
4. Ctrl+S
5. Vérifier UI
```

**Étape 4 - Test** (2 min)
```
1. Swagger /Customer?orderBy=name → teste backend
2. Frontend → clicker tri → teste intégration
3. Vérifier results OK
```

**Étape 5 - Document** (2 min)
```
1. Créer API_SORTING.md
2. Documenter paramètres
3. Donner exemples
```

---

## 💡 TIPS PRODUCTIVITÉ

### Pour aller VITE

```
1. Copie-colle code depuis fiches (pas réinventer)
2. Adaptation minimaliste (change que nécessaire)
3. Test immédiat après chaque changement (avant suivant)
4. Ctrl+Z (undo) si ça marche pas
5. Google si vraiment stuck (mais fiches couvrent 90%)
```

### Organisation fichiers

```
Garder ouverts pendant E6 :
- Rider avec CustomerController.cs + CustomerService.cs
- VS Code avec api.ts + CustomerList.vue
- Browser Swagger : localhost:5001/swagger
- Browser Frontend : localhost:5173
```

---

## 📋 MÉMO RACCOURCIS CLAVIER

```
Général :
Ctrl+S        Sauvegarder
Ctrl+Z        Undo (annuler)
Ctrl+Shift+Z  Redo (refaire)

Fichiers :
Ctrl+N        Ouvrir fichier (Rider)
Ctrl+P        Ouvrir fichier (VS Code)
Ctrl+Tab      Fichier suivant
Ctrl+Shift+Tab Fichier précédent

Édition :
Ctrl+/        Commenter/Décommenter
Ctrl+D        Dupliquer ligne
Ctrl+X        Couper ligne
Ctrl+Shift+K  Supprimer ligne

Recherche :
Ctrl+F        Find (chercher)
Ctrl+H        Replace (remplacer)
Ctrl+G        Go to line (aller à ligne)
```

---

**Guide Navigation Projet - Référence Rapide**

*À garder à portée de main pendant E6*
