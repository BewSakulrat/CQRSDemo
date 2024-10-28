using Application.Commands.Customers.CreateCustomer;
using Application.Commands.Customers.UpdateCustomer;
using Application.DTOs.Customer;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.MappingProfiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<CreateCustomerDto, Customer>().ReverseMap();
        CreateMap<UpdateCustomerDto, Customer>().ReverseMap();
        CreateMap<CreateCustomerCommand, Customer>().ReverseMap();
        CreateMap<UpdateCustomerCommand, Customer>().ReverseMap();
    }
}