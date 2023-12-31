using LikeService.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LikeService.Controllers
{
    [ApiController]
    [Route("/api/likes")]
    public class LikeController: ControllerBase
    {
        private readonly ILikeManager _likeManager;
        public LikeController(ILikeManager likeManager)
        {
            _likeManager = likeManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _likeManager.GetForCurrentUser();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [Authorize]
        [HttpPost("{cooId}")]
        public async Task<IActionResult> Add(string cooId)
        {
            try
            {
                await _likeManager.AddLike(cooId);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpDelete("{cooId}")]
        public async Task<IActionResult> Remove(string cooId)
        {
            try
            {
                await _likeManager.RemoveLike(cooId);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
