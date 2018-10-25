using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Itechart.Repositories
{
    public interface IRepository<T>
    {
        Task<IReadOnlyCollection<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
