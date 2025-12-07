using AdapterPatternDemo.Models;

namespace AdapterPatternDemo.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(Customer customer);
}