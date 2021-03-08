using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MockSchoolManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using MockSchoolManagement.CustomerMiddlewares;

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
            // DBConnection作为我们的连接字符串
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("MockStudentDBConnection")));

            services.AddMvc().AddXmlDataContractSerializerFormatters();

            //依赖注入
            //services.AddSingleton<IStudentRepository, MockStudentRepository>();
            //services.AddScoped<IStudentRepository, MockStudentRepository>();
            //services.AddTransient<IStudentRepository, MockStudentRepository>();
            services.AddScoped<IStudentRepository, SQLStudentRepository>();




            //配置密码默认设置
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            });

            //配置ASP.NET Core Identity服务
            //在 AddIdentity() 服务中使用
            //AddErrorDescriber()方法覆盖默认的错误提示内容
            services.AddIdentity<IdentityUser, IdentityRole>().AddErrorDescriber<CustomIdentityErrorDescriber>().AddEntityFrameworkStores<AppDbContext>();

            #region 另外一种配置密码的设置
            //   services.AddIdentity  <IdentityUser,IdentityRole > (options =>
            //   {
            //       options.Password.RequiredLength = 6;
            //       options.Password.RequiredUniqueChars = 3;
            //       options.Password.RequireNonAlphanumeric = false;
            //   })
            //.AddEntityFrameworkStores<AppDbContext>();
            #endregion




        }

        /// <summary>
        /// Configure()方法配置应用程序的请求处理管道。
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //如果环境是Development serve Developer Exception Page
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (env.IsStaging() || env.IsProduction() || env.IsEnvironment("UAT"))
            {
                //用于处理错误异常

                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");

            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            //添加验证中间件
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();


            //路由设置
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });



            #region 旧代码
            ////当是开发环境才会提示开发异常页面
            //if (env.IsDevelopment())
            //{
            //    DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions();
            //    developerExceptionPageOptions.SourceCodeLineCount = 10;
            //    //开发异常页面 当关闭后则不会弹出异常
            //    app.UseDeveloperExceptionPage(developerExceptionPageOptions);
            //}
            //#region 测试中间件
            ////app.Use(async (context, next) =>
            ////{
            ////    context.Response.ContentType = "text/plain;charset=utf-8";

            ////    logger.LogInformation("M1:传入请求");

            ////    await next();

            ////    logger.LogInformation("M1:传出请求");
            ////});

            ////app.Use(async (context, next) =>
            ////{


            ////    logger.LogInformation("M2:传入请求");

            ////    await next();

            ////    logger.LogInformation("M2:传出请求");
            ////});


            ////app.Run(async (context) =>
            ////{

            ////    ////防止乱码
            ////    //context.Response.ContentType = "text/plain;charset=utf-8";

            ////    #region 测试
            ////    ////获取当前进程名
            ////    //var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            ////    //confi

            ////    ////注入后通过_configuration访问MyKey
            ////    //var configuration = _configuration["MyKey"];
            ////    //await context.Response.WriteAsync();
            ////    #endregion
            ////    logger.LogInformation("M3:处理请求，生成响应");
            ////    await context.Response.WriteAsync("Hello World");
            ////});
            //#endregion


            //#region 设置默认文件中间件

            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();

            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("52abp.html");

            ////添加默认文件中间件  
            //app.UseDefaultFiles(defaultFilesOptions);

            ////index.html  index.htm 默认   default.html  default.htm

            ////静态文件中间介
            //app.UseStaticFiles();


            //#endregion

            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("52abp.html");

            //app.UseFileServer(fileServerOptions);


            //app.Run(async (context) =>
            //{

            //    //////防止乱码
            //    ////context.Response.ContentType = "text/plain;charset=utf-8";
            //    //throw new Exception("您的请求在管道中发生了一些错误，请检查");
            //    await context.Response.WriteAsync("Hosting Environment :"+env.EnvironmentName);  
            //});

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{

            //    endpoints.MapGet("/", async context =>
            //    {
            //        var processName = System.Diagnostics.Process.
            //        GetCurrentProcess().ProcessName;
            //        await context.Response.WriteAsync(processName);
            //    });
            //});
            #endregion
        }
    }
}
