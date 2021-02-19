using Microsoft.AspNetCore.Mvc;
using MockSchoolManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.Controllers
{
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
        public string Index()
        {
            //返回学生名字
            return _studentRepository.GetStudent(1).Name;
        }

        public ObjectResult Detail()
        {
            Student student = _studentRepository.GetStudent(1);

            return new ObjectResult(student);


        }
    }
}
