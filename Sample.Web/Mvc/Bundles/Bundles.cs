using System.Collections.Generic;
using System.Web.Optimization;

namespace Sample.Web.Mvc.Bundles
{
    /// <summary>
    /// Load bundles
    /// </summary>
    public static class Bundles
    {
        public static List<Bundle> Build()
        {
            var result = new List<Bundle>();

            var css = new StyleBundle("~/styles")
                .IncludeDirectory("~/Public/Vendor", "*.css", true)
                .IncludeDirectory("~/Public/Css", "*.css")
                .IncludeDirectory("~/Public/App/core", "*.css", true);
            css.Transforms.Add(new CssUrlTransform());

            var js = new ScriptBundle("~/scripts")
                .Include("~/Public/Vendor/jquery-2.1.1.js", "~/Public/Vendor/moment-with-locales.js", "~/Public/Vendor/angular/angular.js")
                .IncludeDirectory("~/Public/Vendor/", "*.js", true);

            var less = new LessBundle("~/less")
                .IncludeDirectory("~/Public/", "*.less", true);

            var angular = new ScriptBundle("~/app")
                .Include("~/Public/App/app.js")
                .IncludeDirectory("~/Public/App", "*.js", true);

            js.Transforms.Clear();
            js.Transforms.Add(new AngularJsTransform());
            angular.Transforms.Clear();
            angular.Transforms.Add(new AngularJsTransform());

            var html = new TemplateBundle("~/templates", new TemplateCompilerOptions { ModuleName = "app", Standalone = false })
                .IncludeDirectory("~/Public/App/", "*.html", true);

            result.Add(css);
            result.Add(js);
            result.Add(angular);
            result.Add(html);
            result.Add(less);
            return result;
        }
    }
}
