using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisManagement.Migrations
{
    public partial class UpdateCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "22133016",
                column: "Password",
                value: "f12345");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "22133017",
                column: "Password",
                value: "g12345");

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Avatar", "Birthday", "Email", "Name", "Password", "Phone", "Score", "ThesisId", "WorkingTime" },
                values: new object[] { "22133020", null, new DateTime(2001, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "h@student.com", "Nguyễn Văn Hoàng", "h12345", "987-654-3220", null, null, 0f });

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 1,
                column: "Category",
                value: "Desktop App");

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 2,
                column: "Category",
                value: "Web App");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "22133020");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "22133016",
                column: "Password",
                value: "f12346");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "22133017",
                column: "Password",
                value: "g12346");

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 1,
                column: "Category",
                value: "Computer Science");

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 2,
                column: "Category",
                value: "Web Development");
        }
    }
}
