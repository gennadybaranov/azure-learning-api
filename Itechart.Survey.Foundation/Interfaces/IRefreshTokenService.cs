using System;
using System.Threading.Tasks;
using Itechart.Survey.DomainModel;

namespace Itechart.Survey.Foundation.Interfaces
{
    public interface IRefreshTokenService : IDisposable
    {
        Task<RefreshToken> TryDeleteAsync(string token);

        Task<RefreshToken> CreateAsync(string userName, DateTime issuedUtc, DateTime expiresUtc, string protectedTicket);
    }
}
