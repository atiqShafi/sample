using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Sample.Core.Common
{
    public static class SerilizationExtensions
    {
        public static string SerializeToJson(this object objcetToSerialize)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new StringEnumConverter { CamelCaseText = true } }
            };
            return JsonConvert.SerializeObject(objcetToSerialize, settings);
        }
    }
}