﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.Models
{
    /// <summary>
    /// 实现老师类
    /// </summary>
    public class MockStudentRepository : IStudentRepository
    {
        private List<Student> _studentList;
        public MockStudentRepository()
        {
            _studentList = new List<Student>()
            {
                new Student() {Id = 1,Name = "张三",Major = "计算机科学",Email ="zhangsan@52abp.com" },
                new Student() {Id = 2,Name = "李四",Major = "物流",Email ="lisi@52abp.com" },
                new Student() {Id = 3,Name = "赵六",Major = "电子商务",Email ="zhaoliu@52abp.com" }


            };

        }

        /// <summary>
        /// 查询出所有学生信息
        /// </summary>
        /// <returns>返回所有学生类型：IEnumerable[Student]</returns>
        public IEnumerable<Student> GetAllStudents()
        {
            var query = _studentList.ToList();

            return query;
        }

        /// <summary>
        /// 查询单个学生信息
        /// </summary>
        /// <param name="id">IInt类型 学生Id</param>
        /// <returns>返回学生信息：Student类型</returns>
        public Student GetStudent(int id)
        {
          var student=  _studentList.FirstOrDefault(p => p.Id == id);

            return student;
        
        }
    }
}
