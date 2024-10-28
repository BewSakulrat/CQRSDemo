using Application.Interfaces.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Caching;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<T?> GetAsync<T>(string cacheKey)
    {
        var cachedData = await _distributedCache.GetStringAsync(cacheKey);
        return cachedData == null ? default : JsonConvert.DeserializeObject<T>(cachedData);
    }

    public async Task SetAsync<T>(string cacheKey, T value, TimeSpan? absoluteExpiration = null)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpiration ?? TimeSpan.FromMinutes(5)
        };
        var serializedData = JsonConvert.SerializeObject(value);
        await _distributedCache.SetStringAsync(cacheKey, serializedData, options);
    }

    public async Task RemoveAsync(string cacheKey)
    {
        await _distributedCache.RemoveAsync(cacheKey);
    }
}