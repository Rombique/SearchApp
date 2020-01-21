using Microsoft.EntityFrameworkCore;
using SearchApp.DataLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SearchApp.DataLayer.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        internal MainContext context;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(MainContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual void AddNew(TEntity entity)
        {
            CheckEntity(entity);
            context.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            CheckEntity(entity);
            context.Remove(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = dbSet.Where(predicate);
            foreach (var entity in entities)
                dbSet.Remove(entity);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool asNoTracking = false,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = dbSet.Where(predicate);
            if (orderBy != null)
                query = orderBy(query);
            query = includes.Aggregate(query, (cur, prop) => cur.Include(prop));

            return asNoTracking ? query.AsNoTracking().ToList() : query.ToList();
        }

        public IEnumerable<TEntity> GetAll(bool asNoTracking = false)
        {
            return dbSet.ToList();
        }

        public TEntity GetById(int id, bool asNoTracking = false)
        {
            return dbSet.Find(id);
        }

        public int GetCount(Expression<Func<TEntity, bool>> predicate = null, bool asNoTracking = false)
        {
            return predicate == null ? dbSet.Count() : dbSet.Count(predicate);
        }

        public TEntity GetFirst(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = predicate != null ? dbSet.Where(predicate) : dbSet.AsQueryable();
            if (orderBy != null)
                query = orderBy(query);
            query = includes.Aggregate(query, (cur, prop) => cur.Include(prop));

            return asNoTracking ? query.AsNoTracking().FirstOrDefault() : query.FirstOrDefault();
        }

        protected void CheckEntity(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity is null");
        }
    }
}
