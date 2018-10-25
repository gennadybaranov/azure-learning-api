using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http.Dependencies;
using System.Web.Http.Validation;
using Itechart.Common.Logging;
using Itechart.Repositories;
using Itechart.Survey.DomainModel;
using Itechart.Survey.Foundation.AuthenticationService;
using Itechart.Survey.Foundation.Interfaces;
using Itechart.Survey.Foundation.UserService;
using Itechart.Survey.Repositories;
using Itechart.Survey.Repositories.Interfaces;
using Itechart.Survey.WebApi.Constants;
using Itechart.Survey.WebApi.Models;
using Itechart.Survey.WebApi.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.WebApi.Filter;

namespace Itechart.Survey.WebApi.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;


        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }


        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public T GetInstanceOf<T>()
        {
            return _kernel.Get<T>();
        }

        public void Dispose()
        {

        }


        private void AddBindings()
        {
            _kernel.Bind<DefaultModelValidatorProviders>().ToConstant(new DefaultModelValidatorProviders(new List<ModelValidatorProvider>()));
            _kernel.Bind<DefaultFilterProviders>().ToConstant(new DefaultFilterProviders(new[] { new NinjectFilterProvider(_kernel) }.AsEnumerable()));

            _kernel.Bind<ILogger>().To<NlogLogger>().InSingletonScope();

            _kernel.Bind<IUnitOfWork, ISurveyUnitOfWork>().To<SurveyUnitOfWork>().InRequestScope();
            _kernel.Bind<DbContext>().To<SurveyDbContext>().InRequestScope();

            _kernel.Bind<IAuthenticationService>().To<AuthenticationService>().InRequestScope();
            _kernel.Bind<IUserValidator>().To<UserValidator>().InRequestScope();
            _kernel.Bind<UserManager<User, int>>().To<SurveyUserManager>().InRequestScope();
            _kernel.Bind<RoleManager<Role, int>>().To<SurveyRoleManager>().InRequestScope();
            _kernel.Bind<IUserStore<User, int>>().To<SurveyUserStore>().InRequestScope();
            _kernel.Bind<IRoleStore<Role, int>>().To<SurveyRoleStore>().InRequestScope();
            _kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            _kernel.Bind<IRefreshTokenService>().To<SurveyRefreshTokenService>().InRequestScope();
            _kernel.Bind<OAuthAuthorizationServerProvider>().To<SurveyOAuthProvider>().InRequestScope();
            _kernel.Bind<IAuthenticationTokenProvider>().To<SurveyRefreshTokenProvider>().InRequestScope();
            _kernel.Bind<ISecureDataFormat<AuthenticationTicket>>().To<SurveyJwtFormat>().InSingletonScope()
                .WithConstructorArgument(SurveyWebApiConstants.Issuer);
        }
    }
}
