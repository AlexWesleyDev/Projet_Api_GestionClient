# 📋 E6 GUIDE D'ÉVOLUTIONS PRACTIQUES - À EMPORTER À L'EXAM
## Fiches d'actions rapides pour les modifications/évolutions demandées

---

## ⚠️ IMPORTANT AVANT EXAM

**Demain 9h - Épreuve E6**

### Timing Épreuve
```
T : Préparation (30 min) - Analyse besoins
E1 : Entretien 1 (20 min) - Questions jury
--- : Réalisation objectifs (60 min) - C'EST ICI QUE VOUS MODIFIEZ
E2 : Entretien 2 (20 min) - Recette/validation
H : Harmonisation
```

**Pendant phase de réalisation (60 min)** :
- Jury demande modification/évolution
- Vous devez **comprendre la demande**
- **Implémenter la solution**
- **Documenter votre travail**

---

## 🎯 STRUCTURE DE CES FICHES

Chaque fiche contient :
1. **Titre évolution** - Quoi demander?
2. **Analyse** - Comprendre demande
3. **Fichiers à modifier** - Où exactement?
4. **Étapes implementation** - Pas à pas
5. **Code exemple** - Prêt à copier
6. **Documentation** - Ce à écrire
7. **Temps estimé** - Pour 60 min?
8. **Validation** - Tester quoi?

---

---

# 🔧 FICHE 1 : AJOUTER PAGINATION BACKEND

## Contexte
Actuellement : `GET /Customer` retourne TOUS les clients (pas scalable)

**Demande possible** : "Implémenter pagination côté serveur avec 20 clients par page"

---

## 📍 Fichiers à modifier

```
1. CustomerApi/Services/ICustomerService.cs      → Ajouter interface
2. CustomerApi/Services/CustomerService.cs       → Implémenter logique
3. CustomerApi/Controllers/CustomerController.cs → Ajouter endpoint
```

---

## 🔨 ÉTAPES IMPLEMENTATION (20 minutes)

### Étape 1 : Modifier ICustomerService (2 min)

**Fichier** : `CustomerApi/Services/ICustomerService.cs`

**Ajouter cette méthode** :

```csharp
// À ajouter après GetAllAsync()
Task<(IEnumerable<Customer> Items, int Total)> GetPagedAsync(int pageNumber, int pageSize);
```

**Code complet à ajouter** :
```csharp
using CustomerApi.Models;

namespace CustomerApi.SERVICES
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(Guid id);
        Task<Customer> CreateAsync(Customer customer);
        Task<bool> DeleteAsync(Guid id);
        Task<Customer> UpdateAsync(Customer customer);
        
        // NOUVELLE MÉTHODE
        Task<(IEnumerable<Customer> Items, int Total)> GetPagedAsync(int pageNumber, int pageSize);
    }
}
```

---

### Étape 2 : Implémenter dans CustomerService (3 min)

**Fichier** : `CustomerApi/Services/CustomerService.cs`

**Ajouter cette implémentation** à la fin de la classe :

```csharp
public async Task<(IEnumerable<Customer> Items, int Total)> GetPagedAsync(int pageNumber, int pageSize)
{
    // Validation
    if (pageNumber < 1) pageNumber = 1;
    if (pageSize < 1) pageSize = 20;
    
    // Total count
    var total = await _context.Customers.CountAsync();
    
    // Skip + Take
    var items = await _context.Customers
        .OrderByDescending(c => c.Datecreation)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    
    return (items, total);
}
```

---

### Étape 3 : Ajouter endpoint Controller (3 min)

**Fichier** : `CustomerApi/Controllers/CustomerController.cs`

**Ajouter cette méthode** :

