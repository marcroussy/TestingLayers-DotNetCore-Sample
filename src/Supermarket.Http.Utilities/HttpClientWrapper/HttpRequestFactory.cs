using System.Net.Http;
using System.Threading.Tasks;
using Supermarket.Infrastructure.Http.Utilities.Authentication;

namespace Supermarket.Infrastructure.Http.Utilities
{
    public static class HttpRequestFactory
    {
        public static async Task<HttpResponseMessage> GetAnonymous(string requestUri, string resource)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Get)
                                .AddRequestUri($"{requestUri}/{resource}");

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Get(string requestUri, string resource)
        {
            var token = await AuthenticationHelper.GetToken(requestUri);

            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Get)
                                .AddBearerToken(token)
                                .AddRequestUri($"{requestUri}/{resource}");
 
            return await builder.SendAsync();
        }
 
        public static async Task<HttpResponseMessage> Post(string requestUri, string resource, object value)
        {
            var token = await AuthenticationHelper.GetToken(requestUri);

            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddBearerToken(token)
                                .AddRequestUri($"{requestUri}/{resource}")
                                .AddContent(new JsonContent(value)) ;
 
            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Post(string requestUri, string resource, object value, string token)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddBearerToken(token)
                                .AddRequestUri($"{requestUri}/{resource}")
                                .AddContent(new JsonContent(value));

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> PostAnonymous(string requestUri, string resource, object value)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri($"{requestUri}/{resource}")
                                .AddContent(new JsonContent(value));

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Put(string requestUri, string resource, object value)
        {
            var token = await AuthenticationHelper.GetToken(requestUri);

            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Put)
                                .AddBearerToken(token)
                                .AddRequestUri($"{requestUri}/{resource}")
                                .AddContent(new JsonContent(value));
 
            return await builder.SendAsync();
        }
 
        public static async Task<HttpResponseMessage> Patch(string requestUri, string resource, object value)
        {
            var token = await AuthenticationHelper.GetToken(requestUri);

            var builder = new HttpRequestBuilder()
                                .AddMethod(new HttpMethod("PATCH"))
                                .AddBearerToken(token)
                                .AddRequestUri($"{requestUri}/{resource}")
                                .AddContent(new JsonContent(value));
 
            return await builder.SendAsync();
        }
 
        public static async Task<HttpResponseMessage> Delete(string requestUri, string resource)
        {
            var token = await AuthenticationHelper.GetToken(requestUri);

            var builder = new HttpRequestBuilder()
                                .AddBearerToken(token)
                                .AddMethod(HttpMethod.Delete)
                                .AddRequestUri($"{requestUri}/{resource}");
 
            return await builder.SendAsync();
        }
    }
}