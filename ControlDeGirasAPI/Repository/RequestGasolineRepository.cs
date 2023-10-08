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
    public class RequestGasolineRepository : GenericRepository<RequestGasoline>, IRequestGasolineRepository
    {
        private readonly MyDbContext _context;

        public RequestGasolineRepository(MyDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
