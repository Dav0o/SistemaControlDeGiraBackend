using DataAccess.Models.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class User : BaseEntity
    {
        //User info
        public int DNI { get; set; }
        public string Name { get; set; } = string.Empty;

        public string LastName1 { get; set; } = string.Empty;

        public string LastName2 { get; set; } = string.Empty;

        public int PhoneNumber { get; set; }

        public int LicenseUNA { get; set; }

        public bool State { get; set; } = true;

        //Login Info
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

        //Relations

        public List<User_Role> user_Roles { get; set; }

        public List<Process> Processes { get; set; }
    }
}
