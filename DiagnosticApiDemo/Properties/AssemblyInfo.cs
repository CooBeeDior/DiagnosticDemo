using DiagnosticCore;
using Microsoft.AspNetCore.Hosting;
using MongodbCore;

[assembly: HostingStartupAttribute(typeof(MongodbServiceStartup))]
[assembly: HostingStartupAttribute(typeof(DiagnosticServiceStartup))]
 