using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Business.Dal
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        IQueryable<T> Where(Expression<Func<T, bool>> predicate);

        T GetById(object key);

        T Add(T entity);

        IEnumerable<T> Add(IEnumerable<T> entities);


        /// <summary>
        /// update entity without select it
        /// </summary>
        T Attach(T entity);

        T Remove(T entitiy);

        IEnumerable<T> Remove(IEnumerable<T> entities);

        void SaveChanges();
    }


    
}
