using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SimpleStore.Models.Product;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;

namespace SimpleStore.Session
{
    public static class SessitonSaver
    {
        public static void SetObjectAsJson(this ISession session , string key , object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session , string key)
        {
            var value = session.GetString(key);
            return value is null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
