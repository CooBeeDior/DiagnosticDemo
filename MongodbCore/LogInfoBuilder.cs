using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Threading;
namespace MongodbCore
{
    public class LogInfoBuilder
    {
        private LogInfo _loginfo;
        private LogInfoBuilder() { }

        public static LogInfoBuilder CreateBuilder()
        {
            return new LogInfoBuilder();
        }

        public LogInfoBuilder FromLogInfo(LogInfo loginfo)
        {
            if (loginfo == null)
            {
                throw new ArgumentException($"loginfo not null");
            }
            _loginfo = loginfo;
            return this;
        }

        public LogInfoBuilder LogInfoBuild(string Id)
        {
            _loginfo = new LogInfo(Id);
            _loginfo.ServerName = AppDomain.CurrentDomain.FriendlyName;
            _loginfo.HostIPAddress = Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.FirstOrDefault(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.ToString();
            _loginfo.ThreadId = Thread.CurrentThread.ManagedThreadId;
            _loginfo.ThreadName = Thread.CurrentThread.Name;
            _loginfo.CreateAt = DateTimeOffset.Now;

            return this;
        }




        public LogInfoBuilder BuildHttpContext(HttpContext context)
        {
            context.Request.EnableBuffering();

            _loginfo.Request = context.Request?.ToStr().Result;
            _loginfo.Response = context.Response?.Body?.ToStr();
            _loginfo.Header = context.Request?.Headers?.ToJson();
            _loginfo.Cookies = context.Request?.Cookies?.ToJson();
            _loginfo.Method = context.Request?.Method;
            _loginfo.Url = context.Request.Path;
            _loginfo.StatusCode = context.Response.StatusCode;
            _loginfo.HostIPAddress = context.Request.Host.Value;
            _loginfo.ClientIpAddress = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
            return this;
        }
        public LogInfoBuilder BuildRequest(string request)
        {
            _loginfo.Request = request;
            return this;
        }
        public LogInfoBuilder BuildResponse(string response)
        {
            _loginfo.Response = response;
            return this;
        }

        public LogInfoBuilder BuildParentId(string parentId)
        {
            _loginfo.ParentId = parentId;
            return this;
        }
        public LogInfoBuilder BuildLog(string logLevel, string logName, Exception exception = null)
        {
            _loginfo.LogName = logName;
            _loginfo.LogLevel = logLevel;
            _loginfo.Exception = exception;
            return this;
        }


        public LogInfoBuilder BuildException(Exception exception)
        {
            _loginfo.Exception = exception;
            return this;
        }

        public LogInfoBuilder BuildTrackId(string trackId, string parentTrackId = null)
        {
            _loginfo.TrackId = trackId;
            _loginfo.ParentTrackId = parentTrackId;
            return this;
        }

        public LogInfoBuilder BuildParentTrackId(string parentTrackId)
        {
            _loginfo.ParentTrackId = parentTrackId;
            return this;
        }

        public LogInfoBuilder BuildElapsedTime(long elapsedTime)
        {
            _loginfo.ElapsedTime = elapsedTime;
            return this;
        }


        public LogInfo Build()
        {
            return _loginfo;
        }



    }
}
