﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThesisManagement.Repositories.EF;

#nullable disable

namespace ThesisManagement.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ThesisManagement.Models.Admin", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Admin");

                    b.HasData(
                        new
                        {
                            Id = "A1",
                            Email = "ad1@gmail.com",
                            Name = "Nguyen A",
                            Password = "12345"
                        });
                });

            modelBuilder.Entity("ThesisManagement.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ThesisId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ThesisId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("ThesisManagement.Models.Professor", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Professor");

                    b.HasData(
                        new
                        {
                            Id = "P1",
                            Birthday = new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "john@example.com",
                            Name = "John Doe",
                            Password = "hashed_password",
                            Phone = "123-456-7890"
                        },
                        new
                        {
                            Id = "P2",
                            Birthday = new DateTime(1975, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jane@example.com",
                            Name = "Jane Smith",
                            Password = "hashed_password2",
                            Phone = "987-654-3210"
                        });
                });

            modelBuilder.Entity("ThesisManagement.Models.Student", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<int?>("ThesisId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ThesisId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = "S1",
                            Birthday = new DateTime(2000, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "scott@example.com",
                            Name = "Boe Scott",
                            Password = "hashed_password3",
                            Phone = "123-456-7890",
                            ThesisId = 2
                        },
                        new
                        {
                            Id = "S2",
                            Birthday = new DateTime(2001, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "smith@example.com",
                            Name = "Arian Smith",
                            Password = "hashed_password4",
                            Phone = "987-654-3210",
                            ThesisId = 4
                        },
                        new
                        {
                            Id = "S3",
                            Birthday = new DateTime(2001, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "charles@example.com",
                            Name = "Vincent Charles",
                            Password = "hashed_password4",
                            Phone = "987-654-3210",
                            ThesisId = 4
                        },
                        new
                        {
                            Id = "S4",
                            Birthday = new DateTime(2001, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "joyce@example.com",
                            Name = "Nora Joyce",
                            Password = "hashed_password4",
                            Phone = "987-654-3210",
                            ThesisId = 4
                        },
                        new
                        {
                            Id = "S5",
                            Birthday = new DateTime(2001, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "drake@example.com",
                            Name = "Noah Drake",
                            Password = "hashed_password4",
                            Phone = "987-654-3210",
                            ThesisId = 5
                        });
                });

            modelBuilder.Entity("ThesisManagement.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DeadLine")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ThesisManagement.Models.Thesis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("File")
                        .HasColumnType("varbinary(max)");

                    b.Property<float?>("Score")
                        .HasColumnType("real");

                    b.Property<int>("TopicId")
                        .HasColumnType("int");

                    b.Property<string>("TopicStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("Theses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Score = 8f,
                            TopicId = 2,
                            TopicStatus = "Approved"
                        },
                        new
                        {
                            Id = 2,
                            Score = 9f,
                            TopicId = 1,
                            TopicStatus = "Waiting"
                        },
                        new
                        {
                            Id = 3,
                            Score = 10f,
                            TopicId = 3,
                            TopicStatus = "Rejected"
                        },
                        new
                        {
                            Id = 4,
                            Score = 10f,
                            TopicId = 2,
                            TopicStatus = "Waiting"
                        },
                        new
                        {
                            Id = 5,
                            Score = 10f,
                            TopicId = 2,
                            TopicStatus = "Waiting"
                        });
                });

            modelBuilder.Entity("ThesisManagement.Models.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Function")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfessorId")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Requirement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentQuantity")
                        .HasColumnType("int");

                    b.Property<string>("Technology")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Topics");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Computer Science",
                            Description = "Introductory course on database design",
                            Function = "Access and query data",
                            Name = "Database Design",
                            ProfessorId = "P1",
                            Requirement = "",
                            StudentQuantity = 2,
                            Technology = "SQL"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Web Development",
                            Description = "Building dynamic websites using ASP.NET Core",
                            Function = "Add product to cart, pay order invoice",
                            Name = "Web Development",
                            ProfessorId = "P1",
                            Requirement = "",
                            StudentQuantity = 3,
                            Technology = "ASP.NET Core"
                        },
                        new
                        {
                            Id = 3,
                            Category = "Data Science",
                            Description = "Exploring algorithms for predictive modeling",
                            Function = "Train model for project",
                            Name = "Machine Learning",
                            ProfessorId = "P2",
                            Requirement = "",
                            StudentQuantity = 3,
                            Technology = "Python"
                        },
                        new
                        {
                            Id = 4,
                            Category = "Data Science",
                            Description = "Description xyz",
                            Function = "",
                            Name = "Topic abc",
                            ProfessorId = "P2",
                            Requirement = "Requirement something here",
                            StudentQuantity = 2,
                            Technology = "Python"
                        },
                        new
                        {
                            Id = 5,
                            Category = "Other",
                            Description = "Description xyz",
                            Function = "",
                            Name = "Topic opq",
                            ProfessorId = "P2",
                            Requirement = "Requirement something here",
                            StudentQuantity = 2,
                            Technology = "Other"
                        });
                });

            modelBuilder.Entity("ThesisManagement.Models.Feedback", b =>
                {
                    b.HasOne("ThesisManagement.Models.Thesis", "Thesis")
                        .WithMany("Feedbacks")
                        .HasForeignKey("ThesisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Thesis");
                });

            modelBuilder.Entity("ThesisManagement.Models.Student", b =>
                {
                    b.HasOne("ThesisManagement.Models.Thesis", "Thesis")
                        .WithMany("Students")
                        .HasForeignKey("ThesisId");

                    b.Navigation("Thesis");
                });

            modelBuilder.Entity("ThesisManagement.Models.Task", b =>
                {
                    b.HasOne("ThesisManagement.Models.Topic", "Topic")
                        .WithMany("Tasks")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("ThesisManagement.Models.Thesis", b =>
                {
                    b.HasOne("ThesisManagement.Models.Topic", "Topic")
                        .WithMany("Theses")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("ThesisManagement.Models.Topic", b =>
                {
                    b.HasOne("ThesisManagement.Models.Professor", "Professor")
                        .WithMany("Topics")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("ThesisManagement.Models.Professor", b =>
                {
                    b.Navigation("Topics");
                });

            modelBuilder.Entity("ThesisManagement.Models.Thesis", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("ThesisManagement.Models.Topic", b =>
                {
                    b.Navigation("Tasks");

                    b.Navigation("Theses");
                });
#pragma warning restore 612, 618
        }
    }
}
