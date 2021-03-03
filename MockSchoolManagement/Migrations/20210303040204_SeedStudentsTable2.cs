using Microsoft.EntityFrameworkCore.Migrations;

namespace MockSchoolManagement.Migrations
{
    public partial class SeedStudentsTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "ClassName", "Email", "Major", "Name" },
                values: new object[] { 2, 1, "lisi@52abp.com", "数学", "李四" });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "ClassName", "Email", "Major", "Name" },
                values: new object[] { 3, 1, "lizz@52abp.com", "英语", "王五" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
