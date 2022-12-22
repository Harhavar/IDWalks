using AutoMapper;

namespace IDWalks.Profiles
{
    public class WalkProfile : Profile
    {
        public WalkProfile() 
        {
            CreateMap<Models.Domines.Walk, Models.DTO.Walk>().ReverseMap();
        }
    }
}