```csharp
[HttpGet("paged")]
public async Task<ActionResult> GetPagedAsync(int pageNumber = 1, int pageSize = 20)
{
    try
    {
        var (items, total) = await _service.GetPagedAsync(pageNumber, pageSize);
        
        var response = new
        {
            pageNumber,
            pageSize,
            total,
            pages = (int)Math.Ceiling((double)total / pageSize),
            items
        };
        
        return Ok(response);
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

---

### Étape 4 : Modifier Frontend (5 min)

**Fichier** : `Vue_Customer/src/api.ts`

**Ajouter** :

```typescript
// Ajouter après export défaut api
export const getPagedCustomers = async (pageNumber: number, pageSize: number = 20) => {
    try {
        const response = await api.get(`/Customer/paged`, {
            params: {
                pageNumber,
                pageSize
            }
        });
        return response.data;
    } catch (error) {
        console.error('Error fetching paged customers:', error);
        throw error;
    }
};
```

**Fichier** : `Vue_Customer/src/components/CustomerList.vue`

**Utiliser nouveau endpoint** :
```typescript
// Remplacer ancienne requête
const fetchCustomers = async () => {
    try {
        const data = await getPagedCustomers(currentPage.value);
        customers.value = data.items;
        totalPages.value = data.pages;
    } catch (err) {
        console.error('Erreur chargement clients :', err);
    }
};
```

---

## ✅ VALIDATION (2 min)

**Tester** :
```
1. Ouvrir Swagger : https://localhost:5001/swagger
2. Trouver endpoint : GET /Customer/paged
3. Tester avec : pageNumber=1&pageSize=20
4. Vérifier réponse : { pageNumber: 1, total: X, pages: Y, items: [...] }
5. Frontend → charger page 1 OK?
```

**Résultat attendu** :
```json
{
  "pageNumber": 1,
  "pageSize": 20,
  "total": 150,
  "pages": 8,
  "items": [
    { "id": "...", "name": "Dupont", ... }
  ]
}
```

---

## 📝 DOCUMENTATION À AJOUTER

**Créer/Modifier** : `docs/API_PAGINATION.md`

```markdown
# Pagination API

## Endpoint

`GET /Customer/paged?pageNumber=1&pageSize=20`

## Response

```json
{
  "pageNumber": 1,
  "pageSize": 20,
  "total": 150,
  "pages": 8,
  "items": [...]
}
```

## Implémentation

- Backend : CustomerService.GetPagedAsync()
- Frontend : getPagedCustomers()
- Modified: CustomerList.vue
```

---

## ⏱️ TEMPS TOTAL : ~15 minutes

---

---

# 🔍 FICHE 2 : AJOUTER RECHERCHE BACKEND

## Contexte
Actuellement : Recherche faite côté client (Vue)

**Demande possible** : "Implémenter recherche côté serveur pour scalabilité"

---

## 📍 Fichiers à modifier

```
1. CustomerService.cs      → Ajouter méthode SearchAsync
2. CustomerController.cs   → Ajouter endpoint [HttpGet("search")]
3. api.ts                  → Ajouter fonction searchCustomers
4. CustomerList.vue        → Utiliser nouvelle recherche
```

---

## 🔨 ÉTAPES IMPLEMENTATION (15 minutes)

### Étape 1 : Ajouter ICustomerService (1 min)

**Fichier** : `CustomerApi/Services/ICustomerService.cs`

```csharp
// Ajouter cette signature
Task<IEnumerable<Customer>> SearchAsync(string query);
```

---

### Étape 2 : Implémenter SearchAsync (3 min)

**Fichier** : `CustomerApi/Services/CustomerService.cs`

```csharp
public async Task<IEnumerable<Customer>> SearchAsync(string query)
{
    if (string.IsNullOrWhiteSpace(query))
        return await _context.Customers.ToListAsync();
    
    var lowerQuery = query.ToLower();
    
    return await _context.Customers
        .Where(c => 
            c.Name.ToLower().Contains(lowerQuery) ||
            c.Firstname.ToLower().Contains(lowerQuery) ||
            c.Email.ToLower().Contains(lowerQuery) ||
            c.City.ToLower().Contains(lowerQuery) ||
            c.Phonenumber.Contains(query)
        )
        .ToListAsync();
}
```

---

### Étape 3 : Ajouter endpoint Controller (2 min)

**Fichier** : `CustomerApi/Controllers/CustomerController.cs`

```csharp
[HttpGet("search")]
public async Task<ActionResult> SearchAsync(string query)
{
    if (string.IsNullOrWhiteSpace(query))
        return BadRequest("Query ne peut pas être vide");
    
    var results = await _service.SearchAsync(query);
    return Ok(results);
}
```

---

### Étape 4 : Modifier Frontend (3 min)

**Fichier** : `Vue_Customer/src/api.ts`

```typescript
export const searchCustomers = async (query: string) => {
    try {
        const response = await api.get('/Customer/search', {
            params: { query }
        });
        return response.data;
    } catch (error) {
        console.error('Search error:', error);
        throw error;
    }
};
```

**Fichier** : `Vue_Customer/src/components/SearchBar.vue`

```typescript
// Utiliser nouvelle fonction
const handleSearch = async (value: string) => {
    if (!value) {
        // Charger tous
        customers.value = await getCustomers();
    } else {
        // Chercher
        customers.value = await searchCustomers(value);
    }
};
```

---

## ✅ VALIDATION

**Tester dans Swagger** :
```
GET /Customer/search?query=dupont
→ Retourne clients avec "dupont" dans name/email/ville
```

---

## 📝 DOCUMENTATION

```markdown
# Recherche API

