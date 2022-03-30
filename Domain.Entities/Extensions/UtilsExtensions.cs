using System;
using System.Linq;
using Newtonsoft.Json;

namespace Domain.Entities.Extensions
{
    public static class UtilsExtensions
    {
        public static string ToJson(this object jsonObject)
        {
            return JsonConvert.SerializeObject(jsonObject);
        }

        public static string ToJsonIgnoringNulls(this object jsonObject)
        {
            return JsonConvert.SerializeObject(jsonObject, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }

        public static T ToObject<T>(this string jsonString) where T : class
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static dynamic GetTableName(this Type type)
        {

            dynamic tableattr = type.GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "TableAttribute");

            return tableattr;
        }
    }
}
