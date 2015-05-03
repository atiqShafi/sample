using System.Reflection;
using Sample.Core.Db.Context;
using Sample.Core.Infrastructure.Cache;
using Sample.Core.Infrastructure.Logging;
using Sample.Web.Mvc.Authentication;
using Sample.Web.Mvc.Common;
using Sample.Web.Mvc.Kendo;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace Sample.Web.Mvc
{
    /// <summary>
    /// Ioc setup
    /// </summary>
    public static class SimpleWebInjectorContainer
    {
        public static Container Build()
        {
            var container = new Container();
            container.RegisterPerWebRequest<IDbContext>(() => new AppDbContext("AppDb"));
            container.RegisterPerWebRequest<ICurrentUser,FormsAuthenticationCurrentUser>();
            container.RegisterSingle<ICacheProvider,AppCacheProvider>();
            container.Register<ILogger,NLogLogger>();

            container.RegisterManyForOpenGeneric(typeof(IGridModelBuilder<,>), typeof(IGridModelBuilder<,>).Assembly);
            container.RegisterManyForOpenGeneric(typeof(IModelBuilder<>), typeof(IModelBuilder<>).Assembly);
            container.RegisterManyForOpenGeneric(typeof(IModelBuilder<,>), typeof(IModelBuilder<,>).Assembly);
            container.RegisterManyForOpenGeneric(typeof(IModelHandler<>), typeof(IModelHandler<>).Assembly);
            container.RegisterManyForOpenGeneric(typeof(IModelHandler<,>), typeof(IModelHandler<,>).Assembly);
            container.RegisterManyForOpenGeneric(typeof(IFileModelBuilder<>), typeof(IFileModelBuilder<>).Assembly);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();
            return container;
        }



    }
}