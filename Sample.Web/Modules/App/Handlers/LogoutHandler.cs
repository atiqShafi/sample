using System.Web;
using System.Web.Security;
using Sample.Web.Mvc.Authentication;
using Sample.Web.Mvc.Common;
using Sample.Web.Mvc.Extensions;
using Sample.Web.Mvc.Results;

namespace Sample.Web.Modules.App.Handlers
{    
    /// <summary>
    /// Handle logout user
    /// </summary>
    public class LogoutHandler : IModelHandler<HttpRequestBase,LogoutHandler>
    {
        private readonly ICurrentUser _currentUser;

        public LogoutHandler(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }


        public ModelHandlerResult Handle(HttpRequestBase request)
        {
            var user = _currentUser.GetCurrentUser();
            if (user == null)
                return this.Error("No logged user found");

            FormsAuthentication.SignOut();
            return this.Success();
        }
    }
}