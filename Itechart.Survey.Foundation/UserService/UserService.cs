using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Itechart.Common;
using Itechart.Survey.DomainModel;
using Itechart.Survey.Foundation.Interfaces;
using Itechart.Survey.Foundation.UserService.UserActionResult;
using Itechart.Survey.Repositories.Interfaces;
using Microsoft.AspNet.Identity;

namespace Itechart.Survey.Foundation.UserService
{
    [UsedImplicitly]
    public class UserService : IUserService
    {
        private readonly UserManager<User, int> _userManager;
        private readonly RoleManager<Role, int> _roleManager;

        private readonly IUserRepository _repository;


        public UserService(UserManager<User, int> userManager, RoleManager<Role, int> roleManager, ISurveyUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;

            _repository = unitOfWork.GetUserRepository();
        }


        public async Task CreateAsync(User user)
        {
            user.Roles = new List<Role>();
            user.SignUpDate = DateTime.UtcNow;

            await _userManager.CreateAsync(user, user.Password);

            await _userManager.AddToRoleAsync(user.Id, Role.BuiltIn.User);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<User> FindAsync(string userName, string password)
        {
            return await _userManager.FindAsync(userName, password);
        }

        public async Task<IReadOnlyCollection<User>> GetUsersAsync(
            int pageNumber,
            int pageSize,
            string filter,
            SortingOrder sortingOrder,
            SortableField sortableField = SortableField.DisplayName)
        {
            var filterPredicate = GetFilterPredicate(filter);
            var orderingKeySelector = GetOrderingKeySelector(sortableField);

            var users = await _repository.GetUsersAsync(
                pageNumber * pageSize,
                pageSize,
                SortingOrderMapper.Map[sortingOrder],
                orderingKeySelector,
                filterPredicate);

            return users;
        }

        public Task<int> CountUsersAsync(string filter)
        {
            return _repository.CountUsersAsync(GetFilterPredicate(filter));
        }

        public async Task<UserActionResult<UserUpdateError>> UpdateAsync(User user, string userName)
        {
            var updateErrors = new List<UserUpdateError>();

            var dbUser = await _userManager.FindByNameAsync(userName);
            if (dbUser == null)
            {
                updateErrors.Add(UserUpdateError.UserNotFound);
                var errorResult = UserActionResult<UserUpdateError>.CreateUnsuccessful(updateErrors);

                return errorResult;
            }

            if (user.UserName != userName)
            {
                var userWithNewUserName = await _userManager.FindByNameAsync(user.UserName);
                if (userWithNewUserName != null)
                {
                    updateErrors.Add(UserUpdateError.NewUserNameExists);
                    var errorResult = UserActionResult<UserUpdateError>.CreateUnsuccessful(updateErrors);

                    return errorResult;
                }
            }

            var userRoles = new List<Role>();
            foreach (var role in user.Roles)
            {
                var dbRole = await _roleManager.FindByNameAsync(role.Name);

                if (dbRole == null)
                {
                    updateErrors.Add(UserUpdateError.RoleNotFound);
                    var errorResult = UserActionResult<UserUpdateError>.CreateUnsuccessful(updateErrors);

                    return errorResult;
                }

                userRoles.Add(dbRole);
            }

            var allRoles = userRoles.Union(dbUser.Roles).ToList();
            foreach (var role in allRoles)
            {
                if (userRoles.Any(r => r.Name == role.Name) && dbUser.Roles.All(r => r.Name != role.Name))
                {
                    await _userManager.AddToRoleAsync(dbUser.Id, role.Name);
                }
                else if (userRoles.All(r => r.Name != role.Name) && dbUser.Roles.Any(r => r.Name == role.Name))
                {
                    await _userManager.RemoveFromRoleAsync(dbUser.Id, role.Name);
                }
            }

            dbUser.UserName = user.UserName;
            dbUser.DisplayName = user.DisplayName;

            await _userManager.UpdateAsync(dbUser);

            var successfulResult = UserActionResult<UserUpdateError>.CreateSuccessful();

            return successfulResult;
        }

        public async Task<UserActionResult<UserDeleteError>> DeleteAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                var errors = new List<UserDeleteError>
                {
                    UserDeleteError.UserNotFound
                };
                var errorResult = UserActionResult<UserDeleteError>.CreateUnsuccessful(errors);

                return errorResult;
            }

            await _userManager.DeleteAsync(user);

            var successfulResult = UserActionResult<UserDeleteError>.CreateSuccessful();

            return successfulResult;
        }


        private static Expression<Func<User, object>> GetOrderingKeySelector(SortableField sortableField)
        {
            switch (sortableField)
            {
                case SortableField.DisplayName:
                    return u => u.DisplayName;
                case SortableField.UserName:
                    return u => u.UserName;
                case SortableField.SignUpDate:
                    return u => u.SignUpDate;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortableField), sortableField, null);
            }
        }

        private static Expression<Func<User, bool>> GetFilterPredicate(string filter)
        {
            return u => u.DisplayName.Contains(filter);
        }
    }
}
