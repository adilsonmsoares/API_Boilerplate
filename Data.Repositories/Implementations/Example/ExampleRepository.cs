using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories.Contracts.Example;
using Microsoft.Extensions.Configuration;
using ExampleEntity = Domain.Entities.Models.Example.Example;

namespace Data.Repositories.Implementations.Example
{
    public class ExampleRepository : DapperRepository<ExampleEntity>, IExampleRepository
    {
        public ExampleRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public IEnumerable<ExampleEntity> GetForExample()
        {
            var data = new List<ExampleEntity>
            {
                new ExampleEntity() { Property = "teste1" },
                new ExampleEntity() { Property = "teste2" }
            };

            return data;
        }
    }
}