## Endpoint

GET /Customer/search?query=terme

## Champs recherchés
- Name
- Firstname
- Email
- City
- Phone

## Exemple

GET /Customer/search?query=Paris
```

---

## ⏱️ TEMPS TOTAL : ~12 minutes

---

---

# 🔐 FICHE 3 : AJOUTER AUTHENTIFICATION JWT

## Contexte
Actuellement : API sans authentification (CRITIQUE SÉCURITÉ)

**Demande possible** : "Sécuriser API avec JWT tokens"

---

## 📍 Fichiers à modifier

```
1. Program.cs                    → Configurer JWT
2. CustomerController.cs         → [Authorize] attribute
3. appsettings.json             → Ajouter JwtSettings
```

---

## 🔨 ÉTAPES IMPLEMENTATION (25 minutes)

### Étape 1 : Ajouter packages NuGet (3 min)

```bash
dotnet add package System.IdentityModel.Tokens.Jwt --version 7.0.0
dotnet add package Microsoft.IdentityModel.Tokens --version 7.0.0
```

---

### Étape 2 : Configurer Program.cs (5 min)

**Fichier** : `CustomerApi/Program.cs`

**Ajouter AVANT** `builder.Services.AddControllers()` :

```csharp
// JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"] ?? throw new InvalidOperationException());

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});
```

**Ajouter APRÈS** `app.UseHttpsRedirection()` :

```csharp
app.UseAuthentication();
```

---

### Étape 3 : Configurer appsettings (2 min)

**Fichier** : `CustomerApi/appsettings.json`

```json
{
  "JwtSettings": {
    "SecretKey": "your-very-long-secret-key-minimum-32-characters-here",
    "ExpiryMinutes": 60,
    "Issuer": "CustomerApi",
    "Audience": "CustomerApp"
  },
  ...
}
```

---

### Étape 4 : Ajouter [Authorize] au Controller (2 min)

**Fichier** : `CustomerApi/Controllers/CustomerController.cs`

**Ajouter** juste avant la classe :

```csharp
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    // Toutes les méthodes maintenant requièrent auth
}
```

---

### Étape 5 : Ajouter endpoint Login (5 min)

**Créer nouveau Controller** : `CustomerApi/Controllers/AuthController.cs`

```csharp
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CustomerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        
        public AuthController(IConfiguration config)
        {
            _config = config;
        }
        
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Simple auth (hardcoded pour démo)
            if (request.Username != "admin" || request.Password != "password")
                return Unauthorized("Credentials invalides");
            
            var jwtSettings = _config.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(jwtSettings["SecretKey"] ?? "")
            );
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Username)
            };
            
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiryMinutes"] ?? "60")),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
```

**Créer** : `CustomerApi/Models/LoginRequest.cs`

```csharp
namespace CustomerApi.Models
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
```

---

### Étape 6 : Modifier Frontend (5 min)

**Fichier** : `Vue_Customer/src/api.ts`

```typescript
// Ajouter fonction login
export const login = async (username: string, password: string) => {
    try {
        const response = await api.post('/Auth/login', { username, password });
        // Sauvegarder token
        localStorage.setItem('authToken', response.data.token);
        // Ajouter header
        api.defaults.headers.common['Authorization'] = `Bearer ${response.data.token}`;
        return response.data;
    } catch (error) {
        console.error('Login error:', error);
        throw error;
    }
};

// Charger token au startup
const token = localStorage.getItem('authToken');
if (token) {
    api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
}
```

---

## ✅ VALIDATION

**Tester** :
```
1. POST /Auth/login
   Body: { "username": "admin", "password": "password" }
   → Retourne JWT token

2. Copier token
   
3. GET /Customer
   Header: Authorization: Bearer [token]
   → OK (200)

4. GET /Customer (sans token)
   → Unauthorized (401)
```

---

## 📝 DOCUMENTATION

```markdown
# Authentication JWT

## Login

POST /Auth/login
Body: { "username": "admin", "password": "password" }
Response: { "token": "eyJ0..." }

## Utilisation

