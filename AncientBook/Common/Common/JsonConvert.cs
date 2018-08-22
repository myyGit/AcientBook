using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class JSONSerializer
    {
        public static string ToJSONString(this object obj)
        {
            string aa = JsonConvert.SerializeObject(obj);
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            });
        }
        public static T FromJSONString<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static string GetValue(this string str, string key)
        {
            var jtoken = JToken.Parse(str);
            var keyJToken = jtoken.SelectToken(key);
            if (keyJToken == null)
            {
                return null;
            }
            else
            {
                return keyJToken.ToString();
            }
        }
    }
}
