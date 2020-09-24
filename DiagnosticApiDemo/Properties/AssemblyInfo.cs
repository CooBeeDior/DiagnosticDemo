using DiagnosticCore;
using ElasticSearchCore;
using Microsoft.AspNetCore.Hosting;
using MongodbCore;
using PersistenceAbstraction;

[assembly: HostingStartupAttribute(typeof(DiagnosticServiceStartup))]
[assembly: HostingStartupAttribute(typeof(ElasticSearchServiceStartup))]
[assembly: HostingStartupAttribute(typeof(MongodbServiceStartup))]
[assembly: HostingStartupAttribute(typeof(PersistenceStartup))]

