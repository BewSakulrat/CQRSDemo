using Application.DTOs.Customer;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CustomerRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerDto> GetCustomerByIdAsync(Guid customerId)
    {
        var customer = _mapper.Map<CustomerDto>(await _context.Customers.FindAsync(customerId));
        return customer;
    }

    public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
    {
        var customers = await _context.Customers.AsNoTracking().ToListAsync(); 
        return _mapper.Map<IEnumerable<CustomerDto>>(customers); 
    }

    public async Task AddAsync(CreateCustomerDto customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);
        await _context.Customers.AddAsync(customer);
    }

    public async Task UpdateAsync(UpdateCustomerDto customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid customerId)
    {
        var customer = await _context.Customers.FindAsync(customerId);
        if (customer == null)
        {
            return;
        }
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }
}