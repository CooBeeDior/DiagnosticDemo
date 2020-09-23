using System;
using System.Collections.Generic;
using System.Text;

namespace DiagnosticCore.Models
{
    public class LogInfo
    {
        public LogInfo(string id)
        {
            this.Id = id;
        }
        public string Id { get; set; }

        public string ParentId { get; set; }

        public string ServerName { get; set; }

        public string TrackId { get; set; }
        public string ParentTrackId { get; set; }
        public string LogName { get; set; }

        public string LogLevel { get; set; }


        public string Url { get; set; }

        public string Method { get; set; }

        public string Request { get; set; }

        public string Response { get; set; }

        public string Header { get; set; }

        public string Cookies { get; set; }

        public int? StatusCode { get; set; }

        public long ElapsedTime { get; set; }

        public DateTimeOffset CreateAt { get; set; }

        public Exception Exception { get; set; }

        public string ErrorMessage { get; set; }

        public string ErrorStackTrace { get; set; }


        public string HostIPAddress { get; set; }

        public string ClientIpAddress { get; set; }


        public int ThreadId { get; set; }


        public string ThreadName { get; set; }

        public LogInfo Clone()
        {
            return this.MemberwiseClone() as LogInfo;
        }

    }
}
