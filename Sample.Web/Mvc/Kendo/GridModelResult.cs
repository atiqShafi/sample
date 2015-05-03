using System.Net;
using Kendo.Mvc.UI;
using Sample.Web.Mvc.Common;

namespace Sample.Web.Mvc.Kendo
{
    /// <summary>
    /// Kendo grid 
    /// </summary>
    public class GridModelResult : IModelBuilderResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public DataSourceResult Result { get; set; }
    }
}