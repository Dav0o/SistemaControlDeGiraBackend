using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Relations
{
    public class User_Role : BaseEntity
    {
        [Column(Order = 0)]
        public int UserId { get; set; }

        public User User { get; set; }

        [Column(Order = 1)]
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
