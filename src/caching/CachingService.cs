using financias.src.interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace financias.src.caching
{
    public class CachingService : ICachingService
    {
        private IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;

        public CachingService(IDistributedCache cache)
        {
            _cache = cache;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600)
            };
        }

        public async Task<string> Get(string key)
        {
            return await _cache.GetStringAsync(key);
        }

        public async Task Save(string key, string value)
        {
            await _cache.SetStringAsync(key, value, _options);
        }
    }
}