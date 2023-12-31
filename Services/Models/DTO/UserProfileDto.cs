using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Handle { get; set; }
        public string Description { get; set; }
        public string AvatarId { get; set; }
    }
}
