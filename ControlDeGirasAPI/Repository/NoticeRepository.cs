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
    public class NoticeRepository : GenericRepository<Notice>, INoticeRepository
    {

        private readonly MyDbContext _context;

        public NoticeRepository(MyDbContext context)
            : base(context)
        {
            _context = context;
        }

    }
}
