using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisManagement.Migrations
{
    public partial class AddAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Professors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Professors");
        }
    }
}
