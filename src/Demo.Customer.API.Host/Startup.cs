using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Demo.Customer.API.Host.Middlewares;
using Demo.Customer.API.Host.Middlewares.Dependencies;
using System.Text.Json;
using Demo.Customer.API.Infrastructure.Caching.Extension;

namespace Demo.Customer.API.Host
{
    public class Startup
    {
        private readonly IWebHostEnvironment environment;

        private IConfiguration Configuration { get; }

        public Startup(IConfiguration _configuration, IWebHostEnvironment _environment)
        {
            Configuration = _configuration;
            environment = _environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DictionaryKeyPolicy = null;
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddSwaggerServiceConfiguration(Configuration, environment);
            services.RegisterDependencyInjectionContainer(Configuration);

            services.AddDistributedCacheConfiguration(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandlerService();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
