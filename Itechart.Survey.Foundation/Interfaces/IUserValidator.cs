using System.Collections.Generic;
using Itechart.Survey.DomainModel;
using Itechart.Survey.Foundation.AuthenticationService.AuthenticationResults;

namespace Itechart.Survey.Foundation.Interfaces
{
    public interface IUserValidator
    {
        IReadOnlyCollection<SignUpError> ValidateForSignUp(User user);

        IReadOnlyCollection<SignInError> ValidateForSignIn(User user);
    }
}
