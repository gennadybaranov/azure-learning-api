using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Itechart.Common;

namespace Itechart.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<T> _set;


        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;

            _set = _dbContext.Set<T>();
        }


        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await GetQuery().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await GetQuery().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetQuery().SingleOrDefaultAsync(predicate);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetQuery().SingleAsync(predicate);
        }

        public void Add(T entity)
        {
            _set.Add(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _set.Remove(entity);
        }


        protected virtual IQueryable<T> GetQuery()
        {
            return _set;
        }
    }
}
