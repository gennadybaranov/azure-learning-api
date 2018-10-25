using Itechart.Common;
using Itechart.Survey.DomainModel;
using Microsoft.AspNet.Identity;

namespace Itechart.Survey.Foundation.AuthenticationService
{
    [UsedImplicitly]
    public class SurveyRoleManager : RoleManager<Role, int>
    {
        public SurveyRoleManager(IRoleStore<Role, int> roleStore)
            : base(roleStore)
        {

        }
    }
}
