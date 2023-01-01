using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using PhoneTipProject.Models;
using PhoneTipProject.Models.Context;

namespace PhoneTipProject.Models.Repository
{
    public class Repository<T> where T : class
    {
        private MyContext _myContext;
        private DbSet<T> _dbset;

        public Repository(MyContext context)
        {
            _myContext = context;
            _dbset = _myContext.Set<T>();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbset;
            if (filter != null)
            {
                return query.Where(filter).ToList();
            }
            else
            {
                return query.ToList();
            }

        }

        public virtual T Find(object id)
        {
            return _dbset.Find(id);
        }

        public virtual void Add(T Entity)
        {
            _dbset.Add(Entity);
        }

        public virtual void Remove(T Entity)
        {
            _dbset.Remove(Entity);
        }

        public virtual void Update(T Entity)
        {
            _myContext.Entry(Entity).State = EntityState.Modified;
        }
    }
}