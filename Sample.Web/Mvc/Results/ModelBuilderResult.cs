using System.Net;
using Sample.Web.Mvc.Common;

namespace Sample.Web.Mvc.Results
{
    public class ModelBuilderResult<TModel> : IModelBuilderResponse
    {
        public string Message { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public TModel Model { get; set; }         
    }
}