
using CustomerApi.Data;
using CustomerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.SERVICES
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerContext _context;

        public CustomerService(CustomerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            customer.Id = Guid.NewGuid(); // ✅ ID généré ici sous forme de guid
            customer.Datecreation = DateTime.Now;  // Date de création automatique initialisé
            
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<Customer> UpdateAsync(Customer customer)
        {
            var CustomerExistant = await _context.Customers.FindAsync(customer.Id);
            if (CustomerExistant == null) return null!;

            CustomerExistant.Nom = customer.Nom;
            CustomerExistant.Prenom = customer.Prenom;
            CustomerExistant.Email = customer.Email;
            CustomerExistant.Telephone = customer.Telephone;
            CustomerExistant.Adresse = customer.Adresse;
            CustomerExistant.Ville = customer.Ville;
            CustomerExistant.Codepostal = customer.Codepostal;

            await _context.SaveChangesAsync();
            return CustomerExistant;
        }
    }
}
