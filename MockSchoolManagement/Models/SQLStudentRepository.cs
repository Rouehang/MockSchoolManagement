using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.Models
{
    public class SQLStudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        public SQLStudentRepository(AppDbContext context)
        {
            this._context = context;
                
        }

      
        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="student">参数类型：Student（添加学生信息）</param>
        /// <returns>返回添加的学生信息 Student</returns>
        public Student AddStudent(Student student)
        {
            
            _context.Add(student);
            _context.SaveChanges();

            return student;
        }

        /// <summary>
        /// 删除一名学生信息
        /// </summary>
        /// <param name="id">参数类型：Int</param>
        /// <returns>返回学生信息类型：Student</returns>
        public Student Delete(int id)
        {
            Student student = _context.students.Where(s => s.Id == id).FirstOrDefault();
            if (student != null)
            {
                _context.Remove(student);
                _context.SaveChanges();
            }
            return student;
        }

        /// <summary>
        /// 查询出所有学生信息
        /// </summary>
        /// <returns>返回所有学生类型：IEnumerable[Student]</returns>
        public IEnumerable<Student> GetAllStudents()
        {
            var query = _context.students.ToList();

            return query;
        }

        /// <summary>
        /// 查询单个学生信息
        /// </summary>
        /// <param name="id">IInt类型 学生Id</param>
        /// <returns>返回学生信息：Student类型</returns>
        public Student GetStudentById(int id)
        {
            var student = _context.students.FirstOrDefault(p => p.Id == id);

            return student;

        }


        /// <summary>
        /// 修改学生
        /// </summary>
        /// <param name="updateStudent">参数类型：Student</param>
        /// <returns>返回学生信息类型 Student</returns>
        public Student Update(Student updateStudent)
        {
            //Student student = _context.students.Where(p => p.Id == updateStudent.Id).FirstOrDefault();

            //if (student != null)
            //{
            //    student.Name = updateStudent.Name;
            //    student.Email = updateStudent.Email;
            //    student.ClassName = updateStudent.ClassName;
            //}
            var student = _context.students.Attach(updateStudent);
            student.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();
            return updateStudent;
        }
    }
}
