using Newtonsoft.Json;
using System.ComponentModel;

namespace System
{
    public static class JsonExtensions
    {
        public static T FromJson<T>(this string jsonString)
        {
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                return JsonConvert.DeserializeObject<T>(jsonString, settings);
            }
            catch
            {
                return default(T);
            }
        }

        public static string ToJson(this object jsonObject)
        {
            return jsonObject.ToJson(false);
        }

        public static string ToJson(this object jsonObject, bool IgnoreNull)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                NullValueHandling = IgnoreNull ? NullValueHandling.Ignore : NullValueHandling.Include
            };
            return JsonConvert.SerializeObject(jsonObject, settings);
        }
    }
}

