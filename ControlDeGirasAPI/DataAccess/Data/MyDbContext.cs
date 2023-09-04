using DataAccess.Models;
using DataAccess.Models.Relations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {

        }
        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User_Role> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Vehicle -> Maintenance

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.maintenances)
                .WithOne(e => e.Vehicle)
                .HasForeignKey(e => e.VehicleId)
                .IsRequired();

            //User Role

            modelBuilder.Entity<User_Role>()
                .HasKey(ch => new { ch.UserId, ch.RoleId });

            modelBuilder.Entity<User_Role>()
                .HasOne(ch => ch.User)
                .WithMany(c => c.user_Roles)
                .HasForeignKey(ch => ch.UserId);

            modelBuilder.Entity<User_Role>()
                .HasOne(ch => ch.Role)
                .WithMany(h => h.user_Roles)
                .HasForeignKey(ch => ch.RoleId);
        }
    }
}
