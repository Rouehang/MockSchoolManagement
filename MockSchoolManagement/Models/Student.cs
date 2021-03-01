using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.Models
{
    /// <summary>
    /// 学生模型信息
    /// </summary>
    public class Student
    {
        /// <summary>
        /// 学生Id
        /// </summary>
        [Display(Name = "编号")]
        public int Id { get; set; }

        /// <summary>
        /// 学生名字
        /// </summary>
        [Required(ErrorMessage = "请输入名字")]
        [Display(Name = "姓名"), MaxLength(50, ErrorMessage = "名字的长度不能超过50个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 主修科目
        /// </summary>
        [Display(Name = "主修科目")]
        [Required(ErrorMessage = "请输入科目")]
        public string Major { get; set; }

        /// <summary>
        /// 学生Emial
        /// </summary>
        [Required(ErrorMessage = "请输入邮箱")]
        [Display(Name = "邮箱号")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "邮箱格式不对")]
        public string Email { get; set; }

        /// <summary>
        /// 学生年级
        /// </summary>
        [Required(ErrorMessage = "请输入年纪")]
        [Display(Name = "学生年纪")]
        public ClassNameEnum ClassName { get; internal set; }
    }
}
