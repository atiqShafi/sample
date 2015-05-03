using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sample.Web.Mvc.Results;

namespace Sample.Web.Mvc.Attributes
{
    /// <summary>
    /// Auto vlaidate input model on request
    /// </summary>
    public class ModelStateValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var modelState = filterContext.Controller.ViewData.ModelState;
            if (!modelState.IsValid)
            {
                var errors = new Dictionary<string, string>();

                foreach (var property in modelState.Keys)
                {
                    var exception = modelState[property].Errors.Select(s => s.Exception).FirstOrDefault();
                    if (exception != null)
                    {
                        throw exception;
                    }

                    if (modelState[property].Errors.Any())
                    {
                        errors.Add(property, modelState[property].Errors.Select(s => s.ErrorMessage).FirstOrDefault());
                    }
                }

                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                filterContext.Result = new JsonNetResult {Data = new {errors}};
            }
            base.OnActionExecuting(filterContext);
        }
    }
}