using MassTransit;
using Models.Contracts;
using Models.Db;
using MongoDB.Entities;
using System.Security.Claims;

namespace FilesService.Managers
{
    public class FileManager : IFileManager
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IHttpContextAccessor _contextAccessor;

        public FileManager(IPublishEndpoint publishEndpoint, IHttpContextAccessor contextAccessor)
        {
            _publishEndpoint = publishEndpoint;
            _contextAccessor = contextAccessor;
        }

        public async Task<AvatarImage> GetAvatar(string id)
        {
            return await DB.Find<AvatarImage>().OneAsync(id);
        }

        public async Task AddAvatar(IFormFile file)
        {
            if (file == null || file.Length == 0) throw new Exception("invalid file");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            var userId = GetCurrentUserId();
            var avatarImage = new AvatarImage { ImageByteArray = fileBytes, UserId = userId, ContentType = file.ContentType, FileName = file.FileName };
            await avatarImage.SaveAsync();

            var avatarUploaded = new AvatarUploaded { UserId = userId, AvatarId = avatarImage.ID };
            await _publishEndpoint.Publish(avatarUploaded);
        }

        private string GetCurrentUserId()
        {
            return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
