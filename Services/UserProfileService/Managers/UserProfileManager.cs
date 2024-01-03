using AutoMapper;
using MassTransit;
using Models.Db;
using Models.DTO;
using MongoDB.Entities;
using System.Security.Claims;

namespace UserProfileService.Managers
{
    public class UserProfileManager : IUserProfileManager
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserProfileManager(IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<UserProfileDto> Get(string id)
        {
            var profile = await DB.Find<UserProfile>().OneAsync(id);

            if (profile is null) throw new Exception("profile with given id doesnt exist");

            return _mapper.Map<UserProfileDto>(profile);
        }

        public async Task<UserProfileDto> GetByUserHandle(string handle)
        {
            var profile  = (await DB.Find<UserProfile>().ManyAsync(p => p.Handle == handle)).FirstOrDefault();

            if (profile is null) throw new Exception("profile with given handle doesnt exist");

            return _mapper.Map<UserProfileDto>(profile);
        }

        public async Task<UserProfileDto> Create(UserProfileDto profileDto)
        {
            var profile = _mapper.Map<UserProfile>(profileDto);
            profile.ID = GetCurrentUserId();
            profile.Handle = _contextAccessor.HttpContext.User.Identity.Name;

            await profile.SaveAsync();

            return _mapper.Map<UserProfileDto>(profile);
        }

        public async Task<UserProfileDto> Edit(UserProfileDto profileDto)
        {
            var profile = await DB.Find<UserProfile>().OneAsync(profileDto.Id);

            if (profile is null) throw new Exception("profile with given id doesnt exist");
            if (profile.ID != GetCurrentUserId()) throw new Exception("users can only edit their own profile");

            profile.DisplayName = profileDto.DisplayName;
            profile.Description = profileDto.Description;

            await profile.SaveAsync();

            return _mapper.Map<UserProfileDto>(profile);
        }

        private string GetCurrentUserId()
        {
            return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
