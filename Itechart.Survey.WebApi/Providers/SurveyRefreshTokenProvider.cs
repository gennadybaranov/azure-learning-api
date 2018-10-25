using System;
using System.Threading.Tasks;
using Itechart.Common;
using Itechart.Survey.Foundation.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Infrastructure;

namespace Itechart.Survey.WebApi.Providers
{
    [UsedImplicitly]
    public class SurveyRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var refreshTokenService = context.OwinContext.Get<IRefreshTokenService>();

            var issuedUtc = DateTime.UtcNow;
            var expiresUtc = issuedUtc.AddYears(1);

            context.Ticket.Properties.IssuedUtc = issuedUtc;
            context.Ticket.Properties.ExpiresUtc = expiresUtc;

            var userName = context.Ticket.Identity.Name;
            var protectedTicket = context.SerializeTicket();

            var token = await refreshTokenService.CreateAsync(userName, issuedUtc, expiresUtc, protectedTicket);

            context.SetToken(token.Token);
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var refreshTokenService = context.OwinContext.Get<IRefreshTokenService>();

            var token = await refreshTokenService.TryDeleteAsync(context.Token);

            if (token != null)
            {
                context.DeserializeTicket(token.ProtectedTicket);
            }
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}
