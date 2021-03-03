using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MockSchoolManagement.Models;
using MockSchoolManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.Controllers
{
    //[Route("[controller]/[action]")]
    public class HomeController : Controller
    {

        /// <summary>
        /// 学生相关信息
        /// </summary>
        private IStudentRepository _studentRepository { get; set; }

        /// <summary>
        /// 上传图片
        /// </summary>
        private IWebHostEnvironment _webHostEnvironment { get; set; }

        /// <summary>
        /// 使用构造函数注入的方式注入IStudentRepository 
        /// </summary>
        /// <param name="studentRepository"></param>
        public HomeController(IStudentRepository studentRepository,IWebHostEnvironment webHostEnvironment)
        {
            _studentRepository = studentRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// 主页 学生列表
        /// </summary>
        /// <returns></returns>
        //[Route("")]
        //[Route("~/")]
        //[Route("~/Home")]
        public IActionResult Index()
        {
            //返回学生名字
            var students = _studentRepository.GetAllStudents();
            return View(students);
        }

        /// <summary>
        /// 通过Id查询出单个学生列表
        /// </summary>
        /// <param name="id">参数类型：Int （学生Id）</param>
        /// <returns></returns>
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


        /// <summary>
        /// 展示添加学生的界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="student">参数类型：Student</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(StudentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    //拿到Images的路径
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    //为了确保文件名是唯一的，我们在文件名后附加一个新的GUID值和一个下划线
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder,uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath,FileMode.Create));
                }
                Student newStudent = new Student
                {
                    Name = model.Name,
                    Email = model.Email,
                    Major = model.Major,
                    ClassName = model.ClassName,
                    PhotoPath = uniqueFileName,
                };
                _studentRepository.AddStudent(newStudent);
                return RedirectToAction("Detail", new { id = newStudent.Id });

            }
            return View();
        
        }
    }
}
