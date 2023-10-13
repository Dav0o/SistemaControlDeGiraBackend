using DataAccess.Data;
using DataAccess.Models;
using Repository.Internal;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MaintenanceRepository : GenericRepository<Maintenance>, IMaintenanceRepository
    {

        public MaintenanceRepository(MyDbContext _context)
            : base(_context)
        {



        }
    }
}

