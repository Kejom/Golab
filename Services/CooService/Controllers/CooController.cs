using CooService.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.DTO;
using System.Security.Claims;

namespace CooService.Controllers
{
    [ApiController]
    [Route("api/coo")]
    public class CooController : ControllerBase
    {
        private readonly ICooManager _cooManager;

        public CooController(ICooManager cooManager)
        {
            _cooManager = cooManager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = _cooManager.Get();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> Get(int page, int pageSize)
        {
            try
            {
                var result = await _cooManager.Get(page, pageSize);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            try
            {
                var result = await _cooManager.GetByUserId(userId);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _cooManager.GetById(id);
                if (result is null) return NotFound();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCoo([FromBody] CooDto coo)
        {
            try
            {
                var result = await _cooManager.SaveCoo(coo);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditCoo([FromBody] CooDto coo)
        {
            try
            {
                var result = await _cooManager.EditCoo(coo);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _cooManager.RemoveCoo(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

    }
}
