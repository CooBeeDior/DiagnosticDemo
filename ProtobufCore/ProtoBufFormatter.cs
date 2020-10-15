using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using ProtoBuf;
using ProtoBuf.Meta;

namespace ProtobufCore
{
    // <summary>
    /// 处理MIME协议为application/x-protobuf 类 
    /// 注:在.net core 不需要特别添加对application/x-protobuf协议的支持
    /// .net freamwork 若不加此协议支持网络请求的时候会→远程服务器异常:(415) Unsupported Media Type
    /// </summary>
    public class ProtoBufFormatter : MediaTypeFormatter
    {
        private static readonly MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/x-protobuf");
        private static Lazy<RuntimeTypeModel> model = new Lazy<RuntimeTypeModel>(CreateTypeModel);

        public static RuntimeTypeModel Model
        {
            get { return model.Value; }
        }

        public ProtoBufFormatter()
        {
            SupportedMediaTypes.Add(mediaType);
        }

        public static MediaTypeHeaderValue DefaultMediaType
        {
            get { return mediaType; }
        }

        public override bool CanReadType(Type type)
        {
            var temp = CanReadTypeCore(type);
            return temp;
        }

        public override bool CanWriteType(Type type)
        {
            return CanReadTypeCore(type);
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var tcs = new TaskCompletionSource<object>();

            try
            {
                object result = Model.Deserialize(stream, null, type);
                tcs.SetResult(result);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }

            return tcs.Task;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContent content, TransportContext transportContext)
        {
            var tcs = new TaskCompletionSource<object>();

            try
            {
                Model.Serialize(stream, value);
                tcs.SetResult(null);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }

            return tcs.Task;
        }

        private static RuntimeTypeModel CreateTypeModel()
        {
            var typeModel = RuntimeTypeModel.Create();
            typeModel.UseImplicitZeroDefaults = false;
            return typeModel;
        }

        private static bool CanReadTypeCore(Type type)
        {
            bool isCan = type.GetCustomAttributes(typeof(ProtoContractAttribute)).Any();

            if (!isCan && typeof(IEnumerable).IsAssignableFrom(type))
            {
                var temp = type.GetGenericArguments().FirstOrDefault();
                isCan = temp.GetCustomAttributes(typeof(ProtoContractAttribute)).Any();
            }

            return isCan;
        }
    }
}
