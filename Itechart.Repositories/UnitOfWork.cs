using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Itechart.Common;

namespace Itechart.Repositories
{
    [UsedImplicitly]
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        private readonly IDictionary<Type, object> _repositories;
        private readonly IDictionary<Type, Type> _customRepositoryTypes;


        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;

            _repositories = new Dictionary<Type, object>();
            _customRepositoryTypes = new Dictionary<Type, Type>();
        }


        public IRepository<T> GetRepository<T>()
            where T : class, IEntity
        {
            if (!_repositories.TryGetValue(typeof(T), out var repository))
            {
                repository = _customRepositoryTypes.TryGetValue(typeof(T), out var repositoryType)
                    ? Activator.CreateInstance(repositoryType, _dbContext)
                    : new Repository<T>(_dbContext);

                _repositories.Add(typeof(T), repository);
            }

            return (IRepository<T>)repository;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }


        protected void RegisterCustomRepository<TEntity, TRepository>()
            where TEntity : IEntity
            where TRepository: IRepository<TEntity>
        {
            _customRepositoryTypes.Add(typeof(TEntity), typeof(TRepository));
        }
    }
}
