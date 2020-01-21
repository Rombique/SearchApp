using SearchApp.DataLayer.EF;
using SearchApp.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchApp.DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainContext mainContext;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UnitOfWork(MainContext context)
        {
            mainContext = context;
        }

        public IBaseRepository<TEntity> Repository<TEntity>()
            where TEntity : class
        {
            var entityType = typeof(TEntity);
            if (repositories.ContainsKey(entityType))
                return (IBaseRepository<TEntity>)repositories[entityType];

            var newRepository = new BaseRepository<TEntity>(mainContext);
            repositories.Add(entityType, newRepository);
            return newRepository;
        }

        public int Commit()
        {
            return mainContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return mainContext.SaveChangesAsync();
        }

    }

    public interface IUnitOfWork
    {
        IBaseRepository<TEntity> Repository<TEntity>()
            where TEntity : class;

        int Commit();
        Task<int> CommitAsync();
    }
}
