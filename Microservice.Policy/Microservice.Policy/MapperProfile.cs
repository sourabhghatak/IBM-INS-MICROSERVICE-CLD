using AutoMapper;

namespace Microservice.Policy
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PolicyDTO, Policy>();
        }
    }
}
