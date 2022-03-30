using System.Threading.Tasks;
using AutoMapper;
using Core.Services.Contracts.Example;
using DTO.Entities.Models.Example;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.Example;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : BaseController
    {
        private readonly IExampleService _exampleService;

        public ExampleController(
            IMapper mapper,
            IExampleService exampleService) : base(mapper)
        {
            _exampleService = exampleService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Index()
        {
            var result = _exampleService.GetList();

            return OK<ExampleModel>(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync(ExampleModel model)
        {
            var mappedModel = Mapper.Map<ExampleDto>(model);
            await _exampleService.AddAsync(mappedModel);

            return OK();
        }
    }
}
