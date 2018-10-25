using System;
using System.Collections.Generic;
using System.Net.Mail;
using Itechart.Common;
using Itechart.Survey.DomainModel;
using Itechart.Survey.Foundation.AuthenticationService.AuthenticationResults;
using Itechart.Survey.Foundation.Interfaces;

namespace Itechart.Survey.Foundation.AuthenticationService
{
    [UsedImplicitly]
    public class UserValidator : IUserValidator
    {
        public IReadOnlyCollection<SignUpError> ValidateForSignUp(User user)
        {
            var validationErrors = new List<SignUpError>();

            if (String.IsNullOrEmpty(user.UserName))
            {
                validationErrors.Add(SignUpError.UserNameRequired);
            }
            else
            {
                try
                {
                    var email = new MailAddress(user.UserName);
                }
                catch
                {
                    validationErrors.Add(SignUpError.UserNameInvalid);
                }
            }

            if (String.IsNullOrEmpty(user.Password))
            {
                validationErrors.Add(SignUpError.PasswordRequired);
            }
            else if (user.Password.Length > 0 && user.Password.Length < 6)
            {
                validationErrors.Add(SignUpError.PasswordInvalid);
            }

            if (String.IsNullOrEmpty(user.DisplayName))
            {
                validationErrors.Add(SignUpError.DisplayNameRequired);
            }

            return validationErrors;
        }

        public IReadOnlyCollection<SignInError> ValidateForSignIn(User user)
        {
            var validationErrors = new List<SignInError>();

            if (String.IsNullOrEmpty(user.UserName) || String.IsNullOrEmpty(user.Password))
            {
                validationErrors.Add(SignInError.InvalidCredentials);
            }

            return validationErrors;
        }
    }
}
