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
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfessorId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Requirement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentQuantity = table.Column<int>(type: "int", nullable: false),
                    Technology = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeadLine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Theses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    TopicStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Score = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Theses_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThesisId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Theses_ThesisId",
                        column: x => x.ThesisId,
                        principalTable: "Theses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ThesisId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Theses_ThesisId",
                        column: x => x.ThesisId,
                        principalTable: "Theses",
                        principalColumn: "Id");
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
                columns: new[] { "Id", "Birthday", "Email", "Name", "Password", "Phone", "ThesisId" },
                values: new object[,]
                {
                    { "S1", new DateTime(2000, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "scott@example.com", "Boe Scott", "hashed_password3", "123-456-7890", null },
                    { "S2", new DateTime(2001, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "smith@example.com", "Arian Smith", "hashed_password4", "987-654-3210", null }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Category", "Description", "Name", "ProfessorId", "Requirement", "StudentId", "StudentQuantity", "Technology" },
                values: new object[] { 1, "Computer Science", "Introductory course on database design", "Database Design", "P1", "", null, 5, "SQL" });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Category", "Description", "Name", "ProfessorId", "Requirement", "StudentId", "StudentQuantity", "Technology" },
                values: new object[] { 2, "Web Development", "Building dynamic websites using ASP.NET Core", "Web Development", "P1", "", null, 3, "ASP.NET Core" });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Category", "Description", "Name", "ProfessorId", "Requirement", "StudentId", "StudentQuantity", "Technology" },
                values: new object[] { 3, "Data Science", "Exploring algorithms for predictive modeling", "Machine Learning", "P2", "", null, 5, "Python" });

            migrationBuilder.InsertData(
                table: "Theses",
                columns: new[] { "Id", "File", "Score", "TopicId", "TopicStatus" },
                values: new object[] { 1, null, 8f, 2, "Approved" });

            migrationBuilder.InsertData(
                table: "Theses",
                columns: new[] { "Id", "File", "Score", "TopicId", "TopicStatus" },
                values: new object[] { 2, null, 9f, 1, "Waiting" });

            migrationBuilder.InsertData(
                table: "Theses",
                columns: new[] { "Id", "File", "Score", "TopicId", "TopicStatus" },
                values: new object[] { 3, null, 10f, 3, "Rejected" });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ThesisId",
                table: "Feedbacks",
                column: "ThesisId");

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
                name: "IX_Students_ThesisId",
                table: "Students",
                column: "ThesisId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TopicId",
                table: "Tasks",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Theses_TopicId",
                table: "Theses",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_ProfessorId",
                table: "Topics",
                column: "ProfessorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Theses");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Professors");
        }
    }
}
