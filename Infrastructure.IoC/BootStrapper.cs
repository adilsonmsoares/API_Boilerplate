using Core.Services.Contracts;
using Core.Services.Contracts.Example;
using Core.Services.Implementations;
using Core.Services.Implementations.Example;
using Data.Repositories.Contracts;
using Data.Repositories.Contracts.Example;
using Data.Repositories.Implementations;
using Data.Repositories.Implementations.Example;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.IoC
{
    public static class BootStrapper
    {
        public static void Register(this IServiceCollection services)
        {
            services.AddSingleInstances();
            services.AddRepositories();
            services.AddServices();
        }

        private static void AddSingleInstances(this IServiceCollection services)
        {
            // Method intentionally left empty.
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(DapperRepository<>));
            services.AddScoped(typeof(IExampleRepository), typeof(ExampleRepository));
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseService), typeof(BaseService));
            services.AddScoped(typeof(IExampleService), typeof(ExampleService));
        }
    }
}
