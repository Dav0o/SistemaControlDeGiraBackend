using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<MyDbContext>(options =>
            options.UseMySQL(
                configuration.
                GetConnectionString("AspHost")
                ?? throw new InvalidOperationException("Connection string 'MyDbContext' not found.")
                ));


            services.AddScoped<MyDbContext>();

            return services;
        }
    }
}
