using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        private ILogger<ErrorController> logger { get; set; }


        ///<summary>
        /// 注入ASP.NET Core ILogger服务
        /// 将控制器类型指定为泛型参数
        /// 这有助于我们确定哪个类或控制器产生了异常，然后记录它
        ///</summary>
        ///<param name="logger"> </param>
        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        //如果状态码为404，则路径将变为Error/404
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "抱歉，读者访问的页面不存在";

                    logger.LogWarning($"发送了一个404错误，路径：{statusCodeResult.OriginalPath}以及查询字符串={statusCodeResult.OriginalQueryString}");
                    //ViewBag.Path = statusCodeResult.OriginalPath;
                    //ViewBag.QS = statusCodeResult.OriginalQueryString;
                    break;
            }
            return View("NotFound");
        }

        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            logger.LogError($"路径{exceptionHandlerPathFeature.Path},产生了一个错误{exceptionHandlerPathFeature.Error}");
            //ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            //ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            //ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;
            return View("Error");
        }
    }
}
