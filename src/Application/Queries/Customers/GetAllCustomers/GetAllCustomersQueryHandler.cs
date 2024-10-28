using Application.DTOs.Customer;
using Application.Interfaces;
using Application.Interfaces.Caching;
using AutoMapper;
using MediatR;

namespace Application.Queries.Customers.GetAllCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper, ICacheService cacheService)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        string cacheKey = $"GetAllCustomers";

        var cachedCustomer = await _cacheService.GetAsync<IEnumerable<CustomerDto>>(cacheKey);
        if (cachedCustomer != null)
        {
            return cachedCustomer;
        }
        
        var customers = await _customerRepository.GetAllAsync();

        await _cacheService.SetAsync(cacheKey, customers, TimeSpan.FromMinutes(5));
        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }
}