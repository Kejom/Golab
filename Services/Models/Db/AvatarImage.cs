using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Db
{
    public class AvatarImage: Entity
    {
        public string UserId { get; set; }
        public byte[] ImageByteArray { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
