using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Vehicle : BaseEntity
    {
        public string Plate_Number { get; set; } = default!;
        public string Make { get; set; } = default!;
        public string Model { get; set; } = default!;
        public string Category { get; set; } = default!;
        public string Traction { get; set; } = default!;
        public int Year { get; set; } = default!;
        public string Color { get; set; } = default!;
        public int Capacity { get; set; } = default!;
        public int Engine_capacity { get; set; } = default!;
        public int Mileage { get; set; } = default!;
        public string Fuel { get; set; } = default!;
        public DateTime Oil_Change { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; }

        public List<Maintenance> maintenances { get; set; }

        public List<Request> Requests { get; set; }

    }
}
