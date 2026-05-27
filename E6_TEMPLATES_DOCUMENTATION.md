# 📝 GUIDE DOCUMENTATION & TEMPLATES
## À REMPLIR À CHAQUE MODIFICATION - Exigence E6

---

## ⚠️ RÈGLE E6 IMPORTANTE

**Toute modification doit être documentée** ✅

```
Sans documentation = Points perdus
Avec documentation claire = Points bonus
```

Vous serez évalué sur :
- ✅ Code implémenté
- ✅ Tests effectués
- ✅ **DOCUMENTATION produite** ← CLÉS
- ✅ Explication orale

---

## 📋 TEMPLATE GÉNÉRAL DOCUMENTATION

### À remplir après CHAQUE évolution

```markdown
# [TITRE ÉVOLUTION]
Date: [aujourd'hui]
Candidat: [Votre nom]

## 1. OBJECTIF
[Qu'est-ce qu'on voulait faire?]
Exemple: "Ajouter pagination backend pour scalabilité"

## 2. CONTEXTE
[Pourquoi c'était nécessaire?]
Exemple: "Actuellement API retourne TOUS les clients (1000+)"

## 3. FICHIERS MODIFIÉS
- [Fichier 1] - Ligne X à Y - [Changement]
- [Fichier 2] - Ligne A à B - [Changement]
- [Fichier 3] - CRÉÉ - [Nouveau fichier]

Exemple:
- CustomerService.cs (ligne 45-65) - Ajout méthode GetPagedAsync
- CustomerController.cs (ligne 30-42) - Ajout endpoint /paged
- api.ts (ligne 25-35) - Ajout fonction getPagedCustomers

## 4. MODIFICATIONS DÉTAILLÉES

### 4.1 Fichier: CustomerService.cs
**Localisation**: Fin classe, avant dernier }
**Code ajouté**:
\`\`\`csharp
public async Task<(IEnumerable<Customer>, int)> GetPagedAsync(int page, int size)
{
    // Code ici
}
\`\`\`

### 4.2 Fichier: CustomerController.cs
**Localisation**: Nouvelle méthode GET
**Code ajouté**:
\`\`\`csharp
[HttpGet("paged")]
public async Task<ActionResult> GetPagedAsync(int page = 1, int size = 20)
{
    // Code ici
}
\`\`\`

## 5. TESTS EFFECTUÉS
- ✅ Backend compile (dotnet build OK)
- ✅ Endpoint accessible Swagger
- ✅ Paramètres acceptés (page=1&size=20)
- ✅ Réponse JSON correcte
- ✅ Frontend reçoit données OK
- ✅ Affichage pagination OK

## 6. RÉSULTATS OBSERVÉS
\`\`\`
Avant: GET /Customer → 1000 clients (lent)
Après: GET /Customer/paged?page=1&size=20 → 20 clients (rapide)
Response: { pageNumber: 1, pageSize: 20, total: 1000, pages: 50, items: [...] }
\`\`\`

## 7. PROBLÈMES RENCONTRÉS
- ❌ [Problème] → Solution
- ❌ [Problème] → Solution

Exemple:
- ❌ Skip/Take mal placés → Déplacé après OrderBy
- ❌ Total query non optimisé → Utilisé CountAsync séparément

## 8. IMPACTS
- Fichiers affectés: [Lister]
- Endpoints concernés: [Lister]
- Configuration changée: [Décrire]
- Performance: [Amélioration/Dégradation]

## 9. COMMANDES UTILES
\`\`\`bash
# Backend
dotnet build                          # Compiler
dotnet run                           # Lancer serveur
dotnet clean                         # Nettoyer

# Frontend
npm install                          # Installer dépendances
npm run dev                          # Lancer dev server
npm run build                        # Build production
\`\`\`

## 10. RÉFÉRENCES
- Fiche évolution: FICHE 1
- Documentation API: [URL Swagger]
- Commit Git: [Hash si git]
```

