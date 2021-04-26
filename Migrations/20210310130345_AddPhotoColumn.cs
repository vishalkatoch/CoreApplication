using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApplication.Migrations
{
    public partial class AddPhotoColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmpId",
                keyValue: 691115);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmpId", "Deparment", "Email", "Name", "PhotoUrl" },
                values: new object[] { 124465, 2, "Ajax@gr.com", "Ajax", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmpId",
                keyValue: 124465);

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Employees");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmpId", "Deparment", "Email", "Name" },
                values: new object[] { 691115, 2, "Ajax@gr.com", "Ajax" });
        }
    }
}
