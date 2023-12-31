using AutoMapper;
using MassTransit;
using Models.Contracts;
using Models.Db;
using Models.DTO;
using MongoDB.Entities;
using System.Security.Claims;

namespace LikeService.Managers
{
    public class LikeManager : ILikeManager
    {
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IHttpContextAccessor _contextAccessor;

        public LikeManager(IMapper mapper, IPublishEndpoint publishEndpoint, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<LikeDto>> GetForCurrentUser()
        {
            var userId = GetCurrentUserId();

            var likes = await DB.Find<Like>().ManyAsync(l => l.UserId == userId);
            return _mapper.Map<List<LikeDto>>(likes);
        }

        public async Task AddLike(string cooId)
        {
            var userId = GetCurrentUserId();
            var like = DB.Queryable<Like>().FirstOrDefault(l => l.CooId == cooId && l.UserId == userId);

            if (like is not null) return;

            like = new Like() { CooId = cooId, UserId = userId };
            await like.SaveAsync();
            var likeAdded = new CooLikeAdded() { CooId = cooId };
            await _publishEndpoint.Publish(likeAdded);
        }

        public async Task RemoveLike(string cooId)
        {
            var userId = GetCurrentUserId();
            var like = DB.Queryable<Like>().FirstOrDefault(l => l.CooId == cooId && l.UserId == userId);

            if (like is null) return;

            await like.DeleteAsync();
            var likeRemoved = new CooLikeRemoved() { CooId = cooId };
            await _publishEndpoint.Publish(likeRemoved);
        }

        private string GetCurrentUserId()
        {
            return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
