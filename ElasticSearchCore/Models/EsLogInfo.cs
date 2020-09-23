using DiagnosticCore.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchCore.Models
{
    [ElasticsearchType(IdProperty = "Id", RelationName = "loginfo")]
    public class EsLogInfo : LogInfo
    {
        public EsLogInfo(string id) : base(id)
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

        [Keyword]
        public override string Url { get; set; }
        [Keyword]
        public override string Method { get; set; }
        [Text]
        public override string Request { get; set; }
        [Text]
        public override string Response { get; set; }
        [Text]
        public override string Header { get; set; }
        [Text]
        public override string Cookies { get; set; }
        [Keyword]
        public override int? StatusCode { get; set; }
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
    }
}
