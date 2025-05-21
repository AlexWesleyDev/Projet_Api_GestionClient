using CustomerApi.Models;
using CustomerApi.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) //Tentaive de connexion à la base de données.
            : base(options) { }
        
       // protected override void OnConfiguring(DbContextOptionsBuilder options) // On définit que seule la classe utilise cette méthode pour l'intégrer directement dans la base.
       //     => options.UseSqlite("Data Source=Customers.db");
        
        public DbSet<Customer> Customers { get; set; }
        
    }
}
