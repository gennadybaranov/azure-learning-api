using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Itechart.Common;
using Itechart.Repositories;
using Itechart.Survey.DomainModel;
using Itechart.Survey.Repositories.Interfaces;

namespace Itechart.Survey.Repositories.Repositories
{
    [UsedImplicitly]
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext)
            : base(dbContext)
        {

        }


        public async Task<IReadOnlyCollection<User>> GetUsersAsync(
            int skippedUsersAmount,
            int pageSize,
            SortingOrder sortingOrder,
            Expression<Func<User, object>> orderingKeySelector,
            Expression<Func<User, bool>> filterPredicate)
        {
            var users = await GetQuery(sortingOrder, orderingKeySelector).Where(filterPredicate).Skip(skippedUsersAmount).Take(pageSize).ToListAsync();

            return users;
        }

        public async Task<int> CountUsersAsync(Expression<Func<User, bool>> filterPredicate)
        {
            var count = await GetQuery().CountAsync(filterPredicate);

            return count;
        }


        protected override IQueryable<User> GetQuery()
        {
            return base.GetQuery().Include(u => u.Roles);
        }


        private IQueryable<User> GetQuery(SortingOrder order, Expression<Func<User, object>> sortPredicate)
        {
            return order == SortingOrder.Ascending
                ? GetQuery().OrderBy(sortPredicate)
                : GetQuery().OrderByDescending(sortPredicate);
        }
    }
}
