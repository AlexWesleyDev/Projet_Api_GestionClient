# 📇 CHEAT SHEET E6 - PENSE-BÊTE RAPIDE
## À IMPRIMER (recto-verso) - Format petit pour emporter

---

# 🎯 SCÉNARIOS PROBABLES JURY

## Scénario 1 : "Ajouter recherche par ville"

```
1. Modifier ICustomerService.cs
   Task<IEnumerable<Customer>> SearchByCityAsync(string city);

2. Implémenter dans CustomerService.cs
   public async Task<...> SearchByCityAsync(string city)
   { return await _context.Customers
       .Where(c => c.City.ToLower() == city.ToLower())
       .ToListAsync(); }

3. Endpoint CustomerController.cs
   [HttpGet("search-city")]
   public async Task<ActionResult> SearchByCity(string city)
   { return Ok(await _service.SearchByCityAsync(city)); }

4. Test: Swagger → GET /Customer/search-city?city=Paris

⏱️ Temps: 10 min
```

---

## Scénario 2 : "Implémenter pagination backend"

```
1. Service: GetPagedAsync(int page, int size)
2. Controller: [HttpGet("paged")] avec params
3. Frontend: getPagedCustomers() dans api.ts
4. UI: CustomerList.vue utiliser new endpoint
5. Test: GET /Customer/paged?page=1&size=20

⏱️ Temps: 15 min
```

---

## Scénario 3 : "Ajouter tri par colonne"

```
1. Service: GetAllAsync(string orderBy, bool asc)
   → switch(orderBy) { case "name": ..., case "date": ... }
2. Controller: [HttpGet] avec paramètres orderBy, ascending
3. Test: GET /Customer?orderBy=name&ascending=true

⏱️ Temps: 12 min
```

---

## Scénario 4 : "Sécuriser API avec JWT"

```
1. Program.cs: AddAuthentication().AddJwtBearer()
2. appsettings.json: JwtSettings { SecretKey, ExpiryMinutes }
3. AuthController.cs: [HttpPost("login")]
4. CustomerController: [Authorize] sur classe
5. Test: POST /Auth/login → reçoit token
   Puis: GET /Customer + Bearer token

⏱️ Temps: 25 min ⚠️ LONG
```

---

## Scénario 5 : "Ajouter validation données"

```
1. Models/Customer.cs: Ajouter DataAnnotations
   [Required] string Name
   [EmailAddress] string Email
   [Phone] string Phonenumber

2. Controller: if (!ModelState.IsValid)
   return BadRequest(ModelState);

3. Test: POST avec données invalides → 400 Bad Request

⏱️ Temps: 8 min
```

---

## Scénario 6 : "Export CSV clients"

```
1. Controller: [HttpGet("export-csv")]
2. Code: Créer StringBuilder, AppendLine headers + data
3. Return File(bytes, "text/csv", "clients.csv")
4. Test: GET /Customer/export-csv → télécharge fichier

⏱️ Temps: 10 min
```

---

# ⚡ COMMANDES RAPIDES

## Lancement

```bash
# Terminal 1
cd .\CustomerApi
dotnet run

# Terminal 2
cd .\Vue_Customer
npm run dev
```

## Build

```bash
dotnet build                 # Compiler backend
npm run build               # Build frontend prod
dotnet clean                # Nettoyer build
```

## Debug

```bash
# URLs
http://localhost:5173      # Frontend
https://localhost:5001     # Backend
https://localhost:5001/swagger  # API docs

# Vérifier ports
netstat -ano | findstr :5001
netstat -ano | findstr :5173
```

---

# 📝 CODE TEMPLATES RAPIDES

## Nouveau Endpoint GET

```csharp
[HttpGet("route")]
public async Task<ActionResult> FunctionName(params)
{
    try {
        var result = await _service.MethodName(params);
        return Ok(result);
    }
    catch(Exception ex) {
        return BadRequest(ex.Message);
    }
}
```

## Nouvelle Méthode Service

```csharp
public async Task<ReturnType> MethodName(params)
{
    return await _context.Customers
        .Where(c => /* condition */)
        .ToListAsync();
}
```

## Appel API Frontend

```typescript
export const functionName = async (param) => {
    try {
        const response = await api.get('/endpoint', 
            { params: { param } });
        return response.data;
    } catch(error) {
        console.error('Error:', error);
        throw error;
    }
};
```

---

# 🔍 FICHIERS CLÉS À RETENIR

