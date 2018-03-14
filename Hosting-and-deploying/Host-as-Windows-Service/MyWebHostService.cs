using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ServiceProcess;

namespace TestApp
{
    public class MyWebHostService : WebHostService
    {
        private ILogger _logger;

        public MyWebHostService(IWebHost host) : base(host)
        {
            _logger = host.Services.GetRequiredService<ILogger<MyWebHostService>>();
        }
        //public MyWebHostService(IWebHost host) : base(host)
        //{
        //}

        protected override void OnStarting(string[] args)
        {
            System.Diagnostics.Debugger.Launch();
            base.OnStarting(args);
        }

        protected override void OnStarted()
        {
            base.OnStarted();
        }

        protected override void OnStopping()
        {
            base.OnStopping();
        }
    }

    public static class WebHostServiceExtensions
    {
        public static void RunAsCustomService(this IWebHost host)
        {
            var webHostService = new MyWebHostService(host);
            ServiceBase.Run(webHostService);
        }
    }
}
