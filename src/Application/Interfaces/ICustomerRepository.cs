using Application.DTOs.Customer;
using Domain.Entities;

namespace Application.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> GetByIdAsync(Guid customerId);
    Task<IEnumerable<CustomerDto>> GetAllAsync();    
    Task<Guid> AddAsync(Customer customer);
    Task<Customer> UpdateAsync(Customer customer);
    Task DeleteAsync(Guid customerId);
}