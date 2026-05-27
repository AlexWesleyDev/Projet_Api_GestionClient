# ⚡ QUICK START 5 MIN - AVANT D'ÉCOUTER LE JURY
## À LIRE ABSOLUMENT avant 9h05 - Format ultra-compact

---

## 🎯 CES 5 MINUTES DÉCIDENT DE VOTRE EXAM

```
9:00 → Jury commence
9:00-9:05 → LIRE CE DOCUMENT
9:05 → Jury parle
9:05-9:30 → Vous écoutez bien
9:30+ → Vous codez (c'est là que vous utilisez autres fiches)
```

---

## 📍 D'ABORD: PRENDRE RESPIRATION PROFONDE

```
Inspirez 4 sec → Bloquez 4 sec → Expirez 4 sec
Répétez 3 fois

Vous pouvez le faire! 💪
```

---

## 🎓 RAPPEL RAPIDE

Vous allez probablement avoir UNE DE CES DEMANDES :

```
1. Recherche (par ville/nom)     → FICHE 2 → 12 min facile
2. Tri (par colonne)              → FICHE 4 → 8 min très facile
3. Pagination                     → FICHE 1 → 15 min facile
4. Export CSV                     → FICHE 5 → 10 min facile
5. Validation données             → FICHE 6 → 8 min facile
6. Authentification               → FICHE 3 → 25 min complexe (si temps)
```

**Plus probable** : 1-2 petites évolutions faciles

---

## 🚀 PROCESSUS UNIVERSEL

Quelle que soit la demande, TOUJOURS :

```
1. JURY PARLE (écouter 100%)
   → Qu'est exactement demandé?
   → Quels fichiers impactés?
   → Délai estimé?

2. VOUS CONSULTE FICHES (2 min)
   → Ouvrir E6_FICHES_EVOLUTIONS_PRATIQUES.md
   → Chercher fiche correspondante
   → Lire jusqu'au code template

3. VOUS CODE (25-30 min)
   → Copier code depuis fiche
   → Adapter noms/variables si besoin
   → Sauvegarder (Ctrl+S)

4. VOUS TESTE (10 min)
   → Backend: Recompile (dotnet build)?
   → Frontend: Rechargé (npm dev)?
   → Swagger: Endpoint visible?
   → UI: Fonctionne visuellement?

5. VOUS DOCUMENTE (8 min)
   → Remplir template E6_TEMPLATES_DOCUMENTATION.md
   → Faire 2-3 screenshots
   → Expliquer modifications

6. VOUS MONTREZ AU JURY (3 min)
   → Voir code modifié
   → Voir tests Swagger
   → Voir UI si changement
   → Expliquer choix
```

**TOTAL** : ~60 minutes = SUCCÈS ✅

---

## 🎯 CLÉS À RETENIR

### Fichiers que vous allez modifier

```
BACKEND:
→ CustomerController.cs      (Ajouter endpoints)
→ CustomerService.cs         (Ajouter logique)
→ ICustomerService.cs        (Ajouter interface)

FRONTEND:
→ src/api.ts                 (Ajouter appels API)
→ src/components/CustomerList.vue (Modifier UI)
```

### Pattern à suivre

```
Service (Logique) → Controller (Endpoint) → Frontend (UI)

Modifiez dans cet ordre:
1. Interface (décrire contrat)
2. Service (implémenter logique)
3. Controller (exposer endpoint)
4. Frontend (appeler et afficher)
```

### Commandes clés

```bash
dotnet run                  # Lancer backend
npm run dev                 # Lancer frontend
dotnet build               # Compiler backend (après modif)
npm run build              # Build frontend (si besoin)
```

---

## ⚡ SI PANIQUÉ/BLOQUÉ

```
BLOCAGE 1: "Je comprends pas ce qu'il demande"
→ DEMANDER CLARIFICATION
→ Jury aime bien quand vous posez questions

BLOCAGE 2: "Je sais pas par où commencer"
→ Ouvrir E6_FICHES_EVOLUTIONS_PRATIQUES.md
→ Chercher fiche correspondante
→ Suivre étapes (copy-paste 80% du code)

BLOCAGE 3: "Ça compile pas"
→ Vérifier indentation C# (4 espaces)
→ Vérifier ; fin de ligne
→ Vérifier using statements
→ Lire erreur exacte de compiler

BLOCAGE 4: "Ça marche pas au runtime"
→ Vérifier Swagger vs Frontend URLs
→ Vérifier CORS pas bloqué
→ Vérifier DB ok (Customers table existe?)
→ Vérifier pas de typo endpoint

BLOCAGE 5: "J'ai plus de temps"
→ Faire documentation rapidement
→ Screenshots même basiques
→ Expliquer oralement ce qui manque
→ Jury valorise honhêteté
```

---

## 📊 GESTION TEMPS (60 min)

