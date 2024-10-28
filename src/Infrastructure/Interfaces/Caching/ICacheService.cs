namespace Infrastructure.Interfaces;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string cacheKey);
    Task SetAsync<T>(string cacheKey, T value, TimeSpan? absoluteExpiration = null);
    Task RemoveAsync(string cacheKey);
}