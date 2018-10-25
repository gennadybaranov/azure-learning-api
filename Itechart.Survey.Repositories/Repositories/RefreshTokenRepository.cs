using System.Data.Entity;
using System.Linq;
using Itechart.Common;
using Itechart.Repositories;
using Itechart.Survey.DomainModel;

namespace Itechart.Survey.Repositories.Repositories
{
    [UsedImplicitly]
    public class RefreshTokenRepository : Repository<RefreshToken>
    {
        public RefreshTokenRepository(DbContext dbContext)
            : base(dbContext)
        {

        }


        protected override IQueryable<RefreshToken> GetQuery()
        {
            return base.GetQuery().Include(r => r.User);
        }
    }
}
