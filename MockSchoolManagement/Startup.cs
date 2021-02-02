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

        //����ע��  
        private IConfiguration _configuration { get; set; }

        //ע�����
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// ConfigureServices()��������Ӧ�ó�������ķ���
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
        }

        /// <summary>
        /// Configure()��������Ӧ�ó����������ܵ���
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //���ǿ��������Ż���ʾ�����쳣ҳ��
            if (env.IsDevelopment())
            {
                //�����쳣ҳ��
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                //��ֹ����
                context.Response.ContentType = "text/plain;charset=utf-8";

                ////��ȡ��ǰ������
                //var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                //confi

                //ע���ͨ��_configuration����MyKey
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
