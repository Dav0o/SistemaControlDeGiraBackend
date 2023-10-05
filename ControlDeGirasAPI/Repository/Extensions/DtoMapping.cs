using DataAccess.Models;
using DataAccess.Models.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
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
            public int Id { get; set; }

            [Required(ErrorMessage = "El número de placa es obligatorio.")]
            [MinLength(5), MaxLength(10)]
            public string Plate_Number { get; set; }


            [Required(ErrorMessage = "La marca del vehículo es obligatoria.")]
            [MinLength(2), MaxLength(20)]
            public string Make { get; set; }

            [Required(ErrorMessage = "El modelo del vehículo es obligatorio.")]
            [MinLength(2), MaxLength(20)]
            public string Model { get; set; }


            [MinLength(2), MaxLength(20)]
            public string Category { get; set; }


            public string Traction { get; set; }

            [Range(1900, 2100)]
            [DefaultValue(0)]
            public int Year { get; set; }

            [MinLength(2), MaxLength(20)]
            public string Color { get; set; }


            public int Capacity { get; set; }

            [Range(0, int.MaxValue)]
            [DefaultValue(0)]
            public int Engine_capacity { get; set; }

            [Range(0, int.MaxValue)]
            [DefaultValue(0)]
            public int Mileage { get; set; }

            [RegularExpression("^(super|diesel|regular)$", ErrorMessage = "El tipo de combustible debe ser 'super', 'diesel' o 'regular'.")]
            public string Fuel { get; set; }

            [DataType(DataType.Date)]
            [Display(Name = "Fecha de Cambio de Aceite")]
            public DateTime Oil_Change { get; set; }

            [Required(ErrorMessage = "El estado del vehículo es obligatorio.")]
            public bool Status { get; set; }
            public string ImageUrl { get; set; }

        }
        public static Vehicle ToVehicle(this DtoVehicle dtoVehicle)
        {
            Vehicle vehicle = new()

            {
                Id = dtoVehicle.Id,
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
            public int Id { get; set; }

            public int DNI { get; set; }

            [Required(ErrorMessage = "El nombre es obligatorio")]
            [StringLength(50)]
            public string Name { get; set; }

            [Required(ErrorMessage = "El apellido es obligatorio")]
            [StringLength(50)]
            public string LastName1 { get; set; }


            public string LastName2 { get; set; }

            [Required]
            [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "El número de teléfono debe tener 8 dígitos")]
            public int PhoneNumber { get; set; }

            public int LicenseUNA { get; set; }

            public bool State { get; set; }

            [EmailAddress(ErrorMessage = "Dirección de correo electrónico no válida")]
            public string Email { get; set; }

            //  [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
            [DataType(DataType.Password)]
            public string Password { get; set; }




        }
        public class DtoUser
        {
            [EmailAddress(ErrorMessage = "Dirección de correo electrónico no válida")]
            public string Email { get; set; } = string.Empty;

            // [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
        }


        public static User ToUser(this DtoCreateUser dtoUser)
        {
            User user = new()
            {
                Id = dtoUser.Id,
                DNI= dtoUser.DNI,
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


        #region Role

        public struct DtoRole
        {
            public int Id { get; set; }
            public string Name { get; set; } 

            public string Description { get; set; }
        }

        public static Role ToRole(this DtoRole dtoRole)
        {
            Role role = new()
            {
                Id = dtoRole.Id,
                Name = dtoRole.Name,
                Description = dtoRole.Description,
            };
            return role;
        }
        #endregion

        #region Maintenance
        public struct DtoMaintenance
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Severity { get; set; }
            public DateTime Date { get; set; }
            public string Type { get; set; }
            public int Category { get; set; }
            public string Status { get; set; }
            public string Description { get; set; }
            public int VehicleId { get; set; }
            public string ImageUrl { get; set; }
        }

        public static Maintenance ToMaintenance(this DtoMaintenance dtoMaintenance)
        {
            Maintenance maintenance = new()

            {
                Id = dtoMaintenance.Id,
                Name = dtoMaintenance.Name,
                Severity = dtoMaintenance.Severity,
                Date = dtoMaintenance.Date,
                Type = dtoMaintenance.Type,
                Category = dtoMaintenance.Category,
                Status = dtoMaintenance.Status,
                Description = dtoMaintenance.Description,
                VehicleId = dtoMaintenance.VehicleId,
                ImageUrl = dtoMaintenance.ImageUrl


            };

            return maintenance;
        }
        #endregion

        #region User_Rol

        public struct DtoUserRole
        {
            public int UserId { get; set; }

            public int RoleId { get; set; }
        }

        public static User_Role ToUserRole(this DtoUserRole request)
        {
            User_Role user_Role = new()
            {
                UserId = request.UserId,
                RoleId = request.RoleId,
            };
            return user_Role;
        }

        #endregion

        #region Request

        public struct DtoRequestByUser
        {
            public int Id { get; set; }
            public int ConsecutiveNumber { get; set; }

            public string ExecutingUnit { get; set; }

            public string TypeRequest { get; set; }

            public string Condition { get; set; }

            public string Priority { get; set; }

     
            public int PersonsAmount { get; set; }

            public string Objective { get; set; }

            public DateTime DepartureDate { get; set; }

            public DateTime ArriveDate { get; set; }

            public string DepartureLocation { get; set; }

            public string DestinyLocation { get; set; } 

            public string Itinerary { get; set; }

            public string Observations { get; set; }

            public string TypeOfVehicle { get; set; }

            public bool ItsDriver { get; set; }
        }

        public static Request ToRequestByUser(this DtoRequestByUser dtoRequest)
        {
            Request newRequest = new()
            {
                Id = dtoRequest.Id,
                ConsecutiveNumber = dtoRequest.ConsecutiveNumber,
                ExecutingUnit = dtoRequest.ExecutingUnit,
                TypeRequest = dtoRequest.TypeRequest,
                Condition = dtoRequest.Condition,
                Priority = dtoRequest.Priority,
               
                PersonsAmount = dtoRequest.PersonsAmount,
                Objective = dtoRequest.Objective,
                DepartureDate = dtoRequest.DepartureDate,
                ArriveDate = dtoRequest.ArriveDate,
                DepartureLocation = dtoRequest.DepartureLocation,
                DestinyLocation = dtoRequest.DestinyLocation,
                Itinerary = dtoRequest.Itinerary,
                Observations = dtoRequest.Observations,
                TypeOfVehicle = dtoRequest.TypeOfVehicle,
                ItsDriver = dtoRequest.ItsDriver,
            };
            return newRequest;
        }

        public struct DtoRequest
        {
            public int Id { get; set; }
            public int ConsecutiveNumber { get; set; }

            public string ExecutingUnit { get; set; }

            public string TypeRequest { get; set; }

            public string Condition { get; set; }

            public string Priority { get; set; }

            public int PersonsAmount { get; set; }

            public string Objective { get; set; }

            public DateTime DepartureDate { get; set; }

            public DateTime ArriveDate { get; set; }

            public string DepartureLocation { get; set; }

            public string DestinyLocation { get; set; }

            public string Itinerary { get; set; }

            public string Observations { get; set; }

            public int InitialMileague { get; set; }

            public int FinalMileague { get; set; }
            public string TypeOfVehicle { get; set; }
            public int VehicleId { get; set; }

            public bool ItsDriver { get; set; }

        }

        public static Request ToRequest(this  DtoRequest dtoRequest)
        {
            Request newRequest = new()
            {
                Id = dtoRequest.Id,
                ConsecutiveNumber = dtoRequest.ConsecutiveNumber,
                ExecutingUnit = dtoRequest.ExecutingUnit,
                TypeRequest = dtoRequest.TypeRequest,
                Condition = dtoRequest.Condition,
                Priority = dtoRequest.Priority,
              
                PersonsAmount = dtoRequest.PersonsAmount,
                Objective = dtoRequest.Objective,
                DepartureDate = dtoRequest.DepartureDate,
                ArriveDate = dtoRequest.ArriveDate,
                DepartureLocation = dtoRequest.DepartureLocation,
                DestinyLocation = dtoRequest.DestinyLocation,
                Itinerary = dtoRequest.Itinerary,
                Observations = dtoRequest.Observations,
                InitialMileague = dtoRequest.InitialMileague,
                FinalMileague = dtoRequest.FinalMileague,
                VehicleId = dtoRequest.VehicleId,
                TypeOfVehicle = dtoRequest.TypeOfVehicle,
                ItsDriver = dtoRequest.ItsDriver
            };
            return newRequest;
        }

        public struct DtoEndorseRequest
        {
            public int Id {  get; set; }
            
            public int? VehicleId { get; set; }

            public bool ItsEndorse { get; set; }
        }

        public struct DtoApproveRequest
        {
            public int Id { get; set; }

            public bool ItsApprove { get; set; }


        }


        public struct DtoCanceledRequest
        {
            public int Id { get; set; }

            public bool ItsCanceled{ get; set; }
        }

        #endregion

    }
}
