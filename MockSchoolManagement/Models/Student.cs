using System;
using System.Collections.Generic;
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
        public int Id { get; set; }

        /// <summary>
        /// 学生名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 主修科目
        /// </summary>
        public string Major { get; set; }

        /// <summary>
        /// 学生Emial
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 学生年纪
        /// </summary>
        public ClassNameEnum ClassName { get; internal set; }
    }
}
