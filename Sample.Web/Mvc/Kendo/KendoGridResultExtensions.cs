using System.Net;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Sample.Web.Mvc.Common;
using Sample.Web.Mvc.Results;

namespace Sample.Web.Mvc.Kendo
{
    public static class KendoGridResultExtensions
    {
        public static GridModelResult Success<TBuilder>(this TBuilder builder, DataSourceResult result) where TBuilder : IGridModelBuilderResponse
        {
            return new GridModelResult { HttpStatusCode = HttpStatusCode.OK, Result = result };
        }

        public static JsonResult ToJson(this GridModelResult result)
        {
            return new JsonNetResult { Data = result.Result, JsonRequestBehavior = JsonRequestBehavior.AllowGet, };
        }
    
    }
}