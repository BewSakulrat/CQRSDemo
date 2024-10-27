using Application.DTOs.Customer;
using Domain.Entities;

namespace Application.Interfaces;

public interface ICustomerRepository
{
    Task<CustomerDto> GetCustomerByIdAsync(Guid customerId);
    Task<IEnumerable<CustomerDto>> GetCustomersAsync();    
    Task AddAsync(CreateCustomerDto customerDto);
    Task UpdateAsync(UpdateCustomerDto customerDto);
    Task DeleteAsync(Guid customerId);
}