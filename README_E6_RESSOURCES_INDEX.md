# 📚 INDEX RESSOURCES E6 - GUIDE COMPLET
## Votre kit complet pour l'exam E6 - 28 Mai 2026 à 9h

---

## 🎯 STRUCTURE RESSOURCES

Vous avez maintenant **4 documents** à emporter :

```
📁 E6_RESSOURCES_EXAM/
│
├─ 📇 E6_CHEAT_SHEET_RAPIDE.md
│  └─ À IMPRIMER RECTO-VERSO pour consultation rapide
│  └─ Scénarios probables + Commandes + Checklist
│  └─ Format: Compact, rapide à consulter
│  └─ Quand l'utiliser: Déblocage rapide si besoin
│
├─ 📖 E6_FICHES_EVOLUTIONS_PRATIQUES.md
│  └─ Document PRINCIPAL avec toutes les fiches
│  └─ 5 fiches détaillées (Pagination, Recherche, Auth, Tri, Export)
│  └─ Code complet + étapes pas-à-pas
│  └─ Quand l'utiliser: "Jury demande feature X"
│
├─ 📂 E6_GUIDE_NAVIGATION_FICHIERS.md
│  └─ Où trouver exactement quoi modifier
│  └─ Structure projet + localisation fichiers
│  └─ Raccourcis clavier + tips productivité
│  └─ Quand l'utiliser: "Par où je commence?"
│
├─ 🖥️ E6_GUIDE_WINDOWS_SERVER.md
│  └─ Démarrage projet + Troubleshooting
│  └─ Commandes PowerShell
│  └─ Vérification environnement
│  └─ Quand l'utiliser: Avant 9h + si problèmes
│
└─ 📝 E6_TEMPLATES_DOCUMENTATION.md
   └─ Templates pour documentation requise
   └─ Checklist que documenter
   └─ Screenshots à faire
   └─ Quand l'utiliser: Après chaque modification
```

---

## ⏰ TIMELINE EXAM

### MERCREDI 27 MAI (AVANT 20H)

**Préparation** :
```
☐ Télécharger tous les 4 documents
☐ Imprimer:
   - Cheat Sheet (format compact)
   - Fiches Évolutions (les 5 principales)
   - Guide Navigation (si besoin)
   
☐ Tester projet une dernière fois:
   - dotnet run (backend)
   - npm run dev (frontend)
   - Vérifier http://localhost:5173 OK
   - Vérifier https://localhost:5001/swagger OK
   
☐ Créer backup projet
   - Copy-Item -Recurse C:\...\Projet_Api_GestionClient C:\Desktop\E6_Backup\
   
☐ Dormir! (Important pour performance mentale)
```

---

### JEUDI 28 MAI - 8H30 (AVANT EXAM)

**Arrivée & Installation** :
```
☐ 8:30 - Arriver avec:
   - Documents imprimés
   - Clé USB (backup)
   - Ordinateur chargé
   
☐ 8:35 - Ouvrir terminal PowerShell
   - cd .\CustomerApi
   - dotnet run
   - Attendre: "Now listening on: https://localhost:5001"
   
☐ 8:40 - Ouvrir 2e terminal PowerShell
   - cd .\Vue_Customer
   - npm run dev
   - Attendre: "Local: http://localhost:5173"
   
☐ 8:45 - Vérifications
   - Ouvrir http://localhost:5173 → UI visible?
   - Ouvrir https://localhost:5001/swagger → Endpoints visibles?
   - Cliquer GET /Customer → Clients chargés?
   
☐ 8:55 - Prêt!
   - Tous documents disposés
   - IDE ouvert (Rider + VS Code)
   - Navigateur prêt
   - Respiration profonde
```

---

### JEUDI 28 MAI - 9H00 À 10H00 (EXAM)

**Phase T (30 min) - Préparation** :
```
Jury donne enoncé de besoin

☐ Lire enoncé 3 fois
☐ Comprendre exactement demande
☐ Chercher dans fiches si scénario connu
☐ Demander clarification si doute
☐ Planifier implémentation
```

**Phase E1 (20 min) - Entretien** :
```
Jury pose questions contexte
☐ Écouter attentivement
☐ Répondre honnêtement
☐ Poser questions si pas clair
```

**Phase Réalisation (60 min) - CODAGE** :
```
➡️ Vous avez 60 minutes pour:
   1. Implémenter modifications
   2. Tester tout
   3. Documenter
   
Stratégie timing:
- 00-05 min: Consulter fiches
- 05-35 min: Coder + adapter
- 35-50 min: Tester partout
- 50-60 min: Documenter + screenshots

Avoir à portée de main:
- Fiches (ouvertes)
- Guide Navigation
- Cheat Sheet
```

