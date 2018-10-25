using System.ComponentModel.DataAnnotations;
using Itechart.Survey.WebApi.Constants;

namespace Itechart.Survey.WebApi.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = SignUpValidationConstants.EmailRequired)]
        [EmailAddress(ErrorMessage = SignUpValidationConstants.EmailInvalid)]
        public string UserName { get; set; }

        [Required(ErrorMessage = SignUpValidationConstants.PasswordRequired)]
        [MinLength(6, ErrorMessage = SignUpValidationConstants.PasswordInvalid)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = SignUpValidationConstants.NameRequired)]
        public string DisplayName { get; set; }
    }
}
