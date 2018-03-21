using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace SerilogDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateMSSqlLoggerUsingJSONFile();
            //CreateMSSqlLogger();

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();

        public static void CreateMSSqlLogger()
        {
            var connectionString = @"Data Source=(local); Initial Catalog=Test;User ID=sa;Password=Passwd@12;";
            var tableName = "Logs";

            var columnOption = new ColumnOptions();
            columnOption.Store.Remove(StandardColumn.MessageTemplate);

            columnOption.AdditionalDataColumns = new Collection<DataColumn>
                            {
                                new DataColumn {DataType = typeof (string), ColumnName = "OtherData"},
                            };

            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Information()
                            .MinimumLevel.Override("SerilogDemo", LogEventLevel.Information)
                            .WriteTo.MSSqlServer(connectionString, tableName, 
                                    columnOptions: columnOption

                                    )
                            .CreateLogger();
        }

        private static void CreateMSSqlLoggerUsingJSONFile()
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
