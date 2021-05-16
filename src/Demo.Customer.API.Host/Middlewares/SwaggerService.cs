using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.IO;

namespace Demo.Customer.API.Host.Middlewares
{
    public static class SwaggerService
    {

        /// <summary>
        /// Add Swagger Configuration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddSwaggerServiceConfiguration(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Description = "Demo.Customer.API",
                    Title = $"Demo.Customer.API - {environment.EnvironmentName}"
                });

                c.SwaggerDoc("v2", new OpenApiInfo()
                {
                    Version = "v2",
                    Description = "Demo.Customer.API",
                    Title = $"Demo.Customer.API - {environment.EnvironmentName}"
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "Document.xml");
                c.IncludeXmlComments(filePath);
            });
        }


        /// <summary>
        /// Swagger Configuration For Application Builder
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("v1/swagger.json", "V1");
                c.SwaggerEndpoint("v2/swagger.json", "V2");
            });
        }
    }
}
