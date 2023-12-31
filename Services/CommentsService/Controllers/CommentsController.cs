using CommentsService.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace CommentsService.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentsController: ControllerBase
    {
        private readonly ICommentsManager _commentsManager;

        public CommentsController(ICommentsManager commentsManager)
        {
            _commentsManager = commentsManager;
        }

        [HttpGet("{cooId}")]
        public async Task<IActionResult> GetByCooId(string cooId) 
        {
            try
            {
                var result = await _commentsManager.GetByCooId(cooId);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody]CommentDto commentDto)
        {
            try
            {
                var result = await _commentsManager.Add(commentDto);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditComment([FromBody] CommentDto commentDto)
        {
            try
            {
                var result = await _commentsManager.Edit(commentDto);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveComment(string id)
        {
            try
            {
                await _commentsManager.Remove(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
