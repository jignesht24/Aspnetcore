using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TestApp
{
    public class Program
    {
        #region "Application hosted always as Windows service"
        //public static void Main(string[] args)
        //{
        //    var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
        //    var pathToContentRoot = Path.GetDirectoryName(pathToExe);

        //    var host = WebHost.CreateDefaultBuilder(args)
        //        .UseContentRoot(pathToContentRoot)
        //        .UseStartup<Startup>()
        //        .Build();

        //    host.RunAsService();
        //}

        #endregion

        #region "Provide a way to host application outside of a service"
        public static void Main(string[] args)
        {
            var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            var pathToContentRoot = Path.GetDirectoryName(pathToExe);

            IWebHost host;

            if (Debugger.IsAttached || args.Contains("console"))
            {
                host = WebHost.CreateDefaultBuilder()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseStartup<Startup>()
                        .Build();
                host.Run();
            }
            else
            {
                host = WebHost.CreateDefaultBuilder(args)
                        .UseContentRoot(pathToContentRoot)
                        .UseStartup<Startup>()
                        .Build();
                //host.RunAsService();
                host.RunAsCustomService();
            }
        }

        #endregion

        #region "Handle starting and stopping events of Windows service"

        //public static void Main(string[] args)
        //{
        //    var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
        //    var pathToContentRoot = Path.GetDirectoryName(pathToExe);

        //    var host = WebHost.CreateDefaultBuilder(args)
        //        .UseContentRoot(pathToContentRoot)
        //        .UseStartup<Startup>()
        //        .Build();

        //    host.RunAsCustomService();
        //}

        #endregion
    }
}
