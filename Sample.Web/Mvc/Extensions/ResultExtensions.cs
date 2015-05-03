using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sample.Core.Common;
using Sample.Web.Mvc.Results;

namespace Sample.Web.Mvc.Extensions
{
    /// <summary>
    /// Return standarize response
    /// </summary>
    public static class ResultExtensions
    {
        public static JsonResult ToJson(this ModelHandlerResult result)
        {
            if (result.HttpStatusCode == HttpStatusCode.OK)
            {
                return new JsonNetResult
                {
                    Data = new {message = result.Message, data = result.Data},
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            
            HttpContext.Current.Response.StatusCode = (int) result.HttpStatusCode;
            
            if (result.Property.IsNotEmpty())
            {
                return new JsonNetResult {Data = new {errors = new Dictionary<string, string> {{result.Property, result.Message}}}};
            }
            return new JsonNetResult { Data = new { error = result.Message } };
        }

        public static JsonResult ToJson<TModel>(this ModelBuilderResult<TModel> result)
        {
            if (result.HttpStatusCode == HttpStatusCode.OK)
            {
                return new JsonNetResult { Data = result.Model, JsonRequestBehavior = JsonRequestBehavior.AllowGet, };
            }

            HttpContext.Current.Response.StatusCode = (int) result.HttpStatusCode;
            return new JsonNetResult { Data = new { error = result.Message } };
        }

        public static FileResult ToFile(this FileBuilderResult result)
        {
            if (!File.Exists(result.Path))
                throw new FileNotFoundException("");

            var fileStream = new FileStream(result.Path, FileMode.Open,FileAccess.Read);

            return new FileStreamResult(fileStream, result.ContentType)
            {
                FileDownloadName = result.FileName
            };

        }

        public static JsonResult ToJson<TEnum>()
        {
            var data = Enum.GetValues(typeof (TEnum))
                .Cast<TEnum>()
                .Select(s => new
                {
                    value = s.ToString(),
                    description = s.Description()
                });
            
            return new JsonNetResult { Data = data };
        }



    }
}