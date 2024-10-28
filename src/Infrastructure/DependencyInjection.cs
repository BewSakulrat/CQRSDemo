
using Application.Interfaces;
using Application.Interfaces.Caching;
using Infrastructure.MappingProfiles;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Infrastructure.Caching;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // add scoped for repositories DI
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        
        // add scope for services DI
        services.AddScoped<ICacheService, CacheService>();
        
        // add auto mapper profiles
        services.AddAutoMapper(typeof(CustomerProfile).Assembly);
        
        // config AppDbContext with SQL Server
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        
        // config redis cache
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnection");
            // options.InstanceName = configuration["RedisInstanceName"];
        });
        
        return services;
    }
}
