using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Internal.IGeneric
{
    public  interface IGenericRepository<T> where T : class
    {
        //Commands
        public T Add(T request);
        public T Update(T request);
        public void Delete(int id);
        public void Delete(params object[] ids);

        //Queries
        public List<T> GetAll();
        public List<T> GetAllByCondition(Expression<Func<T, bool>>? filter = null, string includeProperties = "");
        public T GetById(int id);
        public T GetByCondition(Expression<Func<T, bool>>? filter = null, string includeProperties = "");
    }
}
