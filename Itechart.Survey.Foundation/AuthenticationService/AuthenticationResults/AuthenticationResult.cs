using System.Collections.Generic;
using Itechart.Survey.DomainModel;

namespace Itechart.Survey.Foundation.AuthenticationService.AuthenticationResults
{
    public class AuthenticationResult<TError>
    {
        public bool IsSuccessful { get; }

        public IReadOnlyCollection<TError> AuthenticationErrors { get; }

        public User User { get; }


        private AuthenticationResult(bool isSuccessful, IReadOnlyCollection<TError> authenticationErrors, User user = null)
        {
            IsSuccessful = isSuccessful;
            AuthenticationErrors = authenticationErrors;
            User = user;
        }


        public static AuthenticationResult<TError> CreateSuccessful(User user)
        {
            return new AuthenticationResult<TError>(true, null, user);
        }

        public static AuthenticationResult<TError> CreateUnsuccessful(IReadOnlyCollection<TError> authenticationErrors)
        {
            return new AuthenticationResult<TError>(false, authenticationErrors);
        }
    }
}
