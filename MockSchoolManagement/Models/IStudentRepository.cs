using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.Models
{
    /// <summary>
    /// 学生信息相关接口
    /// </summary>
    public interface IStudentRepository
    {
        /// <summary>
        /// 通过Id查询出学生Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student GetStudent(int id);

        /// <summary>
        /// 查询出学生信息集合
        /// </summary>
        /// <returns>返回学生信息列表：IEnumerable[Student]类型</returns>
        IEnumerable<Student> GetAllStudents();

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="student">参数类型：Student（添加学生信息）</param>
        /// <returns>返回添加的学生信息 Student</returns>
        Student AddStudent(Student student);

        /// <summary>
        /// 修改学生
        /// </summary>
        /// <param name="updateStudent">参数类型：Student</param>
        /// <returns>返回学生信息类型 Student</returns>
        Student Update(Student updateStudent);

        /// <summary>
        /// 删除一名学生信息
        /// </summary>
        /// <param name="id">参数类型：Int</param>
        /// <returns>返回学生信息类型：Student</returns>
        Student Delete(int id);

    }
}