---

## 🔖 TEMPLATES SPÉCIFIQUES PAR MODIFICATION

### TEMPLATE 1 : Ajouter Endpoint

```markdown
# Ajout Endpoint API

## Objectif
Créer nouvel endpoint REST pour [fonction]

## Endpoint Details
- **URL**: GET /Customer/[route]
- **Paramètres**: [Lister]
- **Response**: [Décrire structure]
- **Authentification**: [Oui/Non]

## Code modifié
\`\`\`csharp
[HttpGET/POST/etc]
[Route("/path")]
public async Task<ActionResult> NomFonction([parametres])
{
    // Votre code
}
\`\`\`

## Swagger Test
1. URL : https://localhost:5001/swagger
2. Chercher endpoint [NOM]
3. Résultat: [200/400/etc]
```

---

### TEMPLATE 2 : Ajouter Logique Business

```markdown
# Ajout Logique Métier

## Objectif
Implémenter [logique] dans service

## Fichiers modifiés
1. **ICustomerService.cs** - Interface
   Task<[ReturnType]> FunctionName([params]);

2. **CustomerService.cs** - Implémentation
   ```csharp
   public async Task<[ReturnType]> FunctionName([params])
   {\n    // Logique\n   }
   ```

## Logique détaillée
- Étape 1: [Décrire]
- Étape 2: [Décrire]
- Étape 3: [Décrire]

## Tests
- Cas normal: [Résultat attendu]
- Cas erreur: [Comportement]
```

---

### TEMPLATE 3 : Modifier Frontend

```markdown
# Modification Composant Frontend

## Composant: [Nom].vue

### Modifications visuelles
**Avant**: [Décrire ou screenshot]
**Après**: [Décrire ou screenshot]

### Code modifié
**Fichier**: src/components/[Nom].vue

#### Template changes
\`\`\`vue
<!-- Code HTML/Vue modifié -->
\`\`\`

#### Script changes
\`\`\`typescript
// Code TypeScript modifié
\`\`\`

#### Style changes
\`\`\`css
/* Code CSS modifié */
\`\`\`

## Tests Frontend
- Compilation: [OK/Erreur]
- Affichage: [OK/Problème]
- Interactions: [OK/Problème]
- Console JS: [Sans erreur/Erreurs]
```

---

### TEMPLATE 4 : Ajouter Configuration

```markdown
# Ajout Configuration

## Fichier: appsettings.json

### Avant
\`\`\`json
{
  // Config avant
}
\`\`\`

### Après
\`\`\`json
{
  // Config après
}
\`\`\`

## Utilisation dans code
**Fichier**: Program.cs

\`\`\`csharp
var config = builder.Configuration.GetSection("SectionName");
var value = config["KeyName"];
\`\`\`

## Impact
- Nécessite rebuild: [Oui/Non]
- Secrets à gérer: [Lister]
```

---

### TEMPLATE 5 : Sécurité/Auth

```markdown
# Modification Sécurité

## Changement implémenté
[Décrire modification sécurité]

## Avant (Vulnérabilité)
\`\`\`
❌ [Problème sécurité]
\`\`\`

## Après (Sécurisé)
\`\`\`
✅ [Solution implémentée]
\`\`\`

## Code modifié
\`\`\`csharp
[Code sécurité]
\`\`\`

## Tests sécurité
- [ ] Authentification requise
- [ ] Pas d'accès non-autorisé
- [ ] Tokens valides
- [ ] Données sensibles encryptées
```

---

## 📸 SCREENSHOTS À INCLURE

Pour chaque modification, inclure :

### 1. Screenshot Swagger

```
Menu: Components
Action: Prendre screenshot endpoint
Inclure: Requête + Réponse
Dossier: docs/screenshots/
Nom: 01_endpoint_nouveau.png
```

### 2. Screenshot Frontend

```
Interface: Nouveau feature visible
Action: Prendre screenshot UI
Inclure: Avant/Après
Dossier: docs/screenshots/
Nom: 02_ui_nouveau_feature.png
```

### 3. Screenshot Tests

```
Console: Tests validés
Action: Prendre screenshot console
Inclure: Résultats tests
Dossier: docs/screenshots/
Nom: 03_tests_ok.png
```

---

## 📂 STRUCTURE DOCUMENTATION PRODUITE

À créer pendant exam :

```
docs/
├─ MODIFICATIONS_E6.md          ← Document principal
├─ screenshots/
│  ├─ 01_endpoint_pagination.png
│  ├─ 02_ui_nouvelle.png
│  └─ 03_tests.png
├─ API_PAGINATION.md            ← Détails technique
├─ CONFIGURATION_SECURITY.md    ← Si auth ajoutée
└─ TESTS_EXECUTED.md            ← Résumé tests
```

---

## 🎯 CHECKLIST DOCUMENTATION

Avant fin exam, vérifier :

```
☐ Document principal rempli (MODIFICATIONS_E6.md)
☐ Tous objectifs documentés
☐ Fichiers modifiés listés avec lignes
☐ Code changes expliqué
☐ Tests décrits et validés
☐ Screenshots inclus
☐ Problèmes rencontrés documentés
☐ Solutions expliquées
☐ Temps estimation correct
☐ Format lisible/complet
```

---

## ⏱️ TEMPS DOCUMENTATION

**Estimation par évolution** :

```
Petite évolution (ex: Tri)
→ 3-5 min documentation

Moyenne évolution (ex: Recherche)
→ 5-8 min documentation

Grande évolution (ex: Auth JWT)
→ 10-15 min documentation
```

**Total 60 min** :
- 40 min : Code + Tests
- 15 min : Documentation
- 5 min : Buffer/Urgencies

---

## 💡 TIPS DOCUMENTATION

### Écrire POUR le jury

```
❌ Technique trop complexe
✅ Clair et compréhensible

❌ "J'ai fait un truc"
✅ "J'ai implémenté pagination avec Skip/Take EF Core"

❌ Pas de précisions
✅ "Modified method GetAllAsync at line 45-65"
```

### Structurer logiquement

```
1️⃣ Quoi : Objectif clair
2️⃣ Pourquoi : Context/motivation
3️⃣ Où : Fichiers précis + lignes
4️⃣ Comment : Code + explications
5️⃣ Tests : Validation
6️⃣ Résultats : Avant/Après
```

### Utiliser images

```
Code long → OK pour screenshot
Complex flow → Diagram/schéma
UI changes → Screenshot comparatif
Erreur rencontrée → Screenshot console
```

---

## 📋 DOCUMENT FINAL À LIVRER

**Jour de l'exam - À remettre à jury** :

```
FICHIER: E6_MODIFICATIONS_REALISEES.md

STRUCTURE:
1. Couverture
   - Candidat: [Nom]
   - Date: [Aujourd'hui]
   - Epreuve: E6
   
2. Résumé modifications
   - Évolution 1: [Titre] - [Temps]
   - Évolution 2: [Titre] - [Temps]
   
3. Pour chaque modification
   - Objectif
   - Fichiers modifiés (avec lignes)
   - Code + explications
   - Screenshots
   - Tests
   - Résultats
   
4. Conclusion
   - Objectifs atteints: [OUI/PARTIEL/NON]
   - Problèmes résolus: [Lister]
   - Améliorations possibles: [Lister]
```

---

## 🖨️ IMPRIMER AVANT E6

Imprimer les templates :
```
☐ Template général
☐ Template endpoint
☐ Template logique
☐ Template frontend
☐ Checklist documentation
```

À avoir physiquement lors exam (pas de traitement de texte = pas autorisé probablement)

---

**Templates Documentation E6**

*Exigence importante pour évaluation - À remplir à chaque modification*
