using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Services.Contracts;
using Core.Services.Contracts.Example;
using Data.Repositories.Contracts.Example;
using Domain.Entities.Resources.Constants;
using DTO.Entities.Models.Example;
using DTO.Entities.Validators.Example;
using ExampleEntity = Domain.Entities.Models.Example.Example;

namespace Core.Services.Implementations.Example
{
    public class ExampleService : IExampleService
    {
        private readonly IMapper _mapper;
        private readonly IBaseService _baseService;
        private readonly IExampleRepository _exampleRepository;

        public ExampleService(IMapper mapper, IBaseService baseService, IExampleRepository exampleRepository)
        {
            _mapper = mapper;
            _baseService = baseService;
            _exampleRepository = exampleRepository;
        }

        public IList<ExampleDto> GetList()
        {
            return _baseService
                .Run(() =>
                {
                    var data = _exampleRepository.GetForExample();

                    return _mapper.Map<IList<ExampleDto>>(data);
                });
        }

        public async Task AddAsync(ExampleDto data)
        {
            await _baseService
                .WithParameters(ActionsConstants.EXAMPLE_ADD, (data, typeof(ExampleDtoValidator)))
                .RunAsync(async () =>
                {
                    var mappedData = _mapper.Map<ExampleEntity>(data);
                    await _exampleRepository.AddAsync(mappedData);
                });
        }
    }
}
