using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Process : BaseEntity
    {
        public int StateId { get; set; }

        public State State { get; set; }

        public int RequestId { get; set; }

        public Request Request { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
