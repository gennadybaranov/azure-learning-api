using System.Collections.Generic;
using Itechart.Survey.Foundation.AuthenticationService.AuthenticationResults;

namespace Itechart.Survey.WebApi.Constants
{
    internal static class SignInValidationConstants
    {
        public const string EmailOrPasswordIncorrect = "EMAIL_OR_PASSWORD_INCORRECT";

        public static readonly IReadOnlyDictionary<SignInError, string> ValidationsErrors;


        static SignInValidationConstants()
        {
            ValidationsErrors = new Dictionary<SignInError, string>
            {
                { SignInError.InvalidCredentials, EmailOrPasswordIncorrect }
            };
        }
    }
}
