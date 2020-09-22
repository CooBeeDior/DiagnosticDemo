using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MongodbCore
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

        public static object ToObj<T>(this string str, JsonSerializerSettings jsonSerializerSettings = null)
        {
            return JsonConvert.DeserializeObject<T>(str, jsonSerializerSettings);
        }


        public static string ToStr(this Stream sm, Encoding encoding = null)
        {
            if (sm.CanRead && sm.CanSeek)
            { 
                encoding = encoding ?? Encoding.UTF8;
                StreamReader reader = new StreamReader(sm, encoding);
                var result= reader.ReadToEndAsync().ConfigureAwait(false).GetAwaiter().GetResult();
               
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


        public static byte[] ToBuffer(this Stream sm)
        {
            byte[] buffer = new byte[sm.Length];
            sm.Write(buffer, 0, buffer.Length);
            return buffer;
        }

        public static LogInfo ToLogInfo(this HttpContext context, Exception exception)
        {
            return context.ToLogInfo(null, exception);
        }

        public static LogInfo ToLogInfo(this HttpContext context, string id)
        {
            return context.ToLogInfo(id, null);
        }


        public static LogInfo ToLogInfo(this HttpContext context, string id = null, Exception exception = null)
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


            var loginfo = LogInfoBuilder.CreateBuilder().LogInfoBuild(id).BuildParentId(parentid).BuildTrackId(trackId, parentTrackId)
                   .BuildHttpContext(context).BuildElapsedTime(elapsedTime).BuildLog(LogLevel.Trace.ToString(), "", exception).Build();
            return loginfo;
        }

        public static void ToPersistence(this LogInfo loginfo, IServiceProvider serviceProvider)
        {
            var mongodb = serviceProvider.GetService<IMongoCollection<BsonDocument>>();
            var bjson = loginfo.ToBsonDocument();
            mongodb.InsertOne(bjson);
        }



        #region 
    

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
