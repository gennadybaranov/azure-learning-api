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
    public class AccountController : ApiController
    {
        private readonly IAuthenticationService _appUserManager;


        public AccountController(IAuthenticationService appUserManager)
        {
            _appUserManager = appUserManager;
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateModel]
        public async Task<HttpResponseMessage> SignUp(UserModel model)
        {
            var user = new User
            {
                DisplayName = model.DisplayName,
                Password = model.Password,
                UserName = model.UserName
            };

            var authenticationResult = await _appUserManager.SignUpAsync(user);

            if (!authenticationResult.IsSuccessful)
            {
                var signUpErrors = authenticationResult.AuthenticationErrors.Select(e => SignUpValidationConstants.ValidationErrors[e])
                    .ToList();
                var errorResponse = Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorDataContract(signUpErrors));

                return errorResponse;
            }

            var successfulResponse = Request.CreateResponse(HttpStatusCode.Created);

            return successfulResponse;
        }
    }
}
