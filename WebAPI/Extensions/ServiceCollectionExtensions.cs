using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Web API",
                        Version = "v1",
                        Description = "Web API",
                        Contact = new OpenApiContact()
                        {
                            Name = "Adilson Soares",
                            Email = "adilsonm-soares@hotmail.com"
                        }
                    });
            });
        }
    }
}
