using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Db
{
    public class UserProfile : Entity
    {
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public string Handle { get; set; }
        public string AvatarId { get; set; }
    }
}
