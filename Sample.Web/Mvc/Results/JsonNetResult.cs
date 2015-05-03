using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Sample.Web.Mvc.Results
{
    public class JsonNetResult : JsonResult
    {
        public JsonSerializerSettings Settings { get; private set; }
        public JsonNetResult()
        {
            JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            Settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Error,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new StringEnumConverter{CamelCaseText = true}}
            };
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            var response = context.HttpContext.Response;

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

            var scriptSerializer = JsonSerializer.Create(Settings);
            
            scriptSerializer.Serialize(response.Output, Data);
        }
    }
}