**Phase E2 (20 min) - Recette** :
```
Jury regarde ce que vous avez fait
☐ Montrer code modifié
☐ Montrer tests Swagger
☐ Montrer UI si changement
☐ Montrer documentation
☐ Expliquer choix techniques
```

**Phase H - Harmonisation** :
```
Commission valide note finale
```

---

## 🎯 UTILISER LES RESSOURCES

### Scénario 1 : "Jury demande pagination"

```
1. CONSULTER
   - Ouvrir E6_FICHES_EVOLUTIONS_PRATIQUES.md
   - Chercher "FICHE 1 : PAGINATION"
   - Lire de début à fin
   
2. CHERCHER FICHIERS
   - E6_GUIDE_NAVIGATION_FICHIERS.md
   - Localiser CustomerService.cs
   - Localiser CustomerController.cs
   
3. IMPLÉMENTER
   - Suivre étapes exactement
   - Copy-paste code depuis fiche
   - Adapter si besoin
   
4. TESTER
   - Swagger: GET /Customer/paged
   - Frontend: Charger clients
   - Console: Pas d'erreur?
   
5. DOCUMENTER
   - E6_TEMPLATES_DOCUMENTATION.md
   - Remplir template
   - Screenshots de tests
```

---

### Scénario 2 : "Jury demande recherche"

```
1. CONSULTER → FICHE 2 : RECHERCHE
2. LOCALISER → SearchAsync dans Service
3. IMPLÉMENTER → Code + Tests
4. DOCUMENTER → API_RECHERCHE.md
```

---

### Scénario 3 : "Ça marche pas"

```
1. CONSULTER → E6_GUIDE_WINDOWS_SERVER.md
2. Chercher erreur dans "Troubleshooting"
3. Suivre solution proposée
4. Si persiste → Restart complet
5. Si vraiment bloqué → Demander examinator
```

---

## 📋 FICHES ÉVOLUTIONS DISPONIBLES

| Fiche | Sujet | Temps | Difficulté |
|-------|-------|-------|-----------|
| 1️⃣ | Pagination Backend | 15 min | Facile |
| 2️⃣ | Recherche Backend | 12 min | Facile |
| 3️⃣ | Authentification JWT | 25 min | Moyen |
| 4️⃣ | Tri/Ordering | 8 min | Très facile |
| 5️⃣ | Export CSV | 10 min | Facile |

**Conseil** : Faire 1-2 fiches faciles (25-30 min) + doc (15 min) = succès garanti!

---

## 💾 FICHIERS CLOÉS À BIEN CONNAÎTRE

```
BACKEND (C#)
├─ Program.cs              ← Configuration globale + DI
├─ CustomerController.cs   ← À modifier SOUVENT
├─ CustomerService.cs      ← À modifier SOUVENT
├─ ICustomerService.cs     ← Interface (avant Service)
├─ Customer.cs             ← Entity (peu modifier)
└─ appsettings.json        ← Config (secrets, DB)

FRONTEND (Vue)
├─ src/api.ts              ← À modifier SOUVENT
├─ src/components/CustomerList.vue ← À modifier SOUVENT
├─ src/App.vue             ← Racine (peu modifier)
└─ vite.config.ts          ← Config build
```

**À retenir** :
- Service = Logique métier
- Controller = Endpoints REST
- Component = UI Vue
- api.ts = Client HTTP

---

## 🚀 COMMANDES ESSENTIELLES À CONNAÎTRE

```bash
# LANCEMENT
dotnet run                    # Backend
npm run dev                   # Frontend

# BUILD
dotnet build                  # Compiler backend
npm run build                 # Build frontend prod

# NETTOYAGE
dotnet clean                  # Nettoyer build
Remove-Item -Recurse -Force node_modules  # Nettoyer npm

# VÉRIFICATION
dotnet --version              # Vérifier .NET
npm --version                 # Vérifier npm
```

---

## 📞 AIDE RAPIDE

### "Je sais pas par où commencer"
→ Lire **E6_GUIDE_NAVIGATION_FICHIERS.md** section "Structure simplifiée"

### "Jury demande feature X"
→ Chercher dans **E6_FICHES_EVOLUTIONS_PRATIQUES.md**

### "Ça marche pas"
→ Consulter **E6_GUIDE_WINDOWS_SERVER.md** section "Troubleshooting"

### "Comment documenter?"
→ Suivre **E6_TEMPLATES_DOCUMENTATION.md**

### "Je suis dans la merde totale"
→ Lire **E6_CHEAT_SHEET_RAPIDE.md** pour déblocage express

---

