using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Itechart.Survey.DomainModel;
using Itechart.Survey.Foundation.Interfaces;
using Itechart.Survey.WebApi.Constants;
using Itechart.Survey.WebApi.DataContracts;
using Itechart.Survey.WebApi.Filters;
using Itechart.Survey.WebApi.Models;
using Microsoft.Web.Http;

namespace Itechart.Survey.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;


        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [Authorize(Roles = Role.BuiltIn.Admin)]
        public async Task<HttpResponseMessage> GetAsync(int pageNumber = 0, int pageSize = 15, string sortingOrder = SortingOrderMapper.Ascending, string filter = "")
        {
            var validationErrors = ValidateParameters(pageNumber, pageSize, sortingOrder);
            if (validationErrors.Errors.Any())
            {
                var errorResponse = Request.CreateResponse(HttpStatusCode.BadRequest, validationErrors);

                return errorResponse;
            }

            var users = await _userService.GetUsersAsync(pageNumber, pageSize, filter, SortingOrderMapper.Map[sortingOrder]);
            var usersCount = await _userService.CountUsersAsync(filter);

            var usersDataContract = users.Select(CreateFrom).ToList();
            var responseData = new UsersDataContract
            {
                Users = usersDataContract,
                UsersCount = usersCount
            };
            var successfulResponse = Request.CreateResponse(HttpStatusCode.OK, responseData);

            return successfulResponse;
        }

        [Authorize(Roles = Role.BuiltIn.Admin)]
        [ValidateModel]
        public async Task<HttpResponseMessage> PutAsync(UserUpdateModel userModel, string userName)
        {
            var roles = userModel.Roles.Select(role => new Role { Name = role }).ToList();
            var user = new User
            {
                DisplayName = userModel.DisplayName,
                UserName = userModel.UserName,
                Roles = roles
            };

            var updateResult = await _userService.UpdateAsync(user, userName);
            if (!updateResult.IsSuccessful)
            {
                var errors = updateResult.Errors.Select(e => UserUpdateConstants.Map[e]).ToList();
                var errorResponse = CreateErrorResponse(errors);

                return errorResponse;
            }

            var successfulResponse = Request.CreateResponse(HttpStatusCode.OK);

            return successfulResponse;
        }

        [Authorize(Roles = Role.BuiltIn.Admin)]
        public async Task<HttpResponseMessage> DeleteAsync(string userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                var errors = new List<string>
                {
                    UserValidationConstants.UserNameRequired
                };
                var errorDataContract = new ErrorDataContract(errors);
                var errorResponse = Request.CreateResponse(HttpStatusCode.BadRequest, errorDataContract);

                return errorResponse;
            }

            var deleteResult = await _userService.DeleteAsync(userName);
            if (!deleteResult.IsSuccessful)
            {
                var errors = deleteResult.Errors.Select(e => UserDeleteConstants.Map[e]).ToList();
                var errorResponse = CreateErrorResponse(errors);

                return errorResponse;
            }

            var successfulResponse = Request.CreateResponse(HttpStatusCode.OK);

            return successfulResponse;
        }


        private static UserDataContract CreateFrom(User user)
        {
            var roles = user.Roles.Select(role => role.Name).ToList();

            var userDataContract = new UserDataContract
            {
                DisplayName = user.DisplayName,
                UserName = user.UserName,
                SignUpDate = user.SignUpDate,
                Roles = roles
            };

            return userDataContract;
        }

        private static ErrorDataContract ValidateParameters(int pageNumber, int pageSize, string sortingOrder)
        {
            var errors = new List<string>();

            if (pageNumber < 0)
            {
                errors.Add(UserValidationConstants.InvalidPageNumber);
            }

            if (pageSize < 0)
            {
                errors.Add(UserValidationConstants.InvalidPageSize);
            }

            if (!SortingOrderMapper.Map.ContainsKey(sortingOrder))
            {
                errors.Add(UserValidationConstants.InvalidSortingOrder);
            }

            var validationResult = new ErrorDataContract(errors);

            return validationResult;
        }

        private HttpResponseMessage CreateErrorResponse(IReadOnlyCollection<string> errors)
        {
            var errorDataContract = new ErrorDataContract(errors);
            var errorResponse = Request.CreateResponse(HttpStatusCode.BadRequest, errorDataContract);

            return errorResponse;
        }
    }
}
