using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Db
{
    public class Coo : Entity
    {
        public string Content { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public uint Comments { get; set; }
        public uint Likes { get; set; }
        public string UserId { get; set; }
    }
}
