using AutoMapper;
using Domain.Entities.Models.Example;
using DTO.Entities.Models.Example;

namespace Core.Services.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            DefaultMapping();
        }

        private void DefaultMapping()
        {
            #region Example

            CreateMap<Example, ExampleDto>().ReverseMap();

            #endregion
        }
    }
}