## 🎓 ÉVALUATION E6 - RAPPEL

Vous serez noté sur :

```
✅ Concevoir et développer application        (40%)
   - Votre code implémenté
   - Architecture respectée
   
✅ Assurer maintenance corrective/évolutive   (30%)
   - Modifications fonctionnent
   - Code testéchâteau validé
   
✅ Gérer données                              (20%)
   - DB utilisée correctement
   - Données persistées
   
✅ DOCUMENTATION PRODUITE                     (10%)
   - Explications modifications
   - Screenshots validations
   
💡 Jury valorise:
   - Logique claire (pas juste copier-coller)
   - Tests consciencieux
   - Documentation complète
   - Explications orale
   - Gestion temps
```

---

## 🏆 STRATÉGIE RÉUSSITE GARANTIE

```
1. COMPRENDRE la demande (5 min)
   ✓ Écouter bien
   ✓ Poser clarifications
   
2. CONSULTER fiches (2 min)
   ✓ Trouver fiche correspondante
   ✓ Lire étapes
   
3. IMPLÉMENTER (30 min)
   ✓ Suivre fiche exactement
   ✓ Copy-paste code
   ✓ Adapter minimal
   ✓ Sauvegarder
   
4. TESTER (15 min)
   ✓ Backend: dotnet build + run OK?
   ✓ Frontend: npm dev OK?
   ✓ Swagger: endpoint OK?
   ✓ UI: affichage OK?
   
5. DOCUMENTER (8 min)
   ✓ Remplir template
   ✓ Faire screenshots
   ✓ Expliquer modifications
   
= SUCCÈS ✅
```

---

## 📊 RÉSUMÉ RESSOURCES

| Document | Pages | Impression | Usage |
|----------|-------|-----------|-------|
| Cheat Sheet | 2-3 | Recto-verso | Rapide |
| Fiches Évolutions | 15-20 | Complet | Principal |
| Guide Navigation | 8-10 | Optionnel | Si besoin |
| Guide Windows | 10-12 | Optionnel | Démarrage |
| Templates Doc | 5-8 | Optionnel | Documentation |

**À IMPRIMER OBLIGATOIREMENT** :
- ✅ Cheat Sheet (format compact)
- ✅ Fiches Évolutions (les 5 fiches)

**Optionnel (selon place/poids)** :
- ◻️ Guide Navigation
- ◻️ Guide Windows
- ◻️ Templates Documentation

---

## 🎁 BONUS : TIPS IMPORLE

### Avant exam
```
☐ Dormez bien
☐ Petit-déjeuner correct
☐ Arriver 30 min avant
☐ Tester environnement
☐ Respiration calme
```

### Pendant exam
```
☐ Lire enoncé 3 fois
☐ Commencer par simple
☐ Tester chaque étape
☐ Ne pas paniquer sur erreurs (normal!)
☐ Documenter au fur/mesure
```

### Attitude jury
```
Jury ne cherche pas parfection. Il cherche:
✅ Compréhension du projet
✅ Capacité résoudre problèmes
✅ Rigueur tests
✅ Clarté explications
✅ Gestion temps
```

---

## 📞 CONTACTS URGENCE (Si problème)

```
Avant exam:
→ Enseignant/Tuteur stage
→ IT support serveur

Pendant exam:
→ Examinator directement
→ Pas d'accès IA/Internet (pas autorisé)
→ Que ces ressources + IDE
```

---

## ✅ CHECKLIST FINALE - À VÉRIFIER

```
MERCREDI:
☐ Documents téléchargés
☐ Documents imprimés
☐ Projet testé une dernière fois
☐ Backup créé
☐ Dormi correctement

JEUDI 8H30:
☐ Arrivé avec documents + clé USB
☐ Backend lancé (dotnet run)
☐ Frontend lancé (npm run dev)
☐ URLs vérifiées (localhost:5173 + 5001)
☐ Swagger vérifié

JEUDI 9H00:
☐ Prêt mentalement
☐ Documents disposés clairement
☐ IDE ouverts
☐ Navigateur prêt
☐ Terminal PowerShell disponible
```

---

## 🎓 BON CHANCE! 💪

Vous avez l'infrastructure, les ressources, et la connaissance!

**Demain à 9h**, vous allez montrer au jury que vous maîtrisez:
- ✅ Architecture 3-tier
- ✅ Entity Framework Core
- ✅ Vue 3 + TypeScript
- ✅ REST API design
- ✅ Testing & documentation

**Vous pouvez le faire!** 🚀

---

**Index Ressources E6 - Guide Complet**

*Tous vos outils réunis pour l'exam 28 Mai 2026 à 9h*

*À garder, relire, et emporter!* 📚
