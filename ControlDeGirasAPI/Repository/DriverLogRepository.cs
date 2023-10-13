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
    public class DriverLogRepository : GenericRepository<DriverLog>, IDriverLogRepository
    {
        private readonly MyDbContext _context;

        public DriverLogRepository(MyDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
