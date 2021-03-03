using Microsoft.EntityFrameworkCore;
using MockSchoolManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchoolManagement.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// 学生类
        /// </summary>
        public DbSet<Student> students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 添加单个种子数据
            modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = 1,
                Name = "李狗蛋",
                ClassName = ClassNameEnum.FirstGrade,
                Email = "ltm@ddxc.org",
                Major = "数学"

            });

            modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = 2,
                Name = "李四",
                ClassName = ClassNameEnum.FirstGrade,
                Major = "数学",
                Email = "lisi@52abp.com",
            });

            modelBuilder.Entity<Student>().HasData(
          new Student
          {
              Id = 3,
              Name = "王五",
              ClassName = ClassNameEnum.FirstGrade,
              Major = "英语",
              Email = "lizz@52abp.com",
          });
            #endregion

            modelBuilder.Seed();


        }






    }
}
