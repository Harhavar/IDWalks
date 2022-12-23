using AutoMapper;

namespace IDWalks.Profiles
{
    public class WalkDeficultyProfile : Profile
    {
        public WalkDeficultyProfile()
        {
            CreateMap<Models.Domines.WalkDeficulty, Models.DTO.WalkDeficulty>().ReverseMap();
        }

    }
}
