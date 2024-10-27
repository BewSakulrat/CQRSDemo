using Application.DTOs.Customer;
using MediatR;

namespace Application.Queries.Customers.GetAllCustomers;

public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerDto>>
{
    
}