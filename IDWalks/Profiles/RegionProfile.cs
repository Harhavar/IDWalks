using AutoMapper;

namespace IDWalks.Profiles
{
    public class RegionProfile : Profile
    {
        public RegionProfile()
        {
            CreateMap<Models.Domines.Region, Models.DTO.Region>().ReverseMap();
        }
    }
}
