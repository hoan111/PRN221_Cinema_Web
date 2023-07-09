using AutoMapper;

namespace PRN231_Cinema_Web_API.Config
{
    public static class AutoMapperConfig
    {
        public static Mapper InitializeAutomapper<TSource, TDestination>()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
                // Any other mapping configuration ...
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
