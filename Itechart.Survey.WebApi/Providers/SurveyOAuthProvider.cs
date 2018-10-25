using System.Security.Claims;
using System.Threading.Tasks;
using Itechart.Common;
using Itechart.Survey.DomainModel;
using Itechart.Survey.Foundation.Interfaces;
using Itechart.Survey.WebApi.Constants;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Itechart.Survey.WebApi.Providers
{
    [UsedImplicitly]
    public class SurveyOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            return Task.CompletedTask;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var authenticationService = context.OwinContext.Get<IAuthenticationService>();

            var user = new User
            {
                UserName = context.UserName,
                Password = context.Password
            };

            var signInResult = await authenticationService.SignInAsync(user);

            if (!signInResult.IsSuccessful)
            {
                foreach (var enumAuthenticationError in signInResult.AuthenticationErrors)
                {
                    context.SetError(SignInValidationConstants.ValidationsErrors[enumAuthenticationError]);
                }

                return;
            }

            user = signInResult.User;

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.DisplayName));
            foreach (var role in user.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
            }

            var ticket = new AuthenticationTicket(identity, null);

            context.Validated(ticket);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);

            context.Validated(newTicket);

            return Task.CompletedTask;
        }
    }
}
