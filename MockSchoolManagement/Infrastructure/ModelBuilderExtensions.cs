using Microsoft.EntityFrameworkCore;
using MockSchoolManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.Infrastructure
{
    /// <summary>
    /// 种子文件
    /// </summary>
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = 4,
                Name = "玉玉",
                ClassName = ClassNameEnum.FirstGrade,
                Major = "数学",
                Email = "lisi@52abp.com",
            }
            );
            modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = 5,
                Name = "牛柳",
                ClassName = ClassNameEnum.FirstGrade,
                Major = "英语",
                Email = "lisi@52abp.com",
            }
            );
        }
    }
}
