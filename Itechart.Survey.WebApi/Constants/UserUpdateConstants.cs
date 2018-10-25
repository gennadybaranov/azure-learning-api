using System.Collections.Generic;
using Itechart.Survey.Foundation.UserService.UserActionResult;

namespace Itechart.Survey.WebApi.Constants
{
    internal static class UserUpdateConstants
    {
        public const string UserNotFound = "USER_NOT_FOUND";
        public const string NewUserNameExists = "NEW_USER_NAME_EXISTS";
        public const string RoleNotFound = "ROLE_NOT_FOUND";

        public static readonly IReadOnlyDictionary<UserUpdateError, string> Map;


        static UserUpdateConstants()
        {
            Map = new Dictionary<UserUpdateError, string>
            {
                { UserUpdateError.UserNotFound, UserNotFound },
                { UserUpdateError.NewUserNameExists, NewUserNameExists },
                { UserUpdateError.RoleNotFound, RoleNotFound }
            };
        }
    }
}
