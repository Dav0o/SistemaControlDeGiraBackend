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
        public DateTime Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set;}

        public int RequestId { get; set; }

        [JsonIgnore]
        public Request Request { get; set; }
    }
}
