using AutoMapper;
using DTO.Entities.Models.Example;
using WebAPI.Models.Example;

namespace WebAPI.AutoMapper
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

            CreateMap<ExampleDto, ExampleModel>().ReverseMap();

            #endregion
        }
    }
}