Headers: Authorization: Bearer [token]

Tous les endpoints /Customer nécessitent auth
```

---

## ⏱️ TEMPS TOTAL : ~25 minutes (LONG - à faire SI TEMPS)

---

---

# 📊 FICHE 4 : AJOUTER TRI/ORDERING

## Contexte
Impossible trier liste par colonne (Name, Date, etc.)

**Demande possible** : "Ajouter tri des clients par colonnes"

---

## 📍 Fichiers à modifier

```
1. ICustomerService.cs
2. CustomerService.cs
3. CustomerController.cs
4. Frontend (optionnel)
```

---

## 🔨 ÉTAPES IMPLEMENTATION (12 minutes)

### Étape 1 : Ajouter interface (1 min)

**Fichier** : `CustomerApi/Services/ICustomerService.cs`

```csharp
// Ajouter signature
Task<IEnumerable<Customer>> GetAllAsync(string? orderBy = "datecreation", bool ascending = false);
```

---

### Étape 2 : Implémenter sorting (3 min)

**Fichier** : `CustomerApi/Services/CustomerService.cs`

**Remplacer** GetAllAsync existante :

```csharp
public async Task<IEnumerable<Customer>> GetAllAsync(string? orderBy = "datecreation", bool ascending = false)
{
    var query = _context.Customers.AsQueryable();
    
    // Appliquer tri
    query = orderBy?.ToLower() switch
    {
        "name" => ascending ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name),
        "email" => ascending ? query.OrderBy(c => c.Email) : query.OrderByDescending(c => c.Email),
        "city" => ascending ? query.OrderBy(c => c.City) : query.OrderByDescending(c => c.City),
        _ => ascending ? query.OrderBy(c => c.Datecreation) : query.OrderByDescending(c => c.Datecreation)
    };
    
    return await query.ToListAsync();
}
```

---

### Étape 3 : Ajouter paramètres Controller (2 min)

**Fichier** : `CustomerApi/Controllers/CustomerController.cs`

**Modifier** endpoint GET :

```csharp
[HttpGet]
public async Task<ActionResult<IEnumerable<Customer>>> GetAll(
    string? orderBy = "datecreation",
    bool ascending = false)
    => Ok(await _service.GetAllAsync(orderBy, ascending));
```

---

## ✅ VALIDATION

**Tester** :
```
GET /Customer?orderBy=name&ascending=true
→ Clients triés par nom A→Z

GET /Customer?orderBy=datecreation&ascending=false
→ Clients plus récents d'abord
```

---

## ⏱️ TEMPS TOTAL : ~8 minutes

---

---

# 📤 FICHE 5 : AJOUTER EXPORT CSV

## Contexte
Pas d'export données clients

**Demande possible** : "Exporter liste clients en CSV"

---

## 📍 Fichiers à modifier

```
1. CustomerController.cs (ajouter endpoint)
2. Frontend (optionnel - ajouter bouton download)
```

---

## 🔨 ÉTAPES IMPLEMENTATION (15 minutes)

### Étape 1 : Ajouter endpoint Export (5 min)

**Fichier** : `CustomerApi/Controllers/CustomerController.cs`

```csharp
using System.Text;

[HttpGet("export-csv")]
public async Task<IActionResult> ExportCsv()
{
    var customers = await _service.GetAllAsync();
    
    var csv = new StringBuilder();
    csv.AppendLine("Id,Name,Firstname,Email,Phone,Address,City,PostalCode,DateCreation");
    
    foreach (var customer in customers)
    {
        csv.AppendLine($"\"{customer.Id}\",\"{customer.Name}\",\"{customer.Firstname}\"," +
            $"\"{customer.Email}\",\"{customer.Phonenumber}\",\"{customer.Adress}\"," +
            $"\"{customer.City}\",\"{customer.Adresscode}\",\"{customer.Datecreation:yyyy-MM-dd}\"");
    }
    
    var bytes = Encoding.UTF8.GetBytes(csv.ToString());
    return File(bytes, "text/csv", "clients.csv");
}
```

---

## ✅ VALIDATION

**Tester** :
```
1. GET /Customer/export-csv
2. Fichier CSV téléchargé
3. Ouvrir dans Excel
4. Vérifier colonnes et données OK
```

---

## ⏱️ TEMPS TOTAL : ~8 minutes

---

---

# 🎯 TABLEAU RÉCAPITULATIF RAPIDE

## Par Temps Disponible

### Si 60 minutes (temps normal E6)

**Prioriser** (dans cet ordre) :

```
1️⃣ FICHE 2 - Recherche (12 min)
   → Très demandé, facile

