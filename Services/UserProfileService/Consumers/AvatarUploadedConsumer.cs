using MassTransit;
using Models.Contracts;
using Models.Db;
using MongoDB.Entities;

namespace UserProfileService.Consumers
{
    public class AvatarUploadedConsumer : IConsumer<AvatarUploaded>
    {
        public async Task Consume(ConsumeContext<AvatarUploaded> context)
        {
            var userId = context.Message.UserId;
            var avatarId = context.Message.AvatarId;

            var userProfile = await DB.Find<UserProfile>().OneAsync(userId);

            if(userProfile is null)
                userProfile = new UserProfile() { ID = userId };

            userProfile.AvatarId = avatarId;
            await userProfile.SaveAsync();
        }
    }
}
