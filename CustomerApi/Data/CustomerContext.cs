using CustomerApi.Models;
using CustomerApi.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options) { }
        public DbSet<Customer> Customers { get; set; }
    }
}
