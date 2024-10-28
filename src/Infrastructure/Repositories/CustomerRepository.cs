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
    private ICustomerRepository _customerRepositoryImplementation;

    public CustomerRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Customer> GetByIdAsync(Guid customerId)
    {
        return await _context.Customers.FindAsync(customerId);
    }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync()
    {
        var customers = await _context.Customers.AsNoTracking().ToListAsync(); 
        return _mapper.Map<IEnumerable<CustomerDto>>(customers); 
    }

    public async Task<Guid> AddAsync(Customer customer)
    {
        // var customer = _mapper.Map<Customer>(customerDto);
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return customer.Id;
    }

    public async Task<Customer> UpdateAsync(Customer customer)
    {
        // var customer = _mapper.Map<Customer>(customerDto);
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
        return customer;
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