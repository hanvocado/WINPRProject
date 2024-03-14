﻿using Microsoft.EntityFrameworkCore;
using ThesisManagement.Models;
using Task = ThesisManagement.Models.Task;

namespace ThesisManagement.Repositories.EF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Thesis> Theses { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Professor>().HasData(
                new Professor
                {
                    Id = "P1",
                    Name = "John Doe",
                    Email = "john@example.com",
                    Password = "hashed_password",
                    Phone = "123-456-7890",
                    Birthday = new DateTime(1980, 1, 1)
                },
                new Professor
                {
                    Id = "P2",
                    Name = "Jane Smith",
                    Email = "jane@example.com",
                    Password = "hashed_password2",
                    Phone = "987-654-3210",
                    Birthday = new DateTime(1975, 5, 15)
                }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = "S1",
                    Name = "Boe Scott",
                    Email = "scott@example.com",
                    Password = "hashed_password3",
                    Phone = "123-456-7890",
                    Birthday = new DateTime(2000, 1, 3)
                },
                new Student
                {
                    Id = "S2",
                    Name = "Arian Smith",
                    Email = "smith@example.com",
                    Password = "hashed_password4",
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
                    Name = "Database Design",
                    Description = "Introductory course on database design",
                    Category = "Computer Science",
                    Technology = "SQL",
                    Requirement = "",
                    StudentQuantity = 5
                },
                new Topic
                {
                    Id = 2,
                    ProfessorId = "P1",
                    Name = "Web Development",
                    Description = "Building dynamic websites using ASP.NET Core",
                    Category = "Web Development",
                    Technology = "ASP.NET Core",
                    Requirement = "",
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
                    Requirement = "",
                    StudentQuantity = 5
                }
            );

            modelBuilder.Entity<Thesis>(entity =>
            {
                entity.HasOne(tp => tp.Topic)
                        .WithMany(th => th.Theses)
                        .HasForeignKey(th => th.TopicId);

            });

            modelBuilder.Entity<Thesis>().HasData(
                new Thesis
                {
                    Id = 1,
                    TopicId = 2,
                    TopicStatus = "Approved",
                    File = null,
                    Score = 8
                },
                  new Thesis
                  {
                      Id = 2,
                      TopicId = 1,
                      TopicStatus = "Waiting",
                      File = null,
                      Score = 9
                  },
                    new Thesis
                    {
                        Id = 3,
                        TopicId = 3,
                        TopicStatus = "Rejected",
                        File = null,
                        Score = 10
                    }
            );

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasOne(fb => fb.Thesis)
                      .WithMany(ts => ts.Feedbacks)
                      .HasForeignKey(fb => fb.ThesisId);
            });

        }
    }
}
