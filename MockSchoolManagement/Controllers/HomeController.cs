using Microsoft.AspNetCore.Mvc;
using MockSchoolManagement.Models;
using MockSchoolManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.Controllers
{
    //[Route("[controller]/[action]")]
    public class HomeController : Controller
    {

        private IStudentRepository _studentRepository { get; set; }

        /// <summary>
        /// 使用构造函数注入的方式注入IStudentRepository 
        /// </summary>
        /// <param name="studentRepository"></param>
        public HomeController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        //[Route("")]
        //[Route("~/")]
        //[Route("~/Home")]
        public IActionResult Index()
        {
            //返回学生名字
            var students = _studentRepository.GetAllStudents();
            return View(students);
        }

        ////[Route("Detail/{id?}")]
        //[Route("{id?}")]
        public IActionResult Detail(int? id)
        {


            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                //当Id为空是输入1 当有值时则为id
                Student = _studentRepository.GetStudent(id??1),
                PageTitle = "学生详细信息"
            };

            return View(homeDetailsViewModel);


        }
    }
}
