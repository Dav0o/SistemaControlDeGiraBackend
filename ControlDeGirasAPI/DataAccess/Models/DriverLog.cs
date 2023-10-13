using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class DriverLog : BaseEntity
    {
        public DateTime InitialLogDate { get; set; }

        public float OrdinaryHours { get; set; }

        public float BonusHours { get; set; }

        public float ExtraHours { get; set; }

        public float Salary { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
