using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Maintenance : BaseEntity
    {

        public string Name { get; set; } = default!;
        public string Severity { get; set; } = default!;
        public DateTime Date { get; set; } = default!;
        public string Type { get; set; } = default!;
        public int Category { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string Description { get; set; } = default!;

        public string ImageUrl { get; set; }

        public int VehicleId { get; set; } //FK
        public Vehicle Vehicle { get; set; } = null!; 
    }

 
}