using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class CooDto
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public uint Comments { get; set; }
        public uint Likes { get; set; }
        public string UserId { get; set; }
    }
}
