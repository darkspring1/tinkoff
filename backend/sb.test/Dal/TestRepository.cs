using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Business.Dal;

namespace Test.Dal
{
    class TestRepository<T> : IRepository<T>
    {
        readonly IQueryable<T> _set;
        public TestRepository(IEnumerable<T> set)
        {
            _set = set.AsQueryable<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _set;
        }

        public T Add(T entity)
        {
            return entity;
        }

        public IEnumerable<T> Add(IEnumerable<T> entities)
        {
            return entities;
        }

        public T Attach(T entity)
        {
            return entity;
        }

        public T GetById(object key)
        {
            Func<T, bool> f = (e) => {
                var dynamicEntity = e.ToDictionary();
                string id = dynamicEntity["Id"].ToString();
                return id == key.ToString();
            };

            return _set
                .Where(f).FirstOrDefault();
        }


        public void SaveChanges()
        {
            
        }


        public T Remove(T entitiy)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Remove(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _set.Where(predicate);
        }
    }
}
