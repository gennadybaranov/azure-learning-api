using Itechart.Repositories;

namespace Itechart.Survey.Repositories.Interfaces
{
    public interface ISurveyUnitOfWork : IUnitOfWork
    {
        IUserRepository GetUserRepository();
    }
}
