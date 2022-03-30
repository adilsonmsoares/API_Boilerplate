using System.Collections.Generic;
using ExampleEntity = Domain.Entities.Models.Example.Example;

namespace Data.Repositories.Contracts.Example
{
    public interface IExampleRepository : IRepository<ExampleEntity>
    {
        IEnumerable<ExampleEntity> GetForExample();
    }
}
