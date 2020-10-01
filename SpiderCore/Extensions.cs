using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace SpiderCore
{
    public static class Extensions
    {
        public static Task<HttpResponseMessage> GetAsync(this ISpiderHttpClient client, string url, Action<HttpRequestHeaders> header = null)
        {
            return client.SendAsync(url, HttpMethod.Get, null, header);
        }

        public static Task<HttpResponseMessage> PostAsync(this ISpiderHttpClient client, string url, HttpContent content = null, Action<HttpRequestHeaders> header = null)
        {
            return client.SendAsync(url, HttpMethod.Post, content, header);
        }
        public static Task<HttpResponseMessage> PutAsync(this ISpiderHttpClient client, string url, HttpContent content = null, Action<HttpRequestHeaders> header = null)
        {
            return client.SendAsync(url, HttpMethod.Put, content, header);
        }

        public static Task<HttpResponseMessage> PatchAsync(this ISpiderHttpClient client, string url, HttpContent content = null, Action<HttpRequestHeaders> header = null)
        {
            return client.SendAsync(url, HttpMethod.Patch, content, header);
        }

        public static Task<HttpResponseMessage> HeadAsync(this ISpiderHttpClient client, string url, HttpContent content = null, Action<HttpRequestHeaders> header = null)
        {
            return client.SendAsync(url, HttpMethod.Head, content, header);
        }


        public static Task<HttpResponseMessage> DeleteAsync(this ISpiderHttpClient client, string url, HttpContent content = null, Action<HttpRequestHeaders> header = null)
        {
            return client.SendAsync(url, HttpMethod.Delete, content, header);
        }
        public static Task<HttpResponseMessage> TraceAsync(this ISpiderHttpClient client, string url, HttpContent content = null, Action<HttpRequestHeaders> header = null)
        {
            return client.SendAsync(url, HttpMethod.Trace, content, header);
        }
        public static Task<HttpResponseMessage> SendAsync(this ISpiderHttpClient client, string url, HttpMethod method, HttpContent content = null, Action<HttpRequestHeaders> header = null)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, url);
            request.Content = content;
            header?.Invoke(request.Headers);
            return client.SendAsync(request);
        }
    }
}
