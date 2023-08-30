using Repository.Internal.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
namespace Repository.IRepository
{
    public interface IMaintenanceRepository: IGenericRepository<Maintenance>
    {
    }
}
