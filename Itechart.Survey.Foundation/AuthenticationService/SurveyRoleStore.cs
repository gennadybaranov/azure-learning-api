using System.Threading.Tasks;
using Itechart.Common;
using Itechart.Repositories;
using Itechart.Survey.DomainModel;
using Microsoft.AspNet.Identity;

namespace Itechart.Survey.Foundation.AuthenticationService
{
    [UsedImplicitly]
    public class SurveyRoleStore : IRoleStore<Role, int>
    {
        private readonly IUnitOfWork _unitOfWork;


        public SurveyRoleStore(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public Task CreateAsync(Role role)
        {
            var roleRepository = _unitOfWork.GetRepository<Role>();
            roleRepository.Add(role);

            return _unitOfWork.SaveChangesAsync();
        }

        public Task UpdateAsync(Role role)
        {
            var roleRepository = _unitOfWork.GetRepository<Role>();
            roleRepository.Update(role);

            return _unitOfWork.SaveChangesAsync();
        }

        public Task DeleteAsync(Role role)
        {
            var roleRepository = _unitOfWork.GetRepository<Role>();
            roleRepository.Delete(role);

            return _unitOfWork.SaveChangesAsync();
        }

        public Task<Role> FindByIdAsync(int roleId)
        {
            var roleRepository = _unitOfWork.GetRepository<Role>();

            return roleRepository.GetByIdAsync(roleId);
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            var roleRepository = _unitOfWork.GetRepository<Role>();

            return roleRepository.GetSingleOrDefaultAsync(r => r.Name == roleName);
        }

        public void Dispose()
        {

        }
    }
}
