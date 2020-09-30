using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiagnosticModel
{
    public static class Extensions
    {
        public static string ToJson(this object obj, JsonSerializerSettings jsonSerializerSettings = null)
        {
            return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
        }

        public static object ToObj(this string str, JsonSerializerSettings jsonSerializerSettings = null)
        {
            return JsonConvert.DeserializeObject(str, jsonSerializerSettings);
        }

        public static T ToObj<T>(this string str, JsonSerializerSettings jsonSerializerSettings = null)
        {
            return JsonConvert.DeserializeObject<T>(str, jsonSerializerSettings);
        }


        public static string ToStr(this HttpResponse response, Encoding encoding = null)
        {

            if (response.Body.CanRead && response.Body.CanSeek)
            {
                encoding = encoding ?? Encoding.UTF8;
                StreamReader reader = new StreamReader(response.Body, encoding);
                var result = reader.ReadToEndAsync().ConfigureAwait(false).GetAwaiter().GetResult();

                return result;
            }
            return null;
        }
        public static async Task<string> ToStr(this HttpRequest request)
        {
            if (request.ContentLength > 0)
            {
                await EnableRewindAsync(request).ConfigureAwait(false);
                var encoding = GetRequestEncoding(request);
                return await ReadStreamAsync(request.Body, encoding).ConfigureAwait(false);
            }
            return null;
        }


        public static string ToStr(this HttpResponse response)
        {
            if (response.ContentLength > 0)
            {
                response.Body?.ToBuffer()?.ToStr();
            }
            return null;
        }

        public static byte[] ToBuffer(this Stream sm)
        {
            byte[] buffer = new byte[sm.Length];
            sm.Write(buffer, 0, buffer.Length);
            return buffer;
        }


        public static string ToStr(this byte[] buffer)
        {
            return Encoding.UTF8.GetString(buffer);
        }

        public static TraceInfoBuilder ToTraceInfo(this HttpContext context, Exception exception)
        {
            return context.ToTraceInfoBuilder(null, exception);
        }

        public static TraceInfoBuilder ToTraceInfo(this HttpContext context, string id)
        {
            return context.ToTraceInfoBuilder(id, null);
        }


        public static TraceInfoBuilder ToTraceInfoBuilder(this HttpContext context, string id = null, Exception exception = null)
        {
            var request = context.Request;
            var parentTrackId = request.Headers["parent-track-id"].FirstOrDefault();
            string parentid = null;
            if (string.IsNullOrEmpty(id))
            {
                id = request.Headers["chain-id"].FirstOrDefault();
            }
            else
            {
                parentid = request.Headers["chain-id"].FirstOrDefault();
            }


            var trackId = request.Headers["track-id"].FirstOrDefault();
            var trackTime = request.Headers["track-time"].FirstOrDefault();
            var elapsedTime = (DateTime.Now.Ticks - Convert.ToInt64(trackTime)) / 1000000;//转换为ms


            var tranceInfoBuilder = TraceInfoBuilder.CreateBuilder().BuildTraceInfo(id).ParentId(parentid).TrackId(trackId, parentTrackId)
                   .HttpContext(context).ElapsedTime(elapsedTime).Log(LogLevel.Trace.ToString(), "", exception);
            return tranceInfoBuilder;
        }




        #region  private


        private static Encoding GetRequestEncoding(HttpRequest request)
        {
            var requestContentType = request.ContentType;
            var requestMediaType = requestContentType == null ? default(MediaType) : new MediaType(requestContentType);
            var requestEncoding = requestMediaType.Encoding;
            if (requestEncoding == null)
            {
                requestEncoding = Encoding.UTF8;
            }
            return requestEncoding;
        }

        private static async Task EnableRewindAsync(HttpRequest request)
        {
            if (!request.Body.CanSeek)
            {
                request.EnableBuffering();

                await request.Body.DrainAsync(CancellationToken.None);
                request.Body.Seek(0L, SeekOrigin.Begin);
            }
        }

        private static async Task<string> ReadStreamAsync(Stream stream, Encoding encoding)
        {
            using (StreamReader sr = new StreamReader(stream, encoding, true, 1024, true))//这里注意Body部分不能随StreamReader一起释放
            {
                var str = await sr.ReadToEndAsync();
                stream.Seek(0, SeekOrigin.Begin);//内容读取完成后需要将当前位置初始化，否则后面的InputFormatter会无法读取
                return str;
            }
        }
        #endregion
    }
}
