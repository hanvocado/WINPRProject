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
                name: "Admin",
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
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
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
                    table.PrimaryKey("PK_Professor", x => x.Id);
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
                    Requirement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Function = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentQuantity = table.Column<int>(type: "int", nullable: false),
                    Technology = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_Professor_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professor",
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
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<float>(type: "real", nullable: true),
                    Evaluation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoUpdates = table.Column<int>(type: "int", nullable: true),
                    WaitingForResponse = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Theses_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id");
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
                name: "ScheduleInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThesisId = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleInfos_Theses_ThesisId",
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
                    Score = table.Column<float>(type: "real", nullable: true),
                    WorkingTime = table.Column<float>(type: "real", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThesisId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkingTime = table.Column<float>(type: "real", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    HasNewUpdate = table.Column<bool>(type: "bit", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_ScheduleInfos_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "ScheduleInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Theses_ThesisId",
                        column: x => x.ThesisId,
                        principalTable: "Theses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaskProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskProgresses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskProgresses_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskProgressId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_TaskProgresses_TaskProgressId",
                        column: x => x.TaskProgressId,
                        principalTable: "TaskProgresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Birthday", "Email", "Name", "Password", "Phone" },
                values: new object[] { "A1", null, "ad1@gmail.com", "Nguyen A", "12345", null });

            migrationBuilder.InsertData(
                table: "Professor",
                columns: new[] { "Id", "Birthday", "Email", "Name", "Password", "Phone" },
                values: new object[,]
                {
                    { "P1", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "anh@hcmute.edu.vn", "Trần Văn Anh", "anh12345", "123-456-7890" },
                    { "P2", new DateTime(1975, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "lenguyen@gmail.com", "Lê Nguyên", "nguyen12345", "987-654-3210" },
                    { "P3", new DateTime(1975, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "lam@hcmute.edu.vn", "Đặng Lâm", "lam12345", "987-654-3210" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Birthday", "Email", "Name", "Password", "Phone", "Score", "ThesisId", "WorkingTime" },
                values: new object[,]
                {
                    { "22110001", new DateTime(2001, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "c@student.com", "Võ Thị Thu Huyền", "c12345", "987-654-3910", null, null, 0f },
                    { "22110010", new DateTime(2001, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "d@student.com", "Nguyễn Bình Minh", "d12345", "987-654-3410", null, null, 0f },
                    { "22133010", new DateTime(2000, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "a@student.com", "Nguyễn Văn Yên", "a12345", "931-456-7890", null, null, 0f },
                    { "22133011", new DateTime(2001, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "b@student.com", "Lâm Khang", "b12345", "977-654-3210", null, null, 0f },
                    { "22133015", new DateTime(2001, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "e@student.com", "Trần Quốc Khánh", "e12345", "987-655-3210", null, null, 0f },
                    { "22133016", new DateTime(2001, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "f@student.com", "Trần Văn Nam", "f12346", "987-654-3215", null, null, 0f },
                    { "22133017", new DateTime(2001, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "g@student.com", "Nguyễn Ngọc Nữ", "g12346", "987-654-3220", null, null, 0f }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Category", "Description", "Function", "Name", "ProfessorId", "Requirement", "StudentId", "StudentQuantity", "Technology" },
                values: new object[,]
                {
                    { 1, "Computer Science", "Introductory course on database design", "Access and query data", "Quản lý ngân hàng", "P1", "Đúng deadline", null, 2, "SQL" },
                    { 2, "Web Development", "Xây dựng website Quản lý công ty quy mô vừa và nhỏ", "Trả lương nhân viên. Giao Tasks theo các cấp.", "Quản lý công ty", "P1", "Đúng deadline, teamwork", null, 3, "ASP.NET Core" },
                    { 3, "Data Science", "Exploring algorithms for predictive modeling", "Train model for project", "Machine Learning", "P2", "Đúng tiến độ", null, 3, "Python" },
                    { 4, "Data Science", "Description xyz", "", "Khảo sát và phân tích chất lượng thư viện trường HCMUTE", "P2", "Requirement something here", null, 2, "Python" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_Email",
                table: "Admin",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_TaskProgressId",
                table: "Attachments",
                column: "TaskProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ThesisId",
                table: "Feedbacks",
                column: "ThesisId");

            migrationBuilder.CreateIndex(
                name: "IX_Professor_Email",
                table: "Professor",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleInfos_ThesisId",
                table: "ScheduleInfos",
                column: "ThesisId");

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
                name: "IX_TaskProgresses_StudentId",
                table: "TaskProgresses",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskProgresses_TaskId",
                table: "TaskProgresses",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ScheduleId",
                table: "Tasks",
                column: "ScheduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ThesisId",
                table: "Tasks",
                column: "ThesisId");

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
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "TaskProgresses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "ScheduleInfos");

            migrationBuilder.DropTable(
                name: "Theses");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Professor");
        }
    }
}
