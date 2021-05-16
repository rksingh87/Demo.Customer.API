using Demo.Customer.API.Infrastructure.Caching.Extension;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Customer.API.Infrastructure.Caching
{
    public class CacheProvider : ICacheProvider
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<CacheProvider> _logger;

        public CacheProvider(IDistributedCache distributedCache, ILoggerFactory loggerFactory)
        {
            _cache = distributedCache;
            _logger = loggerFactory.CreateLogger<CacheProvider>(); ;
        }

        public void Set<T>(string key, T value, DistributedCacheEntryOptions options = default)
        {
            try
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                if (options == default)
                {
                    options = new DistributedCacheEntryOptions();
                }
                _cache.Set(key, value.ToMemoryStreamByteArray(), options);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error While setting data in cache for key {key}", ex);
            }
        }


        public Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options = default, CancellationToken token = default)
        {
            try
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                if (options == default)
                {
                    options = new DistributedCacheEntryOptions();
                }
                return _cache.SetAsync(key, value.ToMemoryStreamByteArray(), options, token);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error While setting data in cache for key {key}", ex);
                return Task.CompletedTask;
            }
        }


        public bool TryGetVaue<T>(string key, out T value)
        {
            bool isPresent = false;
            value = default;
            try
            {
                byte[] data = _cache.Get(key);
                if (data != null)
                {
                    isPresent = true;
                    value = data.ToMemoryStreamType<T>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error While try getting data from cache for key {key}", ex);
            }
            return isPresent;
        }


        public Task<T> GetAsync<T>(string key, CancellationToken token = default)
        {
            try
            {
                return _cache.GetAsync(key, token).ContinueWith(t => t.Result.ToMemoryStreamType<T>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error While get async data from cache for key {key}", ex);
                return null;
            }
        }

        public void Remove(string key)
        {
            try
            {
                _cache.Remove(key);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error While removing data from cache for key {key}", ex);
            }
        }

        public Task RemoveAsync(string key, CancellationToken token = default)
        {
            try
            {
                return _cache.RemoveAsync(key, token);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error While removing async data from cache for key {key}", ex);
                return Task.CompletedTask;
            }
        }


        public void Refresh(string key)
        {
            try
            {
                _cache.Refresh(key);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error While refreshing data in cache for key {key}", ex);
            }
        }

        public Task RefreshAsync(string key, CancellationToken token = default)
        {
            try
            {
                return _cache.RefreshAsync(key, token);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error While refreshing async data in cache for key {key}", ex);
                return Task.CompletedTask;
            }
        }
    }
}
