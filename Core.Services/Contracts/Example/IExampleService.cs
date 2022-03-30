using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Entities.Models;
using DTO.Entities.Models.Example;

namespace Core.Services.Contracts.Example
{
    public interface IExampleService
    {
        IList<ExampleDto> GetList();

        Task AddAsync(ExampleDto data);
    }
}
