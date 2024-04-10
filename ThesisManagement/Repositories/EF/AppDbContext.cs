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
                    Name = "Nguyễn A",
                    Email = "a@student.com",
                    Password = "a12345",
                    Phone = "123-456-7890",
                    Birthday = new DateTime(2000, 1, 3)
                },
                new Student
                {
                    Id = "22133011",
                    Name = "Lâm B",
                    Email = "b@student.com",
                    Password = "b12345",
                    Phone = "987-654-3210",
                    Birthday = new DateTime(2001, 5, 22)
                },
                new Student
                {
                    Id = "22110001",
                    Name = "Võ C",
                    Email = "c@student.com",
                    Password = "c12345",
                    Phone = "987-654-3210",
                    Birthday = new DateTime(2001, 5, 22)
                },
                new Student
                {
                    Id = "22110010",
                    Name = "Nguyễn D",
                    Email = "d@student.com",
                    Password = "d12345",
                    Phone = "987-654-3210",
                    Birthday = new DateTime(2001, 5, 22)
                },
                new Student
                {
                    Id = "22133015",
                    Name = "Trần E",
                    Email = "e@student.com",
                    Password = "e12346",
                    Phone = "987-654-3210",
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
                        .HasForeignKey(th => th.TopicId);

            });

            modelBuilder.Entity<ScheduleInfo>(entity =>
            {
                entity.HasOne(th => th.Thesis)
                        .WithMany(sch => sch.ScheduleInfos)
                        .HasForeignKey(m => m.ThesisId);

            });

            //modelBuilder.Entity<Thesis>().HasData(
            //    new Thesis
            //    {
            //        Id = 1,
            //        TopicId = 2,
            //        TopicStatus = "Approved",
            //        File = null,
            //        Score = 8
            //    },
            //    new Thesis
            //    {
            //        Id = 2,
            //        TopicId = 1,
            //        TopicStatus = "Waiting",
            //        File = null,
            //        Score = 9
            //    },
            //    new Thesis
            //    {
            //        Id = 3,
            //        TopicId = 3,
            //        TopicStatus = "Rejected",
            //        File = null,
            //        Score = 10
            //    },
            //    new Thesis
            //    {
            //        Id = 4,
            //        TopicId = 2,
            //        TopicStatus = "Waiting",
            //        File = null,
            //        Score = 10
            //    },
            //    new Thesis
            //    {
            //        Id = 5,
            //        TopicId = 2,
            //        TopicStatus = "Waiting",
            //        File = null,
            //        Score = 10
            //    }
            //);

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasOne(tp => tp.Thesis)
                      .WithMany(t => t.Tasks)
                      .HasForeignKey(t => t.ThesisId);
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasOne(fb => fb.Thesis)
                      .WithMany(ts => ts.Feedbacks)
                      .HasForeignKey(fb => fb.ThesisId);
            });

            modelBuilder.Entity<TaskProgress>(entity =>
            {
                entity.HasOne(t => t.Task)
                      .WithMany(tp => tp.TaskProgresses)
                      .HasForeignKey(tp => tp.TaskId);

                entity.HasOne(st => st.Student)
                      .WithMany(tp => tp.TaskProgresses)
                      .HasForeignKey(tp => tp.StudentId);
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasOne(tp => tp.TaskProgress)
                      .WithMany(at => at.Attachments)
                      .HasForeignKey(at => at.TaskProgressId);
            });


        }
    }
}
