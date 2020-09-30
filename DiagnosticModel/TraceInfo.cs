using FreeSql.DataAnnotations;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DiagnosticModel
{
    [Serializable]
    public class TraceInfo
    {
        public TraceInfo(string id)
        {
            this.Id = id;
        }
        public virtual string Id { get; set; }
        /// <summary>
        /// 父级别追踪Id(当前服务)
        /// </summary>
        public virtual string ParentId { get; set; }
        /// <summary>
        /// 当前服务名称
        /// </summary>
        public virtual string ServerName { get; set; }
        /// <summary>
        /// 追踪Id(服务级别)
        /// </summary>
        public virtual string TrackId { get; set; }
        /// <summary>
        /// 日志名称
        /// </summary>
        public virtual string LogName { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public virtual string LogLevel { get; set; }
        /// <summary>
        /// 父级别追踪Id（服务级别）
        /// </summary>
        public virtual string ParentTrackId { get; set; }

        [JsonMap]
        [Column(StringLength =-1)]
        public virtual TraceInfoRequest Request { get; set; }
        [JsonMap]
        [Column(StringLength = -1)]
        public virtual TraceInfoResponse Response { get; set; }

        /// <summary>
        /// 运行时间
        /// </summary>
        public virtual long ElapsedTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTimeOffset CreateAt { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public virtual Exception Exception { get; set; }
        /// <summary>
        /// 错误描述
        /// </summary>
        public virtual string ErrorMessage { get; set; }

        //public virtual string ErrorStackTrace { get; set; }

        /// <summary>
        /// 当前服务Ip地址
        /// </summary>
        public virtual string HostIPAddress { get; set; }
        /// <summary>
        /// 客户端调用服务Ip地址
        /// </summary>
        public virtual string ClientIpAddress { get; set; }

        /// <summary>
        /// 线程Id
        /// </summary>
        public virtual int ThreadId { get; set; }

        /// <summary>
        /// 线程名称
        /// </summary>
        public virtual string ThreadName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }


        public virtual TraceInfo Clone()
        {
            return ToTTraceInfo<TraceInfo>();
        }


        public virtual TTraceInfo ToTTraceInfo<TTraceInfo>()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (TTraceInfo)formatter.Deserialize(stream);
            }
        }


    }

    [Serializable]
    public class TraceInfoRequest
    {
        #region 请求内容

        /// <summary>
        /// 请求内容
        /// </summary>
        public virtual string Body { get; set; }
        /// <summary>
        /// 请求url
        /// </summary>
        public virtual string Url { get; set; }
        /// <summary>
        /// 请求方法
        /// </summary>
        public virtual string Method { get; set; }
        /// <summary>
        /// 头部信息
        /// </summary>
        public virtual string Header { get; set; }
        /// <summary>
        /// Cookies
        /// </summary>
        public virtual string Cookies { get; set; }
        #endregion
    }
    [Serializable]
    public class TraceInfoResponse
    {
        #region 返回内容

        /// <summary>
        /// 返回内容
        /// </summary>
        public virtual string Body { get; set; }
        /// <summary>
        /// 返回状态码
        /// </summary>
        public virtual int? StatusCode { get; set; }
        #endregion
    }
}
