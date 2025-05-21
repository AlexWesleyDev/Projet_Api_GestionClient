using CustomerApi.Models;

namespace CustomerApi.SERVICES
{
   public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(Guid id);
        Task<Customer> CreateAsync(Customer customer);
        Task<bool> DeleteAsync(Guid id);
    }
}