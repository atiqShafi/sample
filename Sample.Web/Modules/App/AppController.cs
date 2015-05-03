using System;
using System.Web.Mvc;
using Sample.Web.Modules.App.Builders;
using Sample.Web.Modules.App.Handlers;
using Sample.Web.Mvc.Attributes;
using Sample.Web.Mvc.Common;
using Sample.Web.Mvc.Extensions;
using Sample.Web.Mvc.Results;

namespace Sample.Web.Modules.App
{
    public class AppController : Controller
    {
        /// <summary>
        /// Entry application point
        /// </summary>
        [HttpGet]
        public ActionResult Index()
        {
            var result = Builder.Get<AppModelBuilder>().Build();
            return View("~/Public/App/index.cshtml", result.Model);
        }

        /// <summary>
        /// Handle login request
        /// </summary>
        [HttpPost, Route("login")]
        public ActionResult Login(LoginModel model)
        {
            return Handler.Get<LoginHandler>().Handle(model).ToJson();
        }

        /// <summary>
        /// Handle logout request
        /// </summary>
        [HttpPost, Route("logout")]
        [CheckAuthorization]
        public ActionResult Logout()
        {
            return Handler.Get<LogoutHandler>().Handle(Request).ToJson();
        }

        /// <summary>
        /// Kendo grid export helper
        /// </summary>
        [HttpPost,Route("grid-export")]
        [CheckAuthorization]
        public ActionResult GridExcelExport(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);
            return File(fileContents, contentType, fileName);
        }

        /// <summary>
        /// Elmah helper
        /// </summary>
        [HttpGet, Route("log/{type?}")]
        [CheckAuthorization]
        public ElmahResult Log(string type)
        {
            return new ElmahResult(type);
        }

    }

}