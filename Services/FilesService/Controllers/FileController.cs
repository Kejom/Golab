using FilesService.Managers;
using Microsoft.AspNetCore.Mvc;

namespace FilesService.Controllers
{
    [ApiController]
    [Route("/api/files")]
    public class FileController: ControllerBase
    {
        private readonly IFileManager _fileManager;

        public FileController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpGet("avatar/{id}")]
        public async Task<IActionResult> GetAvatar(string id)
        {
            try
            {
                var result = await _fileManager.GetAvatar(id);
                return File(result.ImageByteArray, result.ContentType, result.FileName);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("avatar")]
        public async Task<IActionResult> UploadAvatar([FromBody]IFormFile file)
        {
            try
            {
                await _fileManager.AddAvatar(file);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
