using System.Collections.Generic;
using System.Web;
using Sample.Core.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Sample.Web.Mvc.Authentication;
using Sample.Web.Mvc.Common;
using Sample.Web.Mvc.Extensions;
using Sample.Web.Mvc.Results;

namespace Sample.Web.Modules.App.Builders
{
    public class AppModel
    {
        public string LoggedUser { get; set; }
        public string IsDebugMode { get; set; }
    }
    /// <summary>
    /// Entry application builder
    /// </summary>
    public class AppModelBuilder : IModelBuilder<AppModel>
    {
        private readonly ICurrentUser _currentUser;

        public AppModelBuilder(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }
            
        public ModelBuilderResult<AppModel> Build()
        {            
            return this.Success(new AppModel
            {
                LoggedUser = _currentUser.GetCurrentUser().SerializeToJson(),
                IsDebugMode = HttpContext.Current.IsDebuggingEnabled.SerializeToJson(),
            });
        }
    }
}