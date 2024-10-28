using Application.DTOs.Customer;
using Application.Interfaces;
using Application.Interfaces.Caching;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Customers.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper, ICacheService cacheService)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<Customer>(request);
        customer.UpdatedAt = DateTime.Now;
        
        var customerDto = _mapper.Map<CustomerDto>(await _customerRepository.UpdateAsync(customer));
        
        string cacheKeyForAllCustomer = $"GetAllCustomers";
        string cacheKeyForCustomerById = $"GetCustomerById:{customer.Id}";
        
        var cachedCustomers = await _cacheService.GetAsync<IEnumerable<CustomerDto>>(cacheKeyForAllCustomer);
        if (cachedCustomers != null)
        {
            await _cacheService.RemoveAsync(cacheKeyForAllCustomer);
        }
        
        var cachedCustomer = await _cacheService.GetAsync<CustomerDto>(cacheKeyForCustomerById);
        if (cachedCustomer != null)
        {
            await _cacheService.RemoveAsync(cacheKeyForCustomerById);
        }
        
        return customerDto;
    }
}