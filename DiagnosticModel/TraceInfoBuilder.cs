using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace DiagnosticModel
{
    public class TraceInfoBuilder
    {
        private TraceInfo _traceInfo;
        private TraceInfoBuilder() { }

        public static TraceInfoBuilder CreateBuilder()
        {
            return new TraceInfoBuilder();
        }
        public TraceInfoBuilder BuildTraceInfo(string Id)
        {
            _traceInfo = new TraceInfo(Id);
            _traceInfo.ServerName = AppDomain.CurrentDomain.FriendlyName;
            _traceInfo.HostIPAddress = Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.FirstOrDefault(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.ToString();
            _traceInfo.ThreadId = Thread.CurrentThread.ManagedThreadId;
            _traceInfo.ThreadName = Thread.CurrentThread.Name;
            _traceInfo.CreateAt = DateTimeOffset.Now;

            return this;
        }
        public TraceInfoBuilder BuildFromTraceInfo(TraceInfo tranceInfo)
        {
            if (tranceInfo == null)
            {
                throw new ArgumentException($"tranceInfo not null");
            }
            _traceInfo = tranceInfo.Clone();
            _traceInfo.Id = Guid.NewGuid().ToString();
            _traceInfo.ThreadId = Thread.CurrentThread.ManagedThreadId;
            _traceInfo.ThreadName = Thread.CurrentThread.Name;
            _traceInfo.CreateAt = DateTimeOffset.Now;
            return this;
        }

        public TraceInfoBuilder ClearTraceInfo()
        {
            _traceInfo = null;
            return this;
        }


        public TraceInfoBuilder ChangeId(string Id)
        {
            _traceInfo.Id = Id;
            return this;
        }

        public TraceInfoBuilder HttpContext(HttpContext context)
        {
            context.Request.EnableBuffering();
            HttpRequest(context.Request);
            HttpResponse(context.Response);
            _traceInfo.HostIPAddress = context.Request.Host.Value;
            _traceInfo.ClientIpAddress = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
            return this;
        }


        public TraceInfoBuilder HttpRequest(HttpRequest request)
        {
            if (_traceInfo.Request == null)
            {
                _traceInfo.Request = new TraceInfoRequest();
            }
            _traceInfo.Request.Body = request?.ToStr().Result;
            _traceInfo.Request.Header = request?.Headers?.ToJson();
            _traceInfo.Request.Cookies = request?.Cookies?.ToJson();
            _traceInfo.Request.Method = request?.Method;
            _traceInfo.Request.Url = request.Path;

            _traceInfo.HostIPAddress = request.Host.Value;
            _traceInfo.ClientIpAddress = Util.GetServerIps();
            return this;
        }


        public TraceInfoBuilder HttpResponse(HttpResponse response)
        {
            if (_traceInfo.Response == null)
            {
                _traceInfo.Response = new TraceInfoResponse();
            }
            _traceInfo.Response.Body = response.ToStr();
            _traceInfo.Response.StatusCode = (int)response.StatusCode;
            return this;
        }

        public TraceInfoBuilder HttpRequestMessage(HttpRequestMessage requestMessage)
        {
            if (_traceInfo.Request == null)
            {
                _traceInfo.Request = new TraceInfoRequest();
            }
            _traceInfo.Request.Body = requestMessage?.Content?.ReadAsStringAsync().Result;
            _traceInfo.Request.Header = requestMessage?.Headers?.ToJson();
            _traceInfo.Request.Cookies = requestMessage?.Content?.ToJson();
            _traceInfo.Request.Method = requestMessage?.Method?.Method;
            _traceInfo.Request.Url = requestMessage.RequestUri.AbsoluteUri;

            _traceInfo.HostIPAddress = requestMessage.RequestUri.Host;
            _traceInfo.ClientIpAddress = Util.GetServerIps();
            return this;
        }

        public TraceInfoBuilder HttpResponseMessage(HttpResponseMessage responseMessage)
        {
            if (_traceInfo.Response == null)
            {
                _traceInfo.Response = new TraceInfoResponse();
            }
            _traceInfo.Response.Body = responseMessage.Content.ReadAsStringAsync().Result;
            _traceInfo.Response.StatusCode = (int)responseMessage.StatusCode;
            return this;
        }
        public TraceInfoBuilder Request(string request)
        {
            if (_traceInfo.Request == null)
            {
                _traceInfo.Request = new TraceInfoRequest();
            }
            _traceInfo.Request.Body = request;
            return this;
        }
        public TraceInfoBuilder Response(string response)
        {
            if (_traceInfo.Response == null)
            {
                _traceInfo.Response = new TraceInfoResponse();
            }
            _traceInfo.Response.Body = response;
            return this;
        }

        public TraceInfoBuilder ParentId(string parentId)
        {
            _traceInfo.ParentId = parentId;
            return this;
        }
        public TraceInfoBuilder Log(string logLevel, string logName, Exception exception = null)
        {
            _traceInfo.LogName = logName;
            _traceInfo.LogLevel = logLevel;
            _traceInfo.Exception = exception;
            return this;
        }


        public TraceInfoBuilder Exception(Exception exception)
        {
            _traceInfo.Exception = exception;
            return this;
        }

        public TraceInfoBuilder TrackId(string trackId, string parentTrackId = null)
        {
            _traceInfo.TrackId = trackId;
            _traceInfo.ParentTrackId = parentTrackId;
            return this;
        }

        public TraceInfoBuilder ParentTrackId(string parentTrackId)
        {
            _traceInfo.ParentTrackId = parentTrackId;
            return this;
        }

        public TraceInfoBuilder ElapsedTime(long elapsedTime)
        {
            _traceInfo.ElapsedTime = elapsedTime;
            return this;
        }
        public TraceInfoBuilder StatusCode(int? statusCode)
        {
            if (_traceInfo.Response == null)
            {
                _traceInfo.Response = new TraceInfoResponse();
            }
            _traceInfo.Response.StatusCode = statusCode;
            return this;
        }

        public TraceInfoBuilder Description(string description)
        {
            _traceInfo.Description = description;
            return this;
        }
        public TraceInfo Build()
        {
            return _traceInfo;
        }



    }
}
