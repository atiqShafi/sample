using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Mvc.Attributes
{
    /// <summary>
    /// Handle unauthorize request. 
    /// </summary>
    public class CheckAuthorization : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.User.Identity.IsAuthenticated;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = 403;
            filterContext.Result = new EmptyResult();
        }
    }
}