using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Itechart.Common.Logging;
using Itechart.Survey.WebApi.Constants;
using Itechart.Survey.WebApi.DataContracts;

namespace Itechart.Survey.WebApi.Filters
{
    public class ControllerExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(HttpActionExecutedContext context, CancellationToken cancellationToken)
        {
            var method = context.Request.Method.Method;
            var uri = context.Request.RequestUri.AbsoluteUri;

            var message = new StringBuilder();
            message.AppendFormat(" {0} | {1} ", method, uri);

            LoggerContext.Current.LogError(message.ToString(), context.Exception);

            context.Response = context.Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                new ErrorDataContract(new []
                {
                    SurveyWebApiConstants.InternalServerError
                }));

            return Task.FromResult<object>(null);
        }
    }
}
