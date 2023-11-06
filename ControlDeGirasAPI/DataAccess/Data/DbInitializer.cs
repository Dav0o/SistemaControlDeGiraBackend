using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<State>().HasData(
                   new State() { Id=1, Name="Pendiente" },
                   new State() { Id = 2, Name = "Avalado" },
                   new State() { Id = 3, Name = "Aprobado" },
                   new State() { Id = 4, Name = "Cancelado" }
            );
        }
    }
}