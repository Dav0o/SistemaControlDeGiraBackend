using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Repository.Internal.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Internal
{
    public class GenericRepository<T> :  IGenericRepository<T> where T : class
    {
        public readonly MyDbContext _myDbContext;
        private readonly DbSet<T> _DbSet;
        public GenericRepository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
            _DbSet = _myDbContext.Set<T>();
        }

        //Commands
        public T Add(T entity)
        {
            _myDbContext.Add(entity);
            _myDbContext.SaveChanges();

            return entity;
        }
        public T Update(T entity)
        {
            _DbSet.Attach(entity);
            _myDbContext.Entry(entity).State = EntityState.Modified;
            _myDbContext.SaveChanges(); //

            return entity;
        }
        public void Delete(int id)
        {
            var entity = _DbSet.Find(id);

            if (entity is not null)
            {
                _DbSet.Remove(entity);
                _myDbContext.SaveChanges();
            }

        }

        public void Delete(params object[] ids)
        {
            var entity = _DbSet.Find(ids);

            if (entity is not null)
            {
                _DbSet.Remove(entity);
                _myDbContext.SaveChanges();
            }

        }


        //Queries

        public List<T> GetAllByCondition(Expression<Func<T, bool>>? filter = null, string includeProperties = "")
        {
            IQueryable<T> query = _DbSet;

            if (filter is not null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.ToList();
        }

        public T GetByCondition(Expression<Func<T, bool>>? filter = null, string includeProperties = "")
        {
            IQueryable<T> query = _DbSet;

            if (filter is not null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.ToList().FirstOrDefault();
        }

        public T GetById(int id)
        {
            return _DbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return  _DbSet.ToList();
        }
    }
}
