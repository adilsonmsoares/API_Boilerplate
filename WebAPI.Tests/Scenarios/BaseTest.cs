using System.Reflection;
using AutoFixture;
using AutoMapper;
using Core.Services.Contracts;
using Core.Services.Implementations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace WebAPI.Tests.Scenarios
{
    public class BaseTest
    {
        protected readonly IMapper Mapper;
        protected readonly IFixture Fixture;
        protected readonly IMemoryCache MemoryCache;
        protected readonly IBaseService BaseService;
        protected readonly IConfiguration Configuration;

        public BaseTest()
        {
            Fixture = new Fixture();
            Mapper = CreateMapper();
            Configuration = LoadConfigurationSettings();
            MemoryCache = new MemoryCache(new MemoryCacheOptions());
            BaseService = new BaseService(new Mock<ILogger<BaseService>>().Object);
        }

        #region Private Methods

        private IMapper CreateMapper()
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(
                    Assembly.Load("Core.Services"),
                    Assembly.Load("Data.Repositories"),
                    Assembly.Load("WebAPI"));
            });

            return configuration.CreateMapper();
        }

        private IConfigurationRoot LoadConfigurationSettings()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        #endregion
    }
}
