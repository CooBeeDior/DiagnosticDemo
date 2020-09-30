using DiagnosticModel;
using Nest;
using System;

namespace ElasticSearchCore.Models
{ 
    [ElasticsearchType(IdProperty = "Id", RelationName = "tranceInfo")]
    public class EsTraceInfo : TraceInfo
    {
        public EsTraceInfo(string id) : base(id)
        {
        }
        [Keyword]
        public override string Id { get; set; }
        [Keyword]
        public override string ParentId { get; set; }
        [Text]
        public override string ServerName { get; set; }
        [Keyword]
        public override string TrackId { get; set; }
        [Keyword]
        public override string ParentTrackId { get; set; }
        [Text]
        public override string LogName { get; set; }
        [Keyword]
        public override string LogLevel { get; set; }
        [Object]
        public override TraceInfoRequest Request { get; set; }
        [Object]
        public override TraceInfoResponse Response { get; set; }

        [Keyword]
        public override long ElapsedTime { get; set; }
        [Date]
        public override DateTimeOffset CreateAt { get; set; }
        [Ignore]
        public override Exception Exception { get; set; }
        [Text]
        public override string ErrorMessage { get; set; }

        //public override string ErrorStackTrace { get; set; }

        [Keyword]
        public override string HostIPAddress { get; set; }
        [Keyword]
        public override string ClientIpAddress { get; set; }

        [Keyword]
        public override int ThreadId { get; set; }

        [Keyword]
        public override string ThreadName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Text]
        public override string Description { get; set; }
    }



 
   
}
