using Models.Db;

namespace FilesService.Managers
{
    public interface IFileManager
    {
        Task AddAvatar(IFormFile file);
        Task<AvatarImage> GetAvatar(string id);
    }
}