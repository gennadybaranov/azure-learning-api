using System.Data.Entity;
using Itechart.Common;
using Itechart.Repositories;
using Itechart.Survey.DomainModel;
using Itechart.Survey.Repositories.Interfaces;
using Itechart.Survey.Repositories.Repositories;

namespace Itechart.Survey.Repositories
{
    [UsedImplicitly]
    public class SurveyUnitOfWork : UnitOfWork, ISurveyUnitOfWork
    {
        public SurveyUnitOfWork(DbContext dbContext)
            : base(dbContext)
        {
            RegisterCustomRepository<User, UserRepository>();
            RegisterCustomRepository<RefreshToken, RefreshTokenRepository>();
        }


        public IUserRepository GetUserRepository()
        {
            return (IUserRepository) GetRepository<User>();
        }
    }
}
