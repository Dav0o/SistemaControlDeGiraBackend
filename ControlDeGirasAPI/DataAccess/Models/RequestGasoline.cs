using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class RequestGasoline : BaseEntity
    {
        public string? City { get; set; }
        public string? Commerce { get; set; } 

        public int Mileague { get; set; }

        public int Litres { get; set; }

        public DateTime Date { get; set; }

        public string? Card { get; set; }

        public string? Invoice { get; set; } //Poner foto de facture?

        public string? Authorization { get; set; }

        public int RequestId { get; set; }

        [JsonIgnore]
        public Request Request { get; set; }
    }
}
