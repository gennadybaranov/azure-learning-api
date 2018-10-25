using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Itechart.Common;
using Itechart.Repositories;
using Itechart.Survey.DomainModel;
using Microsoft.AspNet.Identity;

namespace Itechart.Survey.Foundation.AuthenticationService
{
    [UsedImplicitly]
    public class SurveyUserStore : IUserPasswordStore<User, int>, IUserRoleStore<User, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;


        public SurveyUserStore(IUnitOfWork userUnitOfWork)
        {
            _unitOfWork = userUnitOfWork;

            _userRepository = _unitOfWork.GetRepository<User>();
            _roleRepository = _unitOfWork.GetRepository<Role>();
        }


        public Task CreateAsync(User user)
        {
            _userRepository.Add(user);

            return _unitOfWork.SaveChangesAsync();
        }

        public Task UpdateAsync(User user)
        {
            _userRepository.Update(user);

            return _unitOfWork.SaveChangesAsync();
        }

        public Task DeleteAsync(User user)
        {
            _userRepository.Delete(user);

            return _unitOfWork.SaveChangesAsync();
        }

        public Task<User> FindByIdAsync(int userId)
        {
            return _userRepository.GetByIdAsync(userId);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return _userRepository.GetSingleOrDefaultAsync(x => x.UserName == userName);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.Password = passwordHash;

            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(user.Password.Any());
        }

        public async Task AddToRoleAsync(User user, string roleName)
        {
            var role = await _roleRepository.GetSingleAsync(r => r.Name == roleName);

            user.Roles.Add(role);
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            var roles = new List<string>();

            foreach (var role in user.Roles)
            {
                roles.Add(role.Name);
            }

            return await Task.FromResult(roles);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            var isInRole = user.Roles.Any(r => r.Name == roleName);

            return Task.FromResult(isInRole);
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            var role = user.Roles.Single(r => r.Name == roleName);

            user.Roles.Remove(role);

            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}
