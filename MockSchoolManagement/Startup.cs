using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement
{
    public class Startup
    {

        //依赖注入  
        private IConfiguration _configuration { get; set; }

        //注入服务
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// ConfigureServices()方法配置应用程序所需的服务。
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
        }

        /// <summary>
        /// Configure()方法配置应用程序的请求处理管道。
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //当是开发环境才会提示开发异常页面
            if (env.IsDevelopment())
            {
                //开发异常页面
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                //防止乱码
                context.Response.ContentType = "text/plain;charset=utf-8";

                ////获取当前进程名
                //var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                //confi

                //注入后通过_configuration访问MyKey
                var configuration = _configuration["MyKey"];
                //await context.Response.WriteAsync();
                await context.Response.WriteAsync(configuration);
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});

                endpoints.MapGet("/", async context =>
                {
                    var processName = System.Diagnostics.Process.
                    GetCurrentProcess().ProcessName;
                    await context.Response.WriteAsync(processName);
                });
            });
        }
    }
}
