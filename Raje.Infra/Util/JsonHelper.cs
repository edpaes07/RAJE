using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Raje.Infra.Util
{
    public static class JsonHelper
    {
        public static JsonSerializerSettings StandardFormat()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };

            return jsonSerializerSettings;
        }

        public static string ToJsonFormat(object obj)
        {
            return JsonConvert.SerializeObject(obj, StandardFormat());
        }

        public static T SafeJsonParser<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return default(T);

            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
            }

            return default(T);
        }

        public class JsonConverter : StringContent
        {
            public JsonConverter(object o) : base(JsonHelper.ToJsonFormat(o), Encoding.UTF8, "application/json")
            {
            }
        }
    }
}