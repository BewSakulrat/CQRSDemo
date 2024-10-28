using Application.DTOs.Customer;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Customers.GetCustomerById;

public class GetCustomerByIdQuery : IRequest<CustomerDto>
{
    public Guid Id { get; set; }

    public GetCustomerByIdQuery(Guid id)
    {
        Id = id;
    }
}