using Models.DTO;

namespace LikeService.Managers
{
    public interface ILikeManager
    {
        Task AddLike(string cooId);
        Task<List<LikeDto>> GetForCurrentUser();
        Task RemoveLike(string cooId);
    }
}