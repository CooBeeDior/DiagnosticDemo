using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiagnosticCore
{
    public class DiagnosticMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<DiagnosticMiddleware> _logger;
        private readonly DiagnosticOptions _options;

        public DiagnosticMiddleware(RequestDelegate next, ILogger<DiagnosticMiddleware> logger, DiagnosticOptions options)
        {
            _next = next;
            _logger = logger;
            _options = options; 
        }
        
        public async Task InvokeAsync(HttpContext context)
        {

            if (_options.RequestRule.Invoke(context.Request))
            {
                var activity = Activity.Current; 

            }
            await _next(context);
        }
    }
}
