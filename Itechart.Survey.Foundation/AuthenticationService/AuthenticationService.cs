using System.Linq;
using System.Threading.Tasks;
using Itechart.Common;
using Itechart.Survey.DomainModel;
using Itechart.Survey.Foundation.AuthenticationService.AuthenticationResults;
using Itechart.Survey.Foundation.Interfaces;

namespace Itechart.Survey.Foundation.AuthenticationService
{
    [UsedImplicitly]
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IUserValidator _userValidator;


        public AuthenticationService(IUserService userService, IUserValidator userValidator)
        {
            _userService = userService;
            _userValidator = userValidator;
        }


        public async Task<AuthenticationResult<SignUpError>> SignUpAsync(User user)
        {
            var validationResult = _userValidator.ValidateForSignUp(user);
            var validationResultList = validationResult.ToList();

            var foundUser = await _userService.FindByNameAsync(user.UserName);

            if (foundUser != null)
            {
                validationResultList.Add(SignUpError.UserNameNotUnique);
            }

            if (validationResultList.Any())
            {
                return AuthenticationResult<SignUpError>.CreateUnsuccessful(validationResultList);
            }

            await _userService.CreateAsync(user);

            return AuthenticationResult<SignUpError>.CreateSuccessful(user);
        }

        public async Task<AuthenticationResult<SignInError>> SignInAsync(User user)
        {
            var validationResult = _userValidator.ValidateForSignIn(user);
            var validationResultList = validationResult.ToList();

            if (validationResultList.Any())
            {
                return AuthenticationResult<SignInError>.CreateUnsuccessful(validationResultList);
            }

            var foundUser = await _userService.FindAsync(user.UserName, user.Password);

            if (foundUser == null)
            {
                validationResultList.Add(SignInError.InvalidCredentials);
            }

            return validationResultList.Any()
                ? AuthenticationResult<SignInError>.CreateUnsuccessful(validationResultList)
                : AuthenticationResult<SignInError>.CreateSuccessful(foundUser);
        }

        public void Dispose()
        {

        }
    }
}
