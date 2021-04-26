using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApplication.Migrations
{
    public partial class Extend_IdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmpId", "Deparment", "Email", "Name", "PhotoUrl" },
                values: new object[] { 319805, 2, "Ajax@gr.com", "Ajax", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmpId",
                keyValue: 319805);

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");
        }
    }
}
