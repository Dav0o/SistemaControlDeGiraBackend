using DataAccess.Data;
using DataAccess.Models;
using Repository.Internal;
using Repository.IRepository;

namespace Repository
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {

        private readonly MyDbContext _context;

        public VehicleRepository(MyDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Vehicle> GetSpecificVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            //if (vehicle != null)
            //{
            //    await _context.Entry(vehicle)
            //        .Collection(v => v.maintenances)
            //        .LoadAsync();
            //}

            return vehicle;
        }
    }
}

