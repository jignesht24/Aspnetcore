using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace SerilogDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateFileLogger();
            CreateFileLoggerUsingJSONFile();

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();

        public static void CreateFileLogger()
        {
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Information()
                            .MinimumLevel.Override("SerilogDemo", LogEventLevel.Information)
                            .WriteTo.File("Logs/Example.txt",
                                    LogEventLevel.Information, // Minimum Log level
                                    rollingInterval: RollingInterval.Day, // This will append time period to the filename like Example20180316.txt
                                    retainedFileCountLimit: null,
                                    fileSizeLimitBytes: null,
                                    outputTemplate: "{Timestamp:dd-MMM-yyyy HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",  // Set custom file format
                                    shared: true // Shared between multi-process shared log files
                                    )
                            .CreateLogger();
        }

        private static void CreateFileLoggerUsingJSONFile()
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
