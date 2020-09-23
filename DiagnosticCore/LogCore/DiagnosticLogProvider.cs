using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongodbCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiagnosticCore.LogCore
{

    public class DiagnosticLogProvider : ILoggerProvider
    {
        private readonly IServiceProvider _serviceProvider;
        public DiagnosticLogProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new DiagnosticLogger(categoryName, _serviceProvider);
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }


}
