using Models.DTO;

namespace UserProfileService.Managers
{
    public interface IUserProfileManager
    {
        Task<UserProfileDto> Create(UserProfileDto profileDto);
        Task<UserProfileDto> Edit(UserProfileDto profileDto);
        Task<UserProfileDto> Get(string id);
        Task<UserProfileDto> GetByUserHandle(string handle);
    }
}