using System.Collections.Generic;
using System.Threading.Tasks;
using Itechart.Survey.DomainModel;
using Itechart.Survey.Foundation.UserService;
using Itechart.Survey.Foundation.UserService.UserActionResult;

namespace Itechart.Survey.Foundation.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(User user);

        Task<User> FindByNameAsync(string userName);

        Task<User> FindAsync(string userName, string password);

        Task<IReadOnlyCollection<User>> GetUsersAsync(
            int pageNumber,
            int pageSize,
            string filter,
            SortingOrder sortingOrder,
            SortableField sortableField = SortableField.DisplayName);

        Task<int> CountUsersAsync(string filter = "");

        Task<UserActionResult<UserUpdateError>> UpdateAsync(User user, string userName);

        Task<UserActionResult<UserDeleteError>> DeleteAsync(string userName);
    }
}
