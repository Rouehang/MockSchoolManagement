using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement
{

    public class Program
    {
        /// <summary>
        /// 方法入口
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
         WebHost.CreateDefaultBuilder(args)
         .ConfigureLogging((hostingContext, logging) =>
         {
             logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
             logging.AddConsole();
             logging.AddDebug();
             logging.AddEventSourceLogger();
             //启用NLog作为日志提供程序之一
             logging.SetMinimumLevel(LogLevel.Trace);
             //logging.AddNlog();

         }).UseStartup<Startup>();

    }
}

