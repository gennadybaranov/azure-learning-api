using System.Collections.Generic;
using Itechart.Survey.Foundation.UserService.UserActionResult;

namespace Itechart.Survey.WebApi.Constants
{
    internal static class UserDeleteConstants
    {
        public const string UserNotFound = "USER_NOT_FOUND";

        public static readonly IReadOnlyDictionary<UserDeleteError, string> Map;


        static UserDeleteConstants()
        {
            Map = new Dictionary<UserDeleteError, string>
            {
                { UserDeleteError.UserNotFound, UserNotFound }
            };
        }
    }
}