```
00-05 min : Comprendre demande + consulter fiche
05-30 min : Implémenter modifications (25 min)
30-45 min : Tester tout (15 min)
45-60 min : Documenter + finitions (15 min)

↓↓↓

Si vous êtes en RETARD:
→ Stop coding à 45 min
→ Force documenter / screenshots
→ Jury vaut mieux du "incomplet+documenté" que "terminé+rien documenté"

Si vous FINISSEZ tôt (rare):
→ Tester plus
→ Ajouter autre petite feature facile
→ Améliorer documentation
→ JAMAIS faire rien! (suspicion)
```

---

## 🔑 ATTITUDES À AVOIR

### ✅ À FAIRE

```
✅ Écouter attentivement
✅ Poser questions si doute
✅ Consulter fiches (c'est autorisé!)
✅ Tester consciencieusement
✅ Documenter au fur/mesure
✅ Expliquer votre logique
✅ Montrer rigueur
✅ Rester calme face erreurs
✅ Dire honnêtement "je sais pas, je vais voir"
```

### ❌ À ÉVITER

```
❌ Coder sans réfléchir
❌ Ignorer tests
❌ Oublier documentation
❌ Paniquer sur erreurs (NORMAL!)
❌ Inventer solutions
❌ Déranger jury sans raison
❌ Faire du code pas maintenu
❌ Garder silencieux (jury veut vous entendre)
```

---

## 💡 TIPS EXPRESS

### Copier-coller code efficace

```
1. Ouvrir document (MD ou PDF)
2. Copy code bloc
3. Coller dans IDE
4. Adapter:
   - Noms variables
   - Endpoints URL
   - Paramètres
5. Sauvegarder
6. Build
```

### Chercher fichier rapide

```
Rider:
Ctrl+N → Taper nom fichier → Entrée

VS Code:
Ctrl+P → Taper nom fichier → Entrée
```

### Tester endpoint express

```
1. Swagger: https://localhost:5001/swagger
2. Chercher endpoint
3. "Try it out"
4. Remplir paramètres
5. "Execute"
6. Vérifier Response 200 OK
```

---

## 🚀 À PARTIR DE 9:05

1. **JURY PARLE** → ÉCOUTEZ 100%
2. **JURY DEMANDE** → Demandez clarifications
3. **VOUS OUVREZ FICHES** → Cherchez correspondance
4. **VOUS CODEZ** → Suivez fiche exactement
5. **VOUS TESTEZ** → Vérifiez partout
6. **VOUS DOCUMENTEZ** → Screenshots + explications
7. **VOUS MONTREZ** → Présentez résultats

**C'EST TOUT!** Vous avez ça. 💪

---

## 📋 AVANT DE LAISSER CE DOCUMENT

```
☐ Vous avez les 5 fiches imprimées? (Pagination, Recherche, Auth, Tri, Export)
☐ Vous avez le Cheat Sheet?
☐ Vous avez Guide Navigation?
☐ Vous avez appsettings.json + Program.cs sous les yeux?
☐ Vous avez testéquel jour projet fonctionne?
☐ Vous avez créé backup?
☐ Vous savez où clicker pour:
   - Ouvrir Swagger
   - Ouvrir Frontend
   - Ouvrir IDE
   - Ouvrir Terminal
☐ Vous connaissez keyboard shortcuts:
   - Ctrl+S (sauvegarder)
   - Ctrl+B (build)
   - Ctrl+N (ouvrir fichier Rider)
   - Ctrl+P (ouvrir fichier VS Code)
```

---

## 🎯 LAST BUT IMPORTANT

**Jury teste pas juste votre code.**

Jury évalue:

```
✅ Compréhension projet (parlez de votre logic)
✅ Rigueur (testez vraiment)
✅ Communication (expliques vos choix)
✅ Gestion temps (pas tout crasher à 55 min)
✅ Documentation (écrivez!)
✅ Honhêteté (dites si quelque chose pas clair)
```

**Plus important que le code parfait** = **Démarche professionnelle + doc clear**

---

## 🏆 VOUS ÊTES PRÊT

Vous avez:

✅ Tous les codes template (fiches)
✅ Tous les chemins fichiers (navigation)
✅ Tous les commandes (Windows Server)
✅ Tous les templates doc (documentation)
✅ Ce quick start (pour pas paniquer)

**Il n'y a AUCUNE RAISON que ça échoue.**

---

## 📱 1 DERNIÈRE CHOSE

Si tout crach complètement (rare), vous avez:

```
Plan B: Backup projet
→ Copy-Item -Recurse C:\Desktop\E6_Backup\Projet...

Plan C: Redeployer depuis Git
→ git clone [repo]
→ dotnet restore
→ npm install
```

Vous êtes couvert. 🛡️

---

**Quick Start 5 Min - À LIRE 9:00-9:05 AM**

Ensuite, montrez au jury de quoi vous êtes capable!

**BON CHANCE! 🚀**
