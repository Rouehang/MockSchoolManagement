using MockSchoolManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.ViewModels
{
    /// <summary>
    /// 视图模型（DTO）
    /// </summary>
    public class HomeDetailsViewModel
    {
        /// <summary>
        /// 老师信息
        /// </summary>
        public Student Student { get; set; }

        /// <summary>
        /// 页面名称（标题）
        /// </summary>
        public string PageTitle { get; set; }
    }
}
