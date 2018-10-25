using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Itechart.Common;
using Itechart.Survey.WebApi.DataContracts;

namespace Itechart.Survey.WebApi.Filters
{
    [UsedImplicitly]
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var modelErrors = actionContext.ModelState.Values.SelectMany(s => s.Errors).Select(e => e.ErrorMessage).Distinct().ToList();
                var errorDataContract = new ErrorDataContract(modelErrors);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, errorDataContract);
            }
        }
    }
}
