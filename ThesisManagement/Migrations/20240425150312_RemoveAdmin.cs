using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisManagement.Migrations
{
    public partial class RemoveAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Professor_ProfessorId",
                table: "Topics");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professor",
                table: "Professor");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "ScheduleInfos");

            migrationBuilder.RenameTable(
                name: "Professor",
                newName: "Professors");

            migrationBuilder.RenameIndex(
                name: "IX_Professor_Email",
                table: "Professors",
                newName: "IX_Professors_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professors",
                table: "Professors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Professors_ProfessorId",
                table: "Topics",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Professors_ProfessorId",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professors",
                table: "Professors");

            migrationBuilder.RenameTable(
                name: "Professors",
                newName: "Professor");

            migrationBuilder.RenameIndex(
                name: "IX_Professors_Email",
                table: "Professor",
                newName: "IX_Professor_Email");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "ScheduleInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professor",
                table: "Professor",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Birthday", "Email", "Name", "Password", "Phone" },
                values: new object[] { "A1", null, "ad1@gmail.com", "Nguyen A", "12345", null });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_Email",
                table: "Admin",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Professor_ProfessorId",
                table: "Topics",
                column: "ProfessorId",
                principalTable: "Professor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
