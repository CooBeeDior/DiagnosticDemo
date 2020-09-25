using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace DiagnosticModel
{
    public class LogInfoBuilder
    {
        private LogInfo _loginfo;
        private LogInfoBuilder() { }

        public static LogInfoBuilder CreateBuilder()
        {
            return new LogInfoBuilder();
        }
        public LogInfoBuilder BuildLogInfo(string Id)
        {
            _loginfo = new LogInfo(Id);
            _loginfo.ServerName = AppDomain.CurrentDomain.FriendlyName;
            _loginfo.HostIPAddress = Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.FirstOrDefault(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.ToString();
            _loginfo.ThreadId = Thread.CurrentThread.ManagedThreadId;
            _loginfo.ThreadName = Thread.CurrentThread.Name;
            _loginfo.CreateAt = DateTimeOffset.Now;

            return this;
        }
        public LogInfoBuilder BuildFromLogInfo(LogInfo loginfo)
        {
            if (loginfo == null)
            {
                throw new ArgumentException($"loginfo not null");
            }
            _loginfo = loginfo.Clone();
            _loginfo.Id = Guid.NewGuid().ToString();
            _loginfo.ThreadId = Thread.CurrentThread.ManagedThreadId;
            _loginfo.ThreadName = Thread.CurrentThread.Name;
            _loginfo.CreateAt = DateTimeOffset.Now;
            return this;
        }

        public LogInfoBuilder ClearLogInfo()
        {
            _loginfo = null;
            return this;
        }


        public LogInfoBuilder ChangeId(string Id)
        {
            _loginfo.Id = Id;
            return this;
        }

        public LogInfoBuilder HttpContext(HttpContext context)
        {
            context.Request.EnableBuffering();

            _loginfo.Request = context.Request?.ToStr().Result;
            _loginfo.Response = context.Response?.ToStr();
            _loginfo.Header = context.Request?.Headers?.ToJson();
            _loginfo.Cookies = context.Request?.Cookies?.ToJson();
            _loginfo.Method = context.Request?.Method;
            _loginfo.Url = context.Request.Path;
            _loginfo.StatusCode = context.Response.StatusCode;
            _loginfo.HostIPAddress = context.Request.Host.Value;
            _loginfo.ClientIpAddress = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
            return this;
        }


        public LogInfoBuilder HttpRequest(HttpRequest request)
        {
            _loginfo.Request = request?.ToStr().Result;
            _loginfo.Header = request?.Headers?.ToJson();
            _loginfo.Cookies = request?.Cookies?.ToJson();
            _loginfo.Method = request?.Method;
            _loginfo.Url = request.Path;

            _loginfo.HostIPAddress = request.Host.Value;
            _loginfo.ClientIpAddress = Util.GetServerIps();
            return this;
        }

        public LogInfoBuilder HttpRequestMessage(HttpRequestMessage requestMessage)
        {
            _loginfo.Request = requestMessage?.Content?.ReadAsStringAsync().Result;
            _loginfo.Header = requestMessage?.Headers?.ToJson();
            _loginfo.Cookies = requestMessage?.Content?.ToJson();
            _loginfo.Method = requestMessage?.Method?.Method;
            _loginfo.Url = requestMessage.RequestUri.AbsoluteUri;

            _loginfo.HostIPAddress = requestMessage.RequestUri.Host;
            _loginfo.ClientIpAddress = Util.GetServerIps();
            return this;
        }

        public LogInfoBuilder HttpResponseMessage(HttpResponseMessage responseMessage)
        {
            _loginfo.Response = responseMessage.Content.ReadAsStringAsync().Result;
            _loginfo.StatusCode = (int)responseMessage.StatusCode;
            return this;
        }
        public LogInfoBuilder Request(string request)
        {
            _loginfo.Request = request;
            return this;
        }
        public LogInfoBuilder Response(string response)
        {
            _loginfo.Response = response;
            return this;
        }

        public LogInfoBuilder ParentId(string parentId)
        {
            _loginfo.ParentId = parentId;
            return this;
        }
        public LogInfoBuilder Log(string logLevel, string logName, Exception exception = null)
        {
            _loginfo.LogName = logName;
            _loginfo.LogLevel = logLevel;
            _loginfo.Exception = exception;
            return this;
        }


        public LogInfoBuilder Exception(Exception exception)
        {
            _loginfo.Exception = exception;
            return this;
        }

        public LogInfoBuilder TrackId(string trackId, string parentTrackId = null)
        {
            _loginfo.TrackId = trackId;
            _loginfo.ParentTrackId = parentTrackId;
            return this;
        }

        public LogInfoBuilder ParentTrackId(string parentTrackId)
        {
            _loginfo.ParentTrackId = parentTrackId;
            return this;
        }

        public LogInfoBuilder ElapsedTime(long elapsedTime)
        {
            _loginfo.ElapsedTime = elapsedTime;
            return this;
        }
        public LogInfoBuilder StatusCode(int? statusCode)
        {
            _loginfo.StatusCode = statusCode;
            return this;
        }

        public LogInfoBuilder Description(string description)
        {
            _loginfo.Description = description;
            return this;
        }
        public LogInfo Build()
        {
            return _loginfo;
        }



    }
}
