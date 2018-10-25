using System;
using System.Threading.Tasks;
using Itechart.Survey.DomainModel;
using Itechart.Survey.Foundation.AuthenticationService.AuthenticationResults;

namespace Itechart.Survey.Foundation.Interfaces
{
    public interface IAuthenticationService : IDisposable
    {
        Task<AuthenticationResult<SignUpError>> SignUpAsync(User user);

        Task<AuthenticationResult<SignInError>> SignInAsync(User user);
    }
}
