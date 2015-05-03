using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Optimization;
using Microsoft.Ajax.Utilities;

namespace Sample.Web.Mvc.Bundles
{
    /// <summary>
    /// Css image url workaround in bunbldes
    /// </summary>
    public class CssUrlTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            var pattern = new Regex(@"url\s*\(\s*([""']?)([^:)]+)\1\s*\)", RegexOptions.IgnoreCase);

            response.Content = string.Empty;

            // open each of the files
            foreach (var file in response.Files)
            {
                using (var reader = new StreamReader(file.VirtualFile.Open()))
                {
                    var contents = reader.ReadToEnd();

                    // apply the RegEx to the file (to change relative paths)
                    var matches = pattern.Matches(contents);

                    if (matches.Count > 0)
                    {
                        var directoryPath = VirtualPathUtility.GetDirectory(file.VirtualFile.VirtualPath);

                        foreach (Match match in matches)
                        {
                            // this is a path that is relative to the CSS file
                            var imageRelativePath = match.Groups[2].Value;

                            // get the image virtual path
                            var imageVirtualPath = VirtualPathUtility.Combine(directoryPath, imageRelativePath);

                            // convert the image virtual path to absolute
                            var quote = match.Groups[1].Value;
                            var replace = String.Format("url({0}{1}{0})", quote, VirtualPathUtility.ToAbsolute(imageVirtualPath));
                            contents = contents.Replace(match.Groups[0].Value, replace);
                        }

                    }
                    // copy the result into the response.
                    response.Content = String.Format("{0}\r\n{1}", response.Content, contents);
                }

                var minifer = new Minifier();
                var cs = new CssSettings();
                var responseContent = minifer.MinifyStyleSheet(response.Content, cs);
                response.Content = responseContent;

            }
        }
    }
}