| Fichier | Utilité | Lien |
|---------|---------|------|
| `Program.cs` | Config globale DI | Voir onglet |
| `CustomerController.cs` | Endpoints REST | À modifier souvent |
| `CustomerService.cs` | Logique métier | À modifier souvent |
| `ICustomerService.cs` | Interfaces | Avant Service |
| `Customer.cs` | Entity/Model | Peu modifier |
| `appsettings.json` | Configuration | Secrets/Auth |
| `api.ts` | Client HTTP | À modifier |
| `CustomerList.vue` | UI Tableau | À modifier |

---

# ✅ CHECKLIST MODIFICATION

```
Avant de coder:
☐ Bien comprendre demande jury
☐ Consulter fiche évolution ici

Pendant coding:
☐ Modifier fichier source
☐ Respecter indentation/syntax
☐ Sauvegarder (Ctrl+S)
☐ Compiler/Build OK?

Tests:
☐ Backend: dotnet build OK?
☐ Frontend: npm run dev OK?
☐ Swagger: Endpoint visible?
☐ Test endpoint: Résultat OK?
☐ Frontend: UI correct?

Documentation:
☐ Documenter modifications
☐ Screenshots des tests
☐ Expliquer changements
```

---

# 🆘 PROBLÈMES COURANTS

| Erreur | Cause | Solution |
|--------|-------|----------|
| Port already in use | Processus utilise port | `taskkill /PID [num] /F` |
| dotnet: not found | .NET pas installé | Installer .NET 9 SDK |
| npm not found | Node pas installé | Installer Node.js LTS 18+ |
| Cannot reach API | Backend pas lancé | `dotnet run` |
| CORS error | Frontend bloqué | Vérifier CORS dans Program.cs |
| Database not found | Customer.db manquant | 1er `dotnet run` crée DB |
| Certificate error | SSL dev cert | `dotnet dev-certs https --trust` |

---

# 📊 TEMPS ESTIMATION (60 min)

```
Recherche simple (par ville)      → 10 min
Tri/Ordering                      → 12 min
Pagination backend                → 15 min
Export CSV                        → 10 min
Validation données                → 8 min
Authentication JWT (LONG)         → 25 min

Tests chaque modification         → 5 min
Documentation finale              → 5 min
```

**Stratégie** :
- 1-2 petites modifications (30 min)
- Tests complets (10 min)
- Documentation (15 min)
- Buffer (5 min)

---

# 🎯 PROCESSUS RÉUSSITE

```
1. ÉCOUTER (5 min)
   Jury demande → Bien comprendre
   
2. ANALYSER (3 min)
   Consulter fiche ici
   Ouvrir bons fichiers
   
3. IMPLÉMENTER (25 min)
   Suivre fiche
   Copy-paste code
   Adapter si besoin
   
4. TESTER (10 min)
   Swagger OK?
   Frontend OK?
   Données correctes?
   
5. DOCUMENTER (10 min)
   Remplir template
   Screenshots
   Explications
   
6. PRÉSENTER (7 min)
   Montrer résultats
   Expliquer modifications
   Montrer tests
```

---

# 🔑 CLÉS SUCCÈS

✅ **À FAIRE**
- Suivre fiches (pas inventer)
- Tester chaque étape
- Documenter tout
- Demander clarification si doute

❌ **À ÉVITER**
- Panicker face erreur (normal!)
- Modifications non testées
- Oublier documentation
- Coder sans fiches

---

# 📞 EN CAS DE BLOCAGE

**Q: Ça compile pas**
```
1. Vérifier syntaxe (;, }, etc)
2. dotnet build → voir erreur exacte
3. Consulter line number dans erreur
4. Fix et rebuild
```

**Q: Endpoint pas visible Swagger**
```
1. Backend relancé (dotnet run)?
2. [HttpGet] sur bonne méthode?
3. Pas de typo dans [Route]?
4. Chercher bon nom dans Swagger
```

**Q: Frontend affiche pas données**
```
1. Backend lancé?
2. Appel API correct (console F12)?
3. Réponse API OK (Swagger)?
4. frontend fetch() call correct?
```

**Q: Base de données vide**
```
1. Customer.db existe?
2. Ajouter quelques clients via UI
3. Rafraichir page
4. Data persiste?
```

---

# 🖨️ À IMPRIMER RECTO-VERSO

## Recto : Scénarios (cette page)
## Verso : Commandes + Templates + Checklist

---

**E6 Cheat Sheet - À emporter 28 Mai 2026 à 9h**

*Relisez avant exam = Confiance maximale!* 💪
