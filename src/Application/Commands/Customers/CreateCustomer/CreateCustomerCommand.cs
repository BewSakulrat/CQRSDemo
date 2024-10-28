using Application.DTOs.Customer;
using MediatR;

namespace Application.Commands.Customers.CreateCustomer;

public class CreateCustomerCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public string Email { get; set; }
    
}