2️⃣ FICHE 4 - Tri (8 min)
   → Facile, rapide

3️⃣ FICHE 1 - Pagination (15 min)
   → Important scalabilité

= 35 minutes → Reste 25 min pour tests + documentation
```

---

### Si demande sécurité

```
➡️ FICHE 3 - JWT Auth (25 min)
   → Si jury demande sécurité
   → TRÈS BIEN pour E6
```

---

### Si demande data export

```
➡️ FICHE 5 - Export CSV (8 min)
   → Rapide, bon impression
```

---

## 📍 FICHIERS CLÉS À CONNAÎTRE

```
BACKEND (.NET) :
├─ Program.cs                 ← Configuration globale
├─ Controllers/
│  └─ CustomerController.cs   ← ENDPOINTS REST
├─ Services/
│  ├─ ICustomerService.cs    ← Interface (contrats)
│  └─ CustomerService.cs     ← Implémentation (logique)
├─ Data/
│  └─ CustomerContext.cs     ← DbContext (BD)
└─ Models/
   └─ Customer.cs            ← Entité

FRONTEND (Vue) :
├─ src/
│  ├─ api.ts                 ← Client HTTP
│  ├─ App.vue                ← Composant principal
│  └─ components/
│     ├─ CustomerList.vue    ← Affichage liste
│     └─ [autres]
└─ vite.config.ts            ← Config bundler

CONFIGURATION :
├─ appsettings.json          ← Settings app
├─ tailwind.config.js        ← Config CSS
└─ tsconfig.json             ← Config TypeScript
```

---

## 📝 TEMPLATE DOCUMENTATION À REMPLIR

Chaque évolution doit avoir documentation :

```markdown
# [TITRE ÉVOLUTION]

## Objectif
[Brève description]

## Fichiers modifiés
- [Fichier 1]
- [Fichier 2]

## Modifications apportées
[Détailler changements]

## Code ajouté
[Code clé]

## Tests effectués
[Validation faite]

## Résultats
[Ce qui marche]
```

---

## ⏱️ GESTION TEMPS RÉEL

```
T (30 min) : Préparation
  → Lire demandes jury
  → Analyser quoi faire
  → Ouvrir fiches ici
  
E1 (20 min) : Questions jury
  → Écouter bien
  
RÉALISATION (60 min) :
  00-05 min : Implémenter code
  05-50 min : Tester + valider
  50-60 min : Documenter + screenshots
  
E2 (20 min) : Recette
  → Montrer travail fait
```

---

## 🎯 STRATÉGIE RÉUSSITE

1. **Comprendre la demande** (5 min)
   - Bien écouter jury
   - Poser clarifications si besoin

2. **Consulter la fiche** (2 min)
   - Ouvrir cette ressource
   - Trouver demande dans tableau

3. **Suivre étapes** (40 min)
   - Copier-coller code
   - Adapter si besoin

4. **Tester immédiatement** (10 min)
   - Swagger/Frontend
   - Vérifier fonctionne

5. **Documenter** (3 min)
   - Écrire modifications
   - Preuves (screenshots)

---

## 🚨 POINTS CRITIQUES À RETENIR

❌ **À ÉVITER**
- Inventer solutions (suivre fiches)
- Oublier documentation
- Tester juste mentalement (vérifier réellement)
- Panicker si erreur (normal, trouver solution)

✅ **À FAIRE**
- Avoir ces fiches imprimées DEMAIN
- Lire calmement la demande
- Suivre pas-à-pas
- Tester chaque étape
- Documenter tout

---

## 📞 EN CAS DE PROBLÈME

### "Ça compile pas"

```bash
# Vérifier syntaxe C#
cd CustomerApi
dotnet build

# Erreurs usuelles :
# - Oubli ; à fin ligne
# - Oubli using statements
# - Oubli parenthèse
```

### "Ça marche pas au runtime"

```
1. Vérifier logs
2. Swagger → tester endpoint
3. Frontend console → erreurs JS
4. Vérifier DB connection
```

### "Je sais pas comment continuer"

```
1. Relire fiche entière
2. Vérifier tous fichiers modifiés
3. Redemander clarification jury si confus
```

---

**Ressources Évolutions E6 - À IMPRIMER ET EMPORTER DEMAIN**

*Épreuve 9h - Soyez prêt(e)!* 💪
