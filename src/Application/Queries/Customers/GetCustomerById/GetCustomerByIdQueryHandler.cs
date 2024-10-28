using Application.DTOs.Customer;
using Application.Interfaces;
using Application.Interfaces.Caching;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Customers.GetCustomerById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMediator mediator, IMapper mapper, ICacheService cacheService)
    {
        _customerRepository = customerRepository;
        _mediator = mediator;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);

        if (customer == null)
        {
            return null;
        }
        
        string cacheKey = $"GetCustomerById:{customer.Id}";

        var cachedCustomer = await _cacheService.GetAsync<CustomerDto>(cacheKey);
        if (cachedCustomer != null)
        {
            return cachedCustomer;
        }
        
        var customerDto = _mapper.Map<CustomerDto>(customer);
        await _cacheService.SetAsync(cacheKey, customerDto, TimeSpan.FromMinutes(5));
        
        return customerDto;
    }
}