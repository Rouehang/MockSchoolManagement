using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.Models
{
    public interface IStudentRepository
    {
        /// <summary>
        /// 通过Id查询出学生Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student GetStudent(int id);

        IEnumerable<Student> GetAllStudents();
    }
}
