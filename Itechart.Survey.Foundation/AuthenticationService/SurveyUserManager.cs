using Itechart.Common;
using Itechart.Survey.DomainModel;
using Microsoft.AspNet.Identity;

namespace Itechart.Survey.Foundation.AuthenticationService
{
    [UsedImplicitly]
    public class SurveyUserManager: UserManager<User, int>
    {
        public SurveyUserManager(IUserStore<User, int> store)
            : base(store)
        {

        }
    }
}
