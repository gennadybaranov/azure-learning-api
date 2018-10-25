using System.Collections.Generic;

namespace Itechart.Survey.Foundation.UserService.UserActionResult
{
    public class UserActionResult<TError>
    {
        public bool IsSuccessful { get; }

        public IReadOnlyCollection<TError> Errors { get; }


        private UserActionResult(bool isSuccessful, IReadOnlyCollection<TError> errors = null)
        {
            IsSuccessful = isSuccessful;
            Errors = errors;
        }


        public static UserActionResult<TError> CreateSuccessful()
        {
            return new UserActionResult<TError>(true);
        }

        public static UserActionResult<TError> CreateUnsuccessful(IReadOnlyCollection<TError> errors)
        {
            return new UserActionResult<TError>(false, errors);
        }
    }
}
