using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisManagement.Migrations
{
    public partial class UpdateTaskThesis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WaitingForResponse",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "WaitingForResponse",
                table: "Theses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasNewUpdate",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WaitingForResponse",
                table: "Theses");

            migrationBuilder.DropColumn(
                name: "HasNewUpdate",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "WaitingForResponse",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
