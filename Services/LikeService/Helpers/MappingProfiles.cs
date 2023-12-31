using AutoMapper;
using Models.Db;
using Models.DTO;

namespace LikeService.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Like, LikeDto>();
            CreateMap<LikeDto, Like>();
        }
    }
}
