
using Application.Interfaces;
using Infrastructure.MappingProfiles;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddAutoMapper(typeof(CustomerProfile).Assembly);
        return services;
    }
}
