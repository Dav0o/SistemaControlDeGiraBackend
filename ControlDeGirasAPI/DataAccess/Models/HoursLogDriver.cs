using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class HoursLogDriver : BaseEntity
    {
        public DateTime workedDay { get; set; }

        public string CategoryHours { get; set; }

        public string Description { get; set; }
        public DateTime InitialHour { get; set; }

        public DateTime FinishHour { get; set; }

        public int DriverLogId { get; set; }

        public DriverLog driverLog { get; set; }

        public int RequestId { get; set; }

        public Request request { get; set; }
    }
}
