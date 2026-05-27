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
            var CustomerExistant = await _context.Customers.FindAsync(customer.Id);
            if (CustomerExistant == null) return null!;

            CustomerExistant.Name = customer.Name;
            CustomerExistant.Firstname = customer.Firstname;
            CustomerExistant.Email = customer.Email;
            CustomerExistant.Phonenumber = customer.Phonenumber;
            CustomerExistant.Adress = customer.Adress;
            CustomerExistant.City = customer.City;
            CustomerExistant.Adresscode = customer.Adresscode;

            await _context.SaveChangesAsync();
            return CustomerExistant;
            
        } } }