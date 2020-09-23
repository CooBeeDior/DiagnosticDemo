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
        public virtual string Id { get; set; }

        public virtual string ParentId { get; set; }

        public virtual string ServerName { get; set; }

        public virtual string TrackId { get; set; }
        public virtual string ParentTrackId { get; set; }
        public virtual string LogName { get; set; }

        public virtual string LogLevel { get; set; }


        public virtual string Url { get; set; }

        public virtual string Method { get; set; }

        public virtual string Request { get; set; }

        public virtual string Response { get; set; }

        public virtual string Header { get; set; }

        public virtual string Cookies { get; set; }

        public virtual int? StatusCode { get; set; }

        public virtual long ElapsedTime { get; set; }

        public virtual DateTimeOffset CreateAt { get; set; }
      
        public virtual Exception Exception { get; set; }

        public virtual string ErrorMessage { get; set; }

        //public virtual string ErrorStackTrace { get; set; }


        public virtual string HostIPAddress { get; set; }

        public virtual string ClientIpAddress { get; set; }


        public virtual int ThreadId { get; set; }


        public virtual string ThreadName { get; set; }

        public virtual LogInfo Clone()
        {
            return this.MemberwiseClone() as LogInfo;
        }

    }
}
