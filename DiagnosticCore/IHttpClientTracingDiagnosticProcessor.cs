using Microsoft.Extensions.DiagnosticAdapter;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace DiagnosticCore
{
    public interface IHttpClientTracingDiagnosticProcessor: ITracingDiagnosticProcessor
    {

        [DiagnosticName("System.Net.Http.Request")]
        void HttpRequest(HttpRequestMessage request);



        [DiagnosticName("System.Net.Http.Response")]
        void HttpResponse(HttpResponseMessage response);



        [DiagnosticName("System.Net.Http.Exception")]
        void HttpException(HttpRequestMessage request, Exception exception);
         

    }
}
