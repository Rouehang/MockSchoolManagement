using Microsoft.EntityFrameworkCore.Migrations;

namespace MockSchoolManagement.Migrations
{
    public partial class SeedStudentsTable4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "ClassName", "Email", "Major", "Name" },
                values: new object[] { 4, 1, "lisi@52abp.com", "数学", "玉玉" });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "ClassName", "Email", "Major", "Name" },
                values: new object[] { 5, 1, "lisi@52abp.com", "英语", "牛柳" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
