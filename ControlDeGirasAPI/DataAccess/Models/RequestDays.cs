using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class RequestDays : BaseEntity
    {
        public DateOnly Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set;}

        public int RequestId { get; set; }

        [JsonIgnore]
        public Request Request { get; set; }
    }
}
