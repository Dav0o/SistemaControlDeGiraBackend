using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Relations
{
    public class User_Role : BaseEntity
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
