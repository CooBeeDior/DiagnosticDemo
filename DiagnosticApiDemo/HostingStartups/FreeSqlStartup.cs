using FreeSql;
using FreeSql.Internal;
using FreesqlAbstration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
namespace DiagnosticApiDemo.HostingStartups
{
    public class FreeSqlStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddFreeSql(options =>
                {
                    var db = new FreeSqlDb()
                    {
                        Name = "FreeSqlDb",
                        DataType = DataType.MySql,
                        ConnectString = "Server=coobeedior.com;Database=log;User=root;Password=root;",
                        IsAutoSyncStructure = true,
                        IsNoneCommandParameter = false,
                        IsLazyLoading = false,
                        NameConvertType = NameConvertType.PascalCaseToUnderscoreWithLower,
                        Executing = (command) => { },
                        Executed = (command, sql) => { }
                    };
                    options.FreeSqlDbs.Add(db);
                });
            });
        }
    }

}
