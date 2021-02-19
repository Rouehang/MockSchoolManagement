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
        public Student Student { get; set; }

        public string PageTitle { get; set; }
    }
}
