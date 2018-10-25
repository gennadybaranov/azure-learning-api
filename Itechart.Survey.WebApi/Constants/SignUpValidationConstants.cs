using System.Collections.Generic;
using Itechart.Survey.Foundation.AuthenticationService.AuthenticationResults;

namespace Itechart.Survey.WebApi.Constants
{
    internal static class SignUpValidationConstants
    {
        public const string EmailNotUnique = "EMAIL_NOT_UNIQUE";
        public const string NameRequired = "NAME_REQUIRED";
        public const string EmailInvalid = "EMAIL_INVALID";
        public const string EmailRequired = "EMAIL_REQUIRED";
        public const string PasswordRequired = "PASSWORD_REQUIRED";
        public const string PasswordInvalid = "PASSWORD_INVALID";

        public static readonly IReadOnlyDictionary<SignUpError, string> ValidationErrors;


        static SignUpValidationConstants()
        {
            ValidationErrors = new Dictionary<SignUpError, string>
            {
                { SignUpError.UserNameRequired, EmailRequired },
                { SignUpError.UserNameInvalid, EmailInvalid },
                { SignUpError.UserNameNotUnique, EmailNotUnique },
                { SignUpError.PasswordRequired, PasswordRequired },
                { SignUpError.PasswordInvalid, PasswordInvalid },
                { SignUpError.DisplayNameRequired, NameRequired }
            };
        }
    }
}
