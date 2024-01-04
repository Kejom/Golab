using AutoMapper;
using MassTransit;
using Models.Contracts;
using Models.Db;
using Models.DTO;
using MongoDB.Entities;
using System.Security.Claims;

namespace CommentsService.Managers
{
    public class CommentsManager : ICommentsManager
    {
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _endpoint;
        private readonly IHttpContextAccessor _contextAccessor;

        public CommentsManager(IMapper mapper, IPublishEndpoint endpoint, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _endpoint = endpoint;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<CommentDto>> GetByCooId(string cooId)
        {
            var comments = await DB.Find<Comment>()
                .Match(c => c.CooId == cooId)
                .Sort(c => c.Created, Order.Descending)
                .ExecuteAsync();

            return _mapper.Map<List<CommentDto>>(comments);
        }

        public async Task<CommentDto> Add(CommentDto commentDto)
        {
            if (!String.IsNullOrEmpty(commentDto.Id))
                throw new Exception("Id should be left empty when creating new Comment");

            var comment = _mapper.Map<Comment>(commentDto);
            comment.UserId = GetCurrentUserId();
            comment.Created = DateTime.UtcNow;
            await comment.SaveAsync();
            var commentAdded = new CommentAdded { CooId = comment.CooId };
            await _endpoint.Publish(commentAdded);
            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<CommentDto> Edit(CommentDto commentDto)
        {
            var comment = await DB.Find<Comment>().OneAsync(commentDto.Id);

            if (comment is null) throw new Exception("comment with given id doesnt exist");

            if (comment.UserId != GetCurrentUserId())
                throw new Exception("Comment can only be edited by its creator");

            comment.Content = commentDto.Content;
            await comment.SaveAsync();

            return _mapper.Map<CommentDto>(comment);
        }

        public async Task Remove(string id)
        {
            var comment = await DB.Find<Comment>().OneAsync(id);

            if (comment is null) return;
            if (comment.UserId != GetCurrentUserId())
                throw new Exception("Comment can only be removed by its creator");

            var commentRemoved = new CommentRemoved() { CooId = comment.CooId };
            await comment.DeleteAsync();
            await _endpoint.Publish(commentRemoved);
        }

        private string GetCurrentUserId()
        {
            return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
