using Microsoft.Extensions.Caching.Memory;

namespace Links.Services;

public class CacheService
{
    private readonly IMemoryCache _memoryCache;

    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<T> GetOrCreateAsync<T>(string key,
                                             Func<Task<T>> getFn,
                                             TimeSpan expireTime)
    {
        var valueExists = _memoryCache.TryGetValue(key, out T value);

        if (!valueExists)
        {
            value = await getFn();

            if (value is not null)
            {
                var options = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(expireTime);

                _memoryCache.Set(key, value, options);
            }
        }

        return await Task.Run(() => value!);
    }
}
