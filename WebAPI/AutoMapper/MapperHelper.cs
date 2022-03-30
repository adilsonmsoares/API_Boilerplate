using AutoMapper;

namespace WebAPI.AutoMapper
{
    public static class MapperHelper
    {
        public static MapperConfiguration CreateConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
        }
    }
}
