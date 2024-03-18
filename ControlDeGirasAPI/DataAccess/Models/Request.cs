using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Request : BaseEntity
    {
        public string ConsecutiveNumber { get; set; }

        public string ExecutingUnit { get; set; } = "CH";

        public string TypeRequest { get; set; }

        public string Condition { get; set; } = "Permanente";

        public string Priority { get; set; }

        public int PersonsAmount { get; set; }

        public string Objective { get; set; } = string.Empty;

        public DateTime DepartureDate { get; set; }

        public DateTime ArriveDate { get; set; }

        public string DepartureLocation { get; set; } = string.Empty;

        public string DestinyLocation { get; set; } = string.Empty;

        public string Itinerary { get; set; } = string.Empty;

        public string? Observations { get; set; } = string.Empty;

        public string TypeOfVehicle { get; set; } = string.Empty;

        public bool ItsDriver { get; set; }

        public bool ItsApprove { get; set; } = false;

        public bool ItsEndorse { get; set; } = false;

        public bool ItsCanceled { get; set; } = false;

        //Relations

        public int InitialMileague { get; set; }

        public int FinalMileague { get; set; }

        public int? VehicleId { get; set; }

        public Vehicle? Vehicle { get; set; }

        public List<Process>? Processes { get; set; }

        public List<RequestDays>? RequestDays { get; set; }

        public List<RequestGasoline>? RequestGasoline { get; set; }

        public List<HoursLogDriver> hoursLogDrivers { get; set; }

        public int? DriverId { get; set; }

        public User? Driver { get; set; }
    }
}
