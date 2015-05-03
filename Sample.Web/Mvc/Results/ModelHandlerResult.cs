using System.Net;

namespace Sample.Web.Mvc.Results
{
    public class ModelHandlerResult
    {
        public string Message { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Property { get; set; }
        public object Data { get; set; }
    }
}