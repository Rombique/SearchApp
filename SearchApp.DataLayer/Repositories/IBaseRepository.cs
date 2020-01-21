using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SearchApp.DataLayer.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void AddNew(TEntity entity);
        TEntity GetById(int id, bool asNoTracking = false);
        TEntity GetFirst(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includes);
        int GetCount(Expression<Func<TEntity, bool>> predicate = null, bool asNoTracking = false);
        IEnumerable<TEntity> GetAll(bool asNoTracking = false);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool asNoTracking = false,
            params Expression<Func<TEntity, object>>[] includes);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> predicate);
    }
}
