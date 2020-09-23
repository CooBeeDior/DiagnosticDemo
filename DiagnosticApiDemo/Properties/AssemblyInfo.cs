using DiagnosticCore;
using ElasticSearchCore;
using Microsoft.AspNetCore.Hosting;
using MongodbCore;
[assembly: HostingStartupAttribute(typeof(ElasticSearchServiceStartup))]
//[assembly: HostingStartupAttribute(typeof(MongodbServiceStartup))]
[assembly: HostingStartupAttribute(typeof(DiagnosticServiceStartup))]
 