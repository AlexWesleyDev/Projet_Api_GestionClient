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
            => await _context.Customers.ToListAsync();

        public async Task<Customer?> GetByIdAsync(Guid id)
            => await _context.Customers.FindAsync(id);

        public async Task<Customer> CreateAsync(Customer customer)
        {
            customer.Datecreation = DateTime.Now;
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return false;
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Customer?> UpdateAsync(Customer customer)
        {
            var updatedRows = await _context.Customers
                .Where(c => c.Id == customer.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(c => c.Name, customer.Name)
                    .SetProperty(c => c.Firstname, customer.Firstname)
                    .SetProperty(c => c.Email, customer.Email)
                    .SetProperty(c => c.Phonenumber, customer.Phonenumber)
                    .SetProperty(c => c.Adress, customer.Adress)
                    .SetProperty(c => c.City, customer.City)
                    .SetProperty(c => c.Adresscode, customer.Adresscode)
                );

            if (updatedRows > 0)
                return customer;
            return null;
        } } }