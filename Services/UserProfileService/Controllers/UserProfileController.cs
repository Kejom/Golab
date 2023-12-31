using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using System.Security.Claims;
using UserProfileService.Managers;

namespace UserProfileService.Controllers
{
    [ApiController]
    [Route("api/profile")]
    public class UserProfileController: ControllerBase
    {
        private readonly IUserProfileManager _userProfileManager;

        public UserProfileController(IUserProfileManager userProfileManager)
        {
            _userProfileManager = userProfileManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUserProfile()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _userProfileManager.Get(userId);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var result = await _userProfileManager.Get(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]UserProfileDto userProfileDto)
        {
            try
            {
                var result = await _userProfileManager.Create(userProfileDto);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody]UserProfileDto userProfileDto)
        {
            try
            {
                var result = await _userProfileManager.Edit(userProfileDto);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
