using AutoMapper;
using Models.Db;
using Models.DTO;

namespace CooService.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Coo, CooDto>();
            CreateMap<CooDto, Coo>();

        }
    }
}
