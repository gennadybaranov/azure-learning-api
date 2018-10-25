using System;
using System.Configuration;
using System.Net.Http.Formatting;
using System.Web.Http;
using Itechart.Common;
using Itechart.Common.Logging;
using Itechart.Survey.DomainModel;
using Itechart.Survey.Foundation.Interfaces;
using Itechart.Survey.WebApi.Constants;
using Itechart.Survey.WebApi.Filters;
using Itechart.Survey.WebApi.Infrastructure;
using Itechart.Survey.WebApi.Middlewares;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject;
using Owin;

[assembly: OwinStartup(typeof(Itechart.Survey.WebApi.Startup))]
namespace Itechart.Survey.WebApi
{
    public class Startup
    {
        private readonly NinjectDependencyResolver _dependencyResolver;


        public Startup()
        {
            _dependencyResolver = new NinjectDependencyResolver(new StandardKernel());
        }


        [UsedImplicitly]
        public void Configuration(IAppBuilder app)
        {
            app.Use<GlobalExceptionHandlerMiddleware>();
            app.UseCors(CorsOptions.AllowAll);
            ConfigureDependencies(app);
            ConfigureOAuthTokenGeneration(app);
            ConfigureOAuthTokenConsumption(app);
            ConfigureWebApi(app);
        }


        private void ConfigureDependencies(IAppBuilder app)
        {
            app.CreatePerOwinContext(_dependencyResolver.GetInstanceOf<RoleManager<Role, int>>);
            app.CreatePerOwinContext(_dependencyResolver.GetInstanceOf<IRefreshTokenService>);
            app.CreatePerOwinContext(_dependencyResolver.GetInstanceOf<IAuthenticationService>);
        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            var oAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(15),
                Provider = _dependencyResolver.GetInstanceOf<OAuthAuthorizationServerProvider>(),
                RefreshTokenProvider = _dependencyResolver.GetInstanceOf<IAuthenticationTokenProvider>(),
                AccessTokenFormat = _dependencyResolver.GetInstanceOf<ISecureDataFormat<AuthenticationTicket>>()
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private static void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {
            const string issuer = SurveyWebApiConstants.Issuer;

            var audienceId = ConfigurationManager.AppSettings["AudienceId"];
            var audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["AudienceSecret"]);

            var jwtBearerAuthenticationOprtions = new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { audienceId },
                IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                {
                    new SymmetricKeyIssuerSecurityKeyProvider(issuer, audienceSecret)
                }
            };

            app.UseJwtBearerAuthentication(jwtBearerAuthenticationOprtions);
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.Filters.Add(new ControllerExceptionFilterAttribute());

            config.DependencyResolver = _dependencyResolver;

            LoggerContext.Current = _dependencyResolver.GetInstanceOf<ILogger>();

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            WebApiConfig.Register(config);

            app.UseWebApi(config);
        }
    }
}
