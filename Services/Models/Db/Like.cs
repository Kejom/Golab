using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Db
{
    public class Like : Entity
    {
        public string CooId { get; set; }
        public string UserId { get; set; }
    }
}
