using Application.DTOs.Customer;
using MediatR;

namespace Application.Commands.Customers.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<CustomerDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}