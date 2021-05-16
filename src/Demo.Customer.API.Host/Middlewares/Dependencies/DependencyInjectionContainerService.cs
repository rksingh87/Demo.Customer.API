using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Demo.Customer.API.Host.Middlewares.Dependencies
{
    public static class DependencyInjectionContainerService
    {
        /// <summary>
        /// Resolve And Register Dependencies into ServiceCollection Injection Container
        /// </summary>
        public static void RegisterDependencyInjectionContainer(this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                var resolvedDependencies = ResolveDependencies(configuration);
                RegisterDependencies(services, resolvedDependencies);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Register the Resolved Dependencies
        /// </summary>
        private static void RegisterDependencies(IServiceCollection services, Dictionary<ObjectScope, Dictionary<Type, Type>> resolvedDependencies)
        {
            foreach (var resolvedDependency in resolvedDependencies)
            {
                if (resolvedDependency.Key == ObjectScope.Singleton)
                    RegisterAsSingleton(services, resolvedDependency.Value);
                else if (resolvedDependency.Key == ObjectScope.Transient)
                    RegisterAsTransient(services, resolvedDependency.Value);
                else
                    RegisterAsScoped(services, resolvedDependency.Value);
            }
        }

        /// <summary>
        /// Register the given Dependencies as Singleton
        /// </summary>
        private static void RegisterAsSingleton(IServiceCollection services, Dictionary<Type, Type> keyValuePairs)
        {
            foreach (var dependency in keyValuePairs)
                services.AddSingleton(dependency.Key, dependency.Value);
        }

        /// <summary>
        /// Register the given Dependencies as Transient
        /// </summary>
        private static void RegisterAsTransient(IServiceCollection services, Dictionary<Type, Type> keyValuePairs)
        {
            foreach (var dependency in keyValuePairs)
                services.AddTransient(dependency.Key, dependency.Value);
        }

        /// <summary>
        /// Register the given Dependencies as Scoped
        /// </summary>
        private static void RegisterAsScoped(IServiceCollection services, Dictionary<Type, Type> keyValuePairs)
        {
            foreach (var dependency in keyValuePairs)
                services.AddScoped(dependency.Key, dependency.Value);
        }

        /// <summary>
        /// Fetch the assembly information from the appsetting section and create the dictionary of Resolutions for DI
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static Dictionary<ObjectScope, Dictionary<Type, Type>> ResolveDependencies(IConfiguration configuration)
        {
            Dictionary<ObjectScope, Dictionary<Type, Type>> resolvedDependencies = new Dictionary<ObjectScope, Dictionary<Type, Type>>();
            List<DependencyResolutionsModel> resolutions = new List<DependencyResolutionsModel>();
            configuration.GetSection("DependencyResolutions").Bind(resolutions);

            Dictionary<Type, Type> resolvedResolutions;
            Type contractType, implemetationType;
            foreach (var resolutionGroup in resolutions.GroupBy(e => e.ObjectScope))
            {
                resolvedResolutions = new Dictionary<Type, Type>();
                foreach (var resolution in resolutionGroup)
                {
                    contractType = Type.GetType(resolution.Contract);
                    implemetationType = Type.GetType(resolution.Implementation);
                    resolvedResolutions.Add(contractType, implemetationType);
                }
                resolvedDependencies.Add(resolutionGroup.Key, resolvedResolutions);
            }
            return resolvedDependencies;
        }
    }
}
