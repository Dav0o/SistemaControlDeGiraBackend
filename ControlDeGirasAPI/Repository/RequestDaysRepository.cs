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
    internal class RequestDaysRepository : GenericRepository<RequestDays>, IRequestDaysRepository
    {
        private readonly MyDbContext _context;

        public RequestDaysRepository(MyDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
