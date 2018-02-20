using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Supermarket.Infrastructure.Http.Utilities
{
    public static class HttpResponseExtensions
    {
        public static T ContentAsType<T>(this HttpResponseMessage response)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            return string.IsNullOrEmpty(data) ? 
                            default(T) : 
                            JsonConvert.DeserializeObject<T>(data);
        }
 
        public static dynamic ContentAsDynamic(this HttpResponseMessage response)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            return JObject.Parse(data);
        }

        public static string ContentAsString(this HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}