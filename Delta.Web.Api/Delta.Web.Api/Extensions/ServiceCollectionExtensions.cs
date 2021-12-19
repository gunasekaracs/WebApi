using AutoMapper;

namespace Delta.Web.Api
{
    static class ServiceCollectionExtensions
    {
        public static IMapper CreateAutoMapper(this IServiceCollection _)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles());
            });
            return mapperConfig.CreateMapper();
        }
    }
}
