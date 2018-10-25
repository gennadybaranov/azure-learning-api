using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Itechart.Common;
using Itechart.Common.Logging;
using Itechart.Survey.WebApi.Constants;
using Itechart.Survey.WebApi.DataContracts;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Itechart.Survey.WebApi.Middlewares
{
    [UsedImplicitly]
    public class GlobalExceptionHandlerMiddleware : OwinMiddleware
    {
        public GlobalExceptionHandlerMiddleware(OwinMiddleware next)
            : base(next)
        {

        }


        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch (Exception exception)
            {
                var method = context.Request.Method;
                var uri = context.Request.Uri.AbsoluteUri;
                var message = new StringBuilder();
                message.AppendFormat("{0} | {1}", method, uri);

                LoggerContext.Current.LogError(message.ToString(), exception);

                var response = JsonConvert.SerializeObject(new ErrorDataContract(new[]
                {
                    SurveyWebApiConstants.InternalServerError
                }), new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(response);
            }
        }
    }
}
