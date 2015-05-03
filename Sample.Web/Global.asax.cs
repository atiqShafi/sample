using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Sample.Core.Db.Migrations;
using Elmah;
using SimpleInjector.Integration.Web.Mvc;
using Sample.Web.Mvc;
using Sample.Web.Mvc.Attributes;
using Sample.Web.Mvc.Bundles;
using Sample.Web.Mvc.ModelBinders;

namespace Sample.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var container = SimpleWebInjectorContainer.Build();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            RouteTable.Routes.MapMvcAttributeRoutes();
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.MapRoute("Default", "{controller}/{action}", new { controller = "App", action = "Index", id = UrlParameter.Optional });
            BundleTable.Bundles.IgnoreList.Clear();
            Bundles.Build().ForEach(x => BundleTable.Bundles.Add(x)); 
            GlobalFilters.Filters.Add(new ModelStateValidation()); // enable auto validation on models
            ModelBinders.Binders.Add(typeof(string), new TrimModelBinder()); 

            var migrator = new DbMigrator(new Configuration { TargetDatabase = new DbConnectionInfo("AppDb") });
            migrator.Update(); // run migration on start 
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (!HttpContext.Current.IsDebuggingEnabled)
            {
                var exception = Server.GetLastError();
                ErrorSignal.FromCurrentContext().Raise(exception);
            }
        }
    }

}