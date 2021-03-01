using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.Models
{
    /// <summary>
    /// 班级的枚举
    /// </summary>
    public enum ClassNameEnum
    {
        /// <summary>
        /// 未分配
        /// </summary>
        [Display(Name = "未分配")]
        None = 0,

        /// <summary>
        /// 一年级
        /// </summary>
        [Display(Name = "一年级")]
        FirstGrade = 1,

        /// <summary>
        /// 二年级
        /// </summary>
        [Display(Name = "二年级")]
        SecondGrade = 2,

        /// <summary>
        /// 三年级
        /// </summary>
        [Display(Name = "三年级")]
        GradeThree = 3,
    }
}
