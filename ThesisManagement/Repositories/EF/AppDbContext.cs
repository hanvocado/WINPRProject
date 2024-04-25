using Microsoft.EntityFrameworkCore;
using ThesisManagement.Models;
using Task = ThesisManagement.Models.Task;

namespace ThesisManagement.Repositories.EF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Professor> Admins { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Thesis> Theses { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskProgress> TaskProgresses { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<ScheduleInfo> ScheduleInfos { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>().HasData(
                new Professor
                {
                    Id = "A1",
                    Name = "Nguyen A",
                    Email = "ad1@gmail.com",
                    Password = "12345"
                }
            );

            modelBuilder.Entity<Professor>().HasData(
                new Professor
                {
                    Id = "P1",
                    Name = "Trần Văn Anh",
                    Email = "anh@hcmute.edu.vn",
                    Password = "anh12345",
                    Phone = "123-456-7890",
                    Birthday = new DateTime(1980, 1, 1)
                },
                new Professor
                {
                    Id = "P2",
                    Name = "Lê Nguyên",
                    Email = "lenguyen@gmail.com",
                    Password = "nguyen12345",
                    Phone = "987-654-3210",
                    Birthday = new DateTime(1975, 5, 15)
                },
                new Professor
                {
                    Id = "P3",
                    Name = "Đặng Lâm",
                    Email = "lam@hcmute.edu.vn",
                    Password = "lam12345",
                    Phone = "987-654-3210",
                    Birthday = new DateTime(1975, 5, 15)
                }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = "22133010",
                    Name = "Nguyễn Văn Yên",
                    Email = "a@student.com",
                    Password = "a12345",
                    Phone = "931-456-7890",
                    Birthday = new DateTime(2000, 1, 3)
                },
                new Student
                {
                    Id = "22133011",
                    Name = "Lâm Khang",
                    Email = "b@student.com",
                    Password = "b12345",
                    Phone = "977-654-3210",
                    Birthday = new DateTime(2001, 5, 22)
                },
                new Student
                {
                    Id = "22110001",
                    Name = "Võ Thị Thu Huyền",
                    Email = "c@student.com",
                    Password = "c12345",
                    Phone = "987-654-3910",
                    Birthday = new DateTime(2001, 5, 22)
                },
                new Student
                {
                    Id = "22110010",
                    Name = "Nguyễn Bình Minh",
                    Email = "d@student.com",
                    Password = "d12345",
                    Phone = "987-654-3410",
                    Birthday = new DateTime(2001, 5, 22)
                },
                new Student
                {
                    Id = "22133015",
                    Name = "Trần Quốc Khánh",
                    Email = "e@student.com",
                    Password = "e12345",
                    Phone = "987-655-3210",
                    Birthday = new DateTime(2001, 5, 22)
                },
                new Student
                {
                    Id = "22133016",
                    Name = "Trần Văn Nam",
                    Email = "f@student.com",
                    Password = "f12346",
                    Phone = "987-654-3215",
                    Birthday = new DateTime(2001, 5, 22)
                },
                new Student
                {
                    Id = "22133017",
                    Name = "Nguyễn Ngọc Nữ",
                    Email = "g@student.com",
                    Password = "g12346",
                    Phone = "987-654-3220",
                    Birthday = new DateTime(2001, 5, 22)
                }
            );

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.HasOne(t => t.Professor)
                        .WithMany(p => p.Topics)
                        .HasForeignKey(t => t.ProfessorId);
            });

            modelBuilder.Entity<Topic>().HasData(
                new Topic
                {
                    Id = 1,
                    ProfessorId = "P1",
                    Name = "Quản lý ngân hàng",
                    Description = "Introductory course on database design",
                    Category = "Computer Science",
                    Technology = "SQL",
                    Requirement = "Đúng deadline",
                    Function = "Access and query data",
                    StudentQuantity = 2
                },
                new Topic
                {
                    Id = 2,
                    ProfessorId = "P1",
                    Name = "Quản lý công ty",
                    Description = "Xây dựng website Quản lý công ty quy mô vừa và nhỏ",
                    Category = "Web Development",
                    Technology = "ASP.NET Core",
                    Requirement = "Đúng deadline, teamwork",
                    Function = "Trả lương nhân viên. Giao Tasks theo các cấp.",
                    StudentQuantity = 3
                },
                new Topic
                {
                    Id = 3,
                    ProfessorId = "P2",
                    Name = "Machine Learning",
                    Description = "Exploring algorithms for predictive modeling",
                    Category = "Data Science",
                    Technology = "Python",
                    Requirement = "Đúng tiến độ",
                    Function = "Train model for project",
                    StudentQuantity = 3
                },
                new Topic
                {
                    Id = 4,
                    ProfessorId = "P2",
                    Name = "Khảo sát và phân tích chất lượng thư viện trường HCMUTE",
                    Description = "Description xyz",
                    Category = "Data Science",
                    Technology = "Python",
                    Requirement = "Requirement something here",
                    Function = "",
                    StudentQuantity = 2
                }
            );

            modelBuilder.Entity<Thesis>(entity =>
            {
                entity.HasOne(tp => tp.Topic)
                        .WithMany(th => th.Theses)
                        .HasForeignKey(th => th.TopicId)
                        .OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<ScheduleInfo>(entity =>
            {
                entity.HasOne(th => th.Thesis)
                        .WithMany(sch => sch.ScheduleInfos)
                        .HasForeignKey(m => m.ThesisId)
                        .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(th => th.Task)
                        .WithOne(t => t.Schedule)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasOne(tp => tp.Thesis)
                      .WithMany(t => t.Tasks)
                      .HasForeignKey(t => t.ThesisId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasOne(fb => fb.Thesis)
                      .WithMany(ts => ts.Feedbacks)
                      .HasForeignKey(fb => fb.ThesisId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TaskProgress>(entity =>
            {
                entity.HasOne(t => t.Task)
                      .WithMany(tp => tp.TaskProgresses)
                      .HasForeignKey(tp => tp.TaskId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(st => st.Student)
                      .WithMany(tp => tp.TaskProgresses)
                      .HasForeignKey(tp => tp.StudentId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasOne(tp => tp.TaskProgress)
                      .WithMany(at => at.Attachments)
                      .HasForeignKey(at => at.TaskProgressId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


        }
    }
}
