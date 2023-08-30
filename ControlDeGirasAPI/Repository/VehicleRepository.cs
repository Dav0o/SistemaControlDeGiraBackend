using DataAccess.Data;
using DataAccess.Models;
using Repository.Internal;
using Repository.IRepository;


namespace Repository
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {

        public VehicleRepository(MyDbContext _context)
            : base(_context)
        {
        }
    }
}

