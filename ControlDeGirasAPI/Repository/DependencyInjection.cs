using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Internal.IGeneric;
using Repository.Internal;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.Relations;

namespace Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGenericRepository<Vehicle>, GenericRepository<Vehicle>>();
            services.AddScoped<IGenericRepository<Maintenance>, GenericRepository<Maintenance>>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IGenericRepository<User_Role>, GenericRepository<User_Role>>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IGenericRepository<DriverLog>, GenericRepository<DriverLog>>();
            services.AddScoped<IGenericRepository<RequestDays>, GenericRepository<RequestDays>>();
            services.AddScoped<IGenericRepository<RequestGasoline>, GenericRepository<RequestGasoline>>();

            return services;
        }
    }
}
