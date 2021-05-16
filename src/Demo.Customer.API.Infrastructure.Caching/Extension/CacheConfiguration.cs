using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Demo.Customer.API.Infrastructure.Caching.Extension
{
    public static class CacheConfiguration
    {
        public static void AddDistributedCacheConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            string cacheMode = configuration.GetValue("Caching:CacheMode", "InMemory");
            switch (cacheMode)
            {
                case "InMemory":
                default:
                    {
                        services.AddDistributedMemoryCache();
                        break;
                    }
            }
            services.AddSingleton<ICacheProvider, CacheProvider>();
        }
    }
}
