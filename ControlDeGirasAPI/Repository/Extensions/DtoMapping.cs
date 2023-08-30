﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class DtoMapping
    {
        #region Vehicle
        public struct DtoVehicle
        {
            public string Plate_Number { get; set; }
            public string Make { get; set; }
            public string Model { get; set; }
            public string Category { get; set; }
            public string Traction { get; set; }
            public int Year { get; set; }
            public string Color { get; set; }
            public int Capacity { get; set; }
            public int Engine_capacity { get; set; }
            public int Mileage { get; set; }
            public string Fuel { get; set; }
            public DateTime Oil_Change { get; set; }
            public bool Status { get; set; }
            public string ImageUrl { get; set; }
        }

        public static Vehicle ToVehicle(this DtoVehicle dtoVehicle)
        {
            Vehicle vehicle = new()

            {
                Plate_Number = dtoVehicle.Plate_Number,
                Make = dtoVehicle.Make,
                Model = dtoVehicle.Model,
                Category = dtoVehicle.Category,
                Traction = dtoVehicle.Traction,
                Year = dtoVehicle.Year,
                Color = dtoVehicle.Color,
                Capacity = dtoVehicle.Capacity,
                Engine_capacity = dtoVehicle.Engine_capacity,
                Mileage = dtoVehicle.Mileage,
                Fuel = dtoVehicle.Fuel,
                Oil_Change = dtoVehicle.Oil_Change,
                Status = dtoVehicle.Status,
                ImageUrl = dtoVehicle.ImageUrl

            };

            return vehicle;
        }
        #endregion

        #region User

        public struct DtoCreateUser
        {
            public string Name { get; set; } 

            public string LastName1 { get; set; } 

            public string LastName2 { get; set; } 

            public int PhoneNumber { get; set; }

            public int LicenseUNA { get; set; }
            public string Email { get; set; }

            public string Password { get; set; }

            public bool State { get; set; }

            
        }
        public class DtoUser
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public static User ToUser(this DtoCreateUser dtoUser)
        {
            User user = new()
            {
                Name = dtoUser.Name,
                LastName1 = dtoUser.LastName1,
                LastName2 = dtoUser.LastName2,
                PhoneNumber = dtoUser.PhoneNumber,
                LicenseUNA = dtoUser.LicenseUNA,
                Email = dtoUser.Email,
                State = dtoUser.State
                

            };
            return user;
        }

        #endregion
    }
}
