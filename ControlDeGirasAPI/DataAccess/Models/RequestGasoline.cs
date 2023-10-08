using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class RequestGasoline : BaseEntity
    {
        public string City { get; set; } = string.Empty;
        public string Commerce { get; set; } = string.Empty;

        public int Mileague { get; set; }

        public int Litres { get; set; }

        public DateOnly Date { get; set; }

        public string Card { get; set; }

        public string Invoice { get; set; }

        public string Authorization { get; set; }

        public int RequestId { get; set; }

        public Request Request { get; set; }
    }
}
