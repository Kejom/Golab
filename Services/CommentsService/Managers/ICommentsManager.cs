using Models.DTO;

namespace CommentsService.Managers
{
    public interface ICommentsManager
    {
        Task<CommentDto> Add(CommentDto commentDto);
        Task<CommentDto> Edit(CommentDto commentDto);
        Task<List<CommentDto>> GetByCooId(string cooId);
        Task Remove(string id);
    }
}