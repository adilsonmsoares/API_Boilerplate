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
                        Title = "API",
                        Version = "v1",
                        Description = "API",
                        Contact = new OpenApiContact()
                        {
                            Name = "PROJECT ADMIN NAME",
                            Email = "PROJECT ADMIN EMAIL"
                        }
                    });
            });
        }
    }
}
