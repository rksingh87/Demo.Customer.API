using Microsoft.Extensions.Caching.Distributed;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Customer.API.Infrastructure.Caching
{
    public interface ICacheProvider
    {
        bool TryGetVaue<T>(string key, out T value);
        Task<T> GetAsync<T>(string key, CancellationToken token = default);
        void Set<T>(string key, T value, DistributedCacheEntryOptions options = default);
        Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options = default, CancellationToken token = default);
        void Remove(string key);
        Task RemoveAsync(string key, CancellationToken token = default);
        void Refresh(string key);
        Task RefreshAsync(string key, CancellationToken token = default);
    }
}
