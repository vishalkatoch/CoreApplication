using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApplication.Migrations
{
    public partial class SeedMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmpId", "Deparment", "Email", "Name" },
                values: new object[] { 691115, 2, "Ajax@gr.com", "Ajax" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmpId",
                keyValue: 691115);
        }
    }
}
