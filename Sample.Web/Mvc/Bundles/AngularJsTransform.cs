using System.IO;
using System.Text;
using System.Web;
using System.Web.Optimization;
using Microsoft.Ajax.Utilities;

namespace Sample.Web.Mvc.Bundles
{
    /// <summary>
    /// Minification in angularjs workaround
    /// </summary>
    public class AngularJsTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            var minifer = new Minifier();
            var cs = new CodeSettings {ManualRenamesProperties = false,LocalRenaming = LocalRenaming.KeepAll};
            var content = new StringBuilder();
            foreach (var file in response.Files)
            {
                var path = HttpContext.Current.Server.MapPath(file.IncludedVirtualPath);
                using (var reader = new StreamReader(path))
                {
                    content.Append(reader.ReadToEnd());
                }
            }
            response.ContentType = "application/javascript";
            var responseContent = minifer.MinifyJavaScript(content.ToString(), cs);
            response.Content = responseContent;
        }
    }
}