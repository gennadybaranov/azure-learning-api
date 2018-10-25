using System.Threading.Tasks;
using Itechart.Common;

namespace Itechart.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class, IEntity;

        Task SaveChangesAsync();
    }
}
