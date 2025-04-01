using AutoMapper;

namespace Microservice.Claims
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PolicyDTO, Policy>();
            CreateMap<Claims, ClaimsPolicyResponseDTO>();
        }


    }
}
