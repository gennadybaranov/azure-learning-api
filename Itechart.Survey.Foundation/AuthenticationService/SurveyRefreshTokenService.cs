using System;
using System.Threading.Tasks;
using Itechart.Common;
using Itechart.Repositories;
using Itechart.Survey.DomainModel;
using Itechart.Survey.Foundation.Interfaces;

namespace Itechart.Survey.Foundation.AuthenticationService
{
    [UsedImplicitly]
    public class SurveyRefreshTokenService : IRefreshTokenService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<RefreshToken> _refreshTokenRepository;


        public SurveyRefreshTokenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _refreshTokenRepository = _unitOfWork.GetRepository<RefreshToken>();
        }


        public async Task<RefreshToken> TryDeleteAsync(string token)
        {
            var dbRefreshToken = await _refreshTokenRepository.GetSingleOrDefaultAsync(t => t.Token == token);

            if (dbRefreshToken != null)
            {
                _refreshTokenRepository.Delete(dbRefreshToken);

                await _unitOfWork.SaveChangesAsync();

                return dbRefreshToken;
            }

            return null;
        }

        public async Task<RefreshToken> CreateAsync(string userName, DateTime issuedUtc, DateTime expiresUtc, string protectedTicket)
        {
            var refreshTokenId = Guid.NewGuid().ToString();

            var userRepository = _unitOfWork.GetRepository<User>();

            var user = await userRepository.GetSingleOrDefaultAsync(u => u.UserName == userName);

            var token = new RefreshToken
            {
                Token = refreshTokenId,
                UserId = user.Id,
                User = user,
                IssuedUtc = issuedUtc,
                ExpiresUtc = expiresUtc,
                ProtectedTicket = protectedTicket
            };

            var dbRefreshToken = await _refreshTokenRepository.GetSingleOrDefaultAsync(t => t.User.UserName == userName);

            if (dbRefreshToken != null)
            {
                _refreshTokenRepository.Delete(dbRefreshToken);
            }

            _refreshTokenRepository.Add(token);

            await _unitOfWork.SaveChangesAsync();

            return token;
        }

        public void Dispose()
        {

        }
    }
}
