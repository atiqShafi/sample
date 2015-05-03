using System.Net;
using Sample.Web.Mvc.Common;
using Sample.Web.Mvc.Results;

namespace Sample.Web.Mvc.Extensions
{
    /// <summary>
    /// Helpers for model handlers
    /// </summary>
    public static class ModelHandlerExtensions
    {
        public static ModelHandlerResult Success<T>(this T handler,string message) where T : IModelHandlerResponse
        {
            return new ModelHandlerResult {HttpStatusCode = HttpStatusCode.OK, Message = message};
        }

        public static ModelHandlerResult Success<T>(this T handler,string message, object data) where T : IModelHandlerResponse
        {
            return new ModelHandlerResult { HttpStatusCode = HttpStatusCode.OK, Data = data,Message = message};
        }

        public static ModelHandlerResult Success<T>(this T handler) where T : IModelHandlerResponse
        {
            return new ModelHandlerResult { HttpStatusCode = HttpStatusCode.OK };
        }
        
        public static ModelHandlerResult Error<T>(this T handler, string error) where T : IModelHandlerResponse
        {
            return new ModelHandlerResult { HttpStatusCode = HttpStatusCode.BadRequest, Message = error };
        }

        public static ModelHandlerResult Error<T>(this T handler, string property,string error) where T : IModelHandlerResponse
        {
            return new ModelHandlerResult { HttpStatusCode = HttpStatusCode.BadRequest, Message = error,Property = property};
        }

        public static ModelHandlerResult NotFound<T>(this T handler, string error) where T : IModelHandlerResponse
        {
            return new ModelHandlerResult { HttpStatusCode = HttpStatusCode.NotFound, Message = error };
        }


    }
}