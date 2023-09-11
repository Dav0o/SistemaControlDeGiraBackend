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

    }
}

