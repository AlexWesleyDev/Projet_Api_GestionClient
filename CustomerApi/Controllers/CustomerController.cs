using CustomerApi.Models;
using CustomerApi.SERVICES;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using CustomerApi.Data;


// PARTIE DE CONFIGURATION SOLID

namespace CustomerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(Guid id)
        {
            var customer = await _service.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            var created = await _service.CreateAsync(customer);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        
        
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> Update(Guid id, Customer updatedCustomer)
        {
            if (id != updatedCustomer.Id)
            {
                return BadRequest("L'ID dans l'URL ne correspond pas à celui du client envoyé.");
            }
        
            var existingCustomer = await _service.GetByIdAsync(id);
            if (existingCustomer == null)
            {
                return NotFound($"Aucun client trouvé avec l'ID {id}.");
            }
        
            var result = await _service.UpdateAsync(updatedCustomer);
            return Ok(result);
        }
        

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return Ok($"Client avec ID {id} supprimé.");
        }
    }
}




















































































/*         //PARTIE AEVC LA BASE DE DONNEES

namespace CustomerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
            
        // Injection du CustomerContext dans le controlleur
        
        private readonly CustomerContext _context;

        public CustomerController(CustomerContext context)
        {
            _context = context;
        }
        
        // Affichage des clients présents dans la base de données
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }
        
        // Affiche le client selon l'ID selectionné
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();
            return customer;
        }
        
        // Ajouter un nouveau client dans la BDD

        [HttpPost]
        public async Task<ActionResult<Customer>> Create(Customer newCustomer)
        {
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = newCustomer.Id }, newCustomer);
        }

        // Supprimer un client de la BDD
        [HttpDelete]
        public async Task<ActionResult<Customer>> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound("Client introuvable");
            
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            
            string chaineInformations = "\n ID : " + customer.Id + "\n Nom : " + customer.Nom +  "\n Prénom : " + customer.Prenom + "\n Email : " + customer.Email +
                                        "\n Téléphone : " + customer.Telephone + "\n Adresse : " + customer.Adresse +  "\n Ville : " + customer.Ville + 
                                        "\n Code Postal : " + customer.Codepostal +  "\n Date de création : " + customer.Datecreation ;
            
            return Ok("Le Client (Customer) supprimé dont les informations sont : " + chaineInformations + "\n \n Suppression réussie !");
        }
        
        // Modifier un client
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> Update(int id, Customer updatedCustomer)
        {
            var customer = await _context.Customers.FindAsync(id);
            
            if (customer == null)
                return NotFound("Client introuvable.");

            // Mise à jour des champs
            customer.Nom = updatedCustomer.Nom;
            customer.Prenom = updatedCustomer.Prenom;
            customer.Email = updatedCustomer.Email;
            customer.Telephone = updatedCustomer.Telephone;
            customer.Adresse = updatedCustomer.Adresse;
            customer.Ville = updatedCustomer.Ville;
            customer.Codepostal = updatedCustomer.Codepostal;
            customer.Datecreation = updatedCustomer.Datecreation;
            
            // Actualiser en synchronisant les informations.
            
            await _context.SaveChangesAsync();
            
            string chaineInformations = "\n ID : " + customer.Id + "\n Nom : " + customer.Nom +  "\n Prénom : " + customer.Prenom + "\n Email : " + customer.Email +
                                        "\n Téléphone : " + customer.Telephone + "\n Adresse : " + customer.Adresse +  "\n Ville : " + customer.Ville + 
                                        "\n Code Postal : " + customer.Codepostal +  "\n Date de création : " + customer.Datecreation ;
            
            return Ok("Le Client (Customer) modifié dont les informations sont : " + chaineInformations + "\n \n Modification réussie !");
        } 

    }
}

*/







// --------------------------------------------------------------------------------------------------------------------------------------
// --------------------------------------------------------------------------------------------------------------------------------------






/*
         PARTIE SANS CONNECTION A UNE BASE DE DONNEES
         
         private static List<Customer> customers = new()
        {
            new Customer { Id = 1, Nom = "Wesley", Email = "wesley@gmail.com" },
            new Customer { Id = 2, Nom = "Alexandre", Email = "alxnadre@gmail.com" , Telephone = "01 23 45 67 89"},
        };
        
        //Liste des clients

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAll()
        {
            return Ok(customers);
        }
        
        // Détail d’un client selon l'ID saisi

        [HttpGet("{id}")]
        public ActionResult<Customer> GetById(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }
        
        // Ajouter un client

        [HttpPost]
        public ActionResult<Customer> Post(Customer customer)
        {
            customer.Id = customers.Max(c => c.Id) + 1;
            customers.Add(customer);
            return CreatedAtAction(nameof(Post), new { id = customer.Id }, customer);
        }
        
        // Supprimer un client
        [HttpDelete]
        public ActionResult<Customer> Delete(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();
            string chaineInformations = "\n ID : " + customer.Id + "\n Nom : " + customer.Nom + "\n Email : " + customer.Email +  "\n Téléphone : " + customer.Telephone;   
            customers.Remove(customer);
            return Ok("Le Client (Customer) sélectionné dont les informations sont : " + chaineInformations + "\nSuppression réussie !");
        }
        
        // Modifier un client
        [HttpPut("{id}")]
        public IActionResult Update(int id, Customer updatedCustomer)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound("Client introuvable.");

            // Mise à jour des champs
            customer.Nom = updatedCustomer.Nom;
            customer.Email = updatedCustomer.Email;
            customer.Telephone = updatedCustomer.Telephone;
            
            string chaineInformations = "\n ID : " + customer.Id + "\n Nom : " + customer.Nom + "\n Email : " + customer.Email +  "\n Téléphone : " + customer.Telephone;   
            return Ok("Le Client (Customer) modifié dont les informations sont : " + chaineInformations + "\n Modification réussie !");
        } */
