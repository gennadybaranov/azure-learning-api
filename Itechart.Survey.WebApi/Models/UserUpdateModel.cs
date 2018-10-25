using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Itechart.Survey.WebApi.Constants;

namespace Itechart.Survey.WebApi.Models
{
    public class UserUpdateModel
    {
        [Required(ErrorMessage = UserValidationConstants.DisplayNameRequired)]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = UserValidationConstants.UserNameRequired)]
        public string UserName { get; set; }

        [Required(ErrorMessage = UserValidationConstants.RolesRequired)]
        public IReadOnlyCollection<string> Roles { get; set; }
    }
}
