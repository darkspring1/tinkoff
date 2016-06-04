using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Business.Dal;

namespace Dal
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        readonly DbSet<T> _set;

        readonly DataContext _context;

        public EfRepository(DataContext context)
        {
            _set = context.Set<T>();
            _context = context;
        }


        public IQueryable<T> GetAll()
        {
            return _set;
        }

        public T Add(T entity)
        {
            return _set.Add(entity);
        }

        public IEnumerable<T> Add(IEnumerable<T> entities)
        {
            return _set.AddRange(entities);
        }

        public T Attach(T entity)
        {
            return _set.Attach(entity);
        }

        public T GetById(object key)
        {
            return _set.Find(key);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public T Remove(T entitiy)
        {
            return _set.Remove(entitiy);
        }

        public IEnumerable<T> Remove(IEnumerable<T> entities)
        {
            return _set.RemoveRange(entities);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _set.Where(predicate);
        }
    }
}
