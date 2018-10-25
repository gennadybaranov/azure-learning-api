using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Itechart.Repositories;
using Itechart.Survey.DomainModel;

namespace Itechart.Survey.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IReadOnlyCollection<User>> GetUsersAsync(
            int skippedUsersAmount,
            int pageSize,
            SortingOrder sortingOrder,
            Expression<Func<User, object>> orderingKeySelector,
            Expression<Func<User, bool>> filterPredicate);

        Task<int> CountUsersAsync(Expression<Func<User, bool>> filterPredicate);
    }
}
