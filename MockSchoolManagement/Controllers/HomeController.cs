using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        public HomeController(IStudentRepository studentRepository, IWebHostEnvironment webHostEnvironment)
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
            var student = _studentRepository.GetStudentById(id??1);
            //判断学生信息是否存在
            if (student == null)
            {
                Response.StatusCode = 404;
                return View("StudentNotFound", id);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                //当Id为空是输入1 当有值时则为id
                Student = _studentRepository.GetStudentById(id ?? 1),
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
                if (model.Photos != null && model.Photos.Count() > 0)
                {
                    foreach (IFormFile photo in model.Photos)
                    {
                        //必须将图片文件上传到wwwroot的images/avatars文件夹中而要获取wwwroot文件夹的
                        //路径，我们需要注入ASP.NET Core提供的WebHost Environment 服务通过
                        //WebHostEnvironment服务获取wwwroot文件夹的路径
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "avatars");
                        //为了确保文件名是唯一的，我们在文件名后附加一个新的GUID值和一个下划线                  
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        //使用IFormFile接口提供的CopyTo()方法将文件复制到wwwroot/images/avatars文件夹
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }

                }
                Student newStudent = new Student
                {
                    Name = model.Name,
                    Email = model.Email,
                    Major = model.Major,
                    ClassName = model.ClassName,
                    // 将文件名保存在Student对象的PhotoPath属性中
                    //它将被保存到数据库Students的表中
                    PhotoPath = uniqueFileName,
                };
                _studentRepository.AddStudent(newStudent);
                return RedirectToAction("Detail", new { id = newStudent.Id });

            }
            return View();

        }

        /// <summary>
        /// 编辑学生信息界面
        /// </summary>
        /// <param name="id">参数类型：int（学生Id）</param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Student student = _studentRepository.GetStudentById(id);
            StudentEditViewModel studentEditViewModel = new StudentEditViewModel()
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Major = student.Major,
                ExistingPhotoPath = student.PhotoPath
            };
            return View(studentEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {
            Student student = _studentRepository.GetStudentById(model.Id);
            //判断页面验证是否通过
            if (ModelState.IsValid)
            {

                student.ClassName = model.ClassName;
                student.Email = model.Email;
                student.Id = model.Id;
                student.Major = model.Major;
                student.Name = model.Name;



                if (!string.IsNullOrEmpty(model.ExistingPhotoPath))
                {
                    //当文件存在的情况把旧的删除
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "avatars", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);

                    }
                    //我们将新的图片文件保存到wwwroot/images/avatars文件夹中，并且会更新
                    //Student对象中的PhotoPath属性，最终都会将它们保存到数据库中
                    student.PhotoPath = ProcessUploadedFile(model);

                }
                Student updatedstudent = _studentRepository.Update(student);

                return RedirectToAction("index");

            };

            return View(model);

        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string ProcessUploadedFile(StudentEditViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photos.Count > 0)
            {
                foreach (var photo in model.Photos)
                {
                    //必须将图片文件上传到wwwroot的images/avatars文件夹中
                    //而要获取wwwroot文件夹的路径，我们需要注入ASP.NET Core提供的webHostEnvironment服务
                    //通过webHostEnvironment服务去获取wwwroot文件夹的路径
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "avatars");

                    //为了确保文件名是唯一的，我们在文件名后附加一个新的GUID值和一个下划线

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    //因为使用了非托管资源，所以需要手动进行释放
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        //使用IFormFile接口提供的CopyTo()方法将文件复制到
                        //wwwroot/images/avatars文件夹
                        photo.CopyTo(fileStream);
                    }

                }

            }
            return uniqueFileName;
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            _studentRepository.Delete(id);

            return RedirectToAction("Index");

        }




    }
}
