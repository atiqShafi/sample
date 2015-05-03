using System.Web.Mvc;
using Kendo.Mvc.UI;
using Sample.Web.Modules.Users.Builders;
using Sample.Web.Modules.Users.Handlers;
using Sample.Web.Mvc.Attributes;
using Sample.Web.Mvc.Common;
using Sample.Web.Mvc.Extensions;
using Sample.Web.Mvc.Kendo;

namespace Sample.Web.Modules.Users
{
    /// <summary>
    /// Users crud sample
    /// </summary>
    [RoutePrefix("users")]
    [CheckAuthorization]
    public class UsersController : Controller
    {
        /// <summary>
        /// Load list of users to kendo grid
        /// </summary>
        [HttpGet, Route("users-grid")]
        public ActionResult UsersGrid([DataSourceRequest]DataSourceRequest request)
        {
            return Builder.Get<UsersGridBuilder>().Build(request).ToJson();
        }

        /// <summary>
        /// Handle create user request
        /// </summary>
        [HttpPost, Route("create")]
        public ActionResult CrateUser(CreateUserModel model)
        {
            return Handler.Get<CreateUserHandler>().Handle(model).ToJson();
        }

        /// <summary>
        /// Handle edit user get request
        /// </summary>
        [HttpGet, Route("edit/{userId:int}")]
        public ActionResult EditUser(int userId)
        {
            return Builder.Get<EditUserBuilder>().Build(userId).ToJson();
        }

        /// <summary>
        /// Handle edit user post request
        /// </summary>
        [HttpPost, Route("edit/{userId:int}")]
        public ActionResult EditUser(EditUserModel model)
        {
            return Handler.Get<EditUserHandler>().Handle(model).ToJson();
        }

        /// <summary>
        /// Handle delete user request
        /// </summary>
        [HttpPost, Route("delete/{userId:int}")]
        public ActionResult DeleteUser(int userId)
        {
            return Handler.Get<DeleteUserHandler>().Handle(userId).ToJson();
        }


    }

}