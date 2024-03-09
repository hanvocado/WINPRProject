using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisManagement.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfessorId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    StudentId = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Technology = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Topics_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentTopics",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTopics", x => new { x.StudentId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_StudentTopics_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentTopics_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Professors",
                columns: new[] { "Id", "Birthday", "Email", "Name", "Password", "Phone" },
                values: new object[,]
                {
                    { "P1", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "john@example.com", "John Doe", "hashed_password", "123-456-7890" },
                    { "P2", new DateTime(1975, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane@example.com", "Jane Smith", "hashed_password2", "987-654-3210" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Birthday", "Email", "Name", "Password", "Phone" },
                values: new object[,]
                {
                    { "S1", new DateTime(2000, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "scott@example.com", "Boe Scott", "hashed_password3", "123-456-7890" },
                    { "S2", new DateTime(2001, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "smith@example.com", "Arian Smith", "hashed_password4", "987-654-3210" }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Category", "Description", "Name", "ProfessorId", "StudentId", "Technology" },
                values: new object[] { 1, "Computer Science", "Introductory course on database design", "Database Design", "P1", null, "SQL" });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Category", "Description", "Name", "ProfessorId", "StudentId", "Technology" },
                values: new object[] { 2, "Web Development", "Building dynamic websites using ASP.NET Core", "Web Development", "P1", null, "ASP.NET Core" });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Category", "Description", "Name", "ProfessorId", "StudentId", "Technology" },
                values: new object[] { 3, "Data Science", "Exploring algorithms for predictive modeling", "Machine Learning", "P2", null, "Python" });

            migrationBuilder.InsertData(
                table: "StudentTopics",
                columns: new[] { "StudentId", "TopicId", "Status" },
                values: new object[] { "S1", 1, "Approved" });

            migrationBuilder.InsertData(
                table: "StudentTopics",
                columns: new[] { "StudentId", "TopicId", "Status" },
                values: new object[] { "S1", 2, "Rejected" });

            migrationBuilder.InsertData(
                table: "StudentTopics",
                columns: new[] { "StudentId", "TopicId", "Status" },
                values: new object[] { "S1", 3, "Waiting" });

            migrationBuilder.CreateIndex(
                name: "IX_Professors_Email",
                table: "Professors",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentTopics_TopicId",
                table: "StudentTopics",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_ProfessorId",
                table: "Topics",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_StudentId",
                table: "Topics",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentTopics");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Professors");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
