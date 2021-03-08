using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MockSchoolManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.Controllers
{
    /// <summary>
    /// 账户控制器
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// 管理基础数据存储中的用户所需的方法 如增删改查
        /// 具有CreateAsync()、 DeleteAsync()和UpdateAsync()等方法来创建、删除和更新用户
        /// </summary>
        private UserManager<IdentityUser> _userManager { get; set; }

        /// <summary>
        /// 用户登录所需的方法
        /// 具有SignInAsync()、 SignOutAsync()等方法来登录和注销用户
        /// </summary>
        private SignInManager<IdentityUser> _signInManager { get; set; }

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name=""></param>
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        /// <summary>
        /// 注册用户展示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="model">参数类型：RegisterViewModel</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //将数据从RegisterViewModel复制到IdentityUser
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                //将用户数据存储在AspNetUsers数据库表中
                var result = await _userManager.CreateAsync(user, model.Password);

                //如果成功创建用户，则使用登录服务登录用户信息
                //并重定向到HomeController的索引操作
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user,isPersistent:false );
                    return RedirectToAction("index", "home");
                }

                //如果有任何错误，则将它们添加到ModelState对象中
                //将由验证摘要标记助手显示到视图中
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }

            }
            return View(model);


        }

    }
}
