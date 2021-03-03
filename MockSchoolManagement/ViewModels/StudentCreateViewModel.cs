using Microsoft.AspNetCore.Http;
using MockSchoolManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.ViewModels
{
    /// <summary>
    /// 添加学生所用的视图模型
    /// </summary>
    public class StudentCreateViewModel
    {
        [Required(ErrorMessage = "请输入名字"),MaxLength(50, ErrorMessage = "名字的长度不能超过50个字符")]
        [Display(Name = "名字")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "主修科目")]
        public string Major { get; set; }

        [Display(Name = "电子邮箱")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "邮箱的格式不正确")]
        [Required(ErrorMessage = "请输入邮箱地址")]
        public string Email { get; set; }

        [Display(Name = "头像")]
        public IFormFile Photo { get; set; }

        [Display(Name = "年级名")]
        public ClassNameEnum ClassName { get; set; }

    }
}
