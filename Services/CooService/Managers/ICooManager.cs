using Models.DTO;

namespace CooService.Managers
{
    public interface ICooManager
    {
        List<CooDto> Get();
        Task<List<CooDto>> Get(int page, int pageSize);
        Task<CooDto> GetById(string id);
        Task<List<CooDto>> GetByUserId(string userId);
        Task RemoveCoo(string id);
        Task<CooDto> SaveCoo(CooDto cooDto);
        Task<CooDto> EditCoo(CooDto cooDto);
    }
}