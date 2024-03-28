﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThesisManagement.Repositories.EF;

#nullable disable

namespace ThesisManagement.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240328031246_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("ThesisManagement.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Read")
                        .HasColumnType("bit");

                    b.Property<int>("ThesisId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ThesisId");

                    b.ToTable("Notification");
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
                            Email = "anh@hcmute.edu.vn",
                            Name = "Trần Văn Anh",
                            Password = "anh12345",
                            Phone = "123-456-7890"
                        },
                        new
                        {
                            Id = "P2",
                            Birthday = new DateTime(1975, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "lenguyen@gmail.com",
                            Name = "Lê Nguyên",
                            Password = "nguyen12345",
                            Phone = "987-654-3210"
                        },
                        new
                        {
                            Id = "P3",
                            Birthday = new DateTime(1975, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "lam@hcmute.edu.vn",
                            Name = "Đặng Lâm",
                            Password = "lam12345",
                            Phone = "987-654-3210"
                        });
                });

            modelBuilder.Entity("ThesisManagement.Models.ScheduleInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ThesisId")
                        .HasColumnType("int");

                    b.Property<DateTime>("To")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ThesisId");

                    b.ToTable("ScheduleInfos");
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
                            Id = "22133010",
                            Birthday = new DateTime(2000, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "a@student.com",
                            Name = "Nguyễn A",
                            Password = "a12345",
                            Phone = "123-456-7890"
                        },
                        new
                        {
                            Id = "22133011",
                            Birthday = new DateTime(2001, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "b@student.com",
                            Name = "Lâm B",
                            Password = "b12345",
                            Phone = "987-654-3210"
                        },
                        new
                        {
                            Id = "22110001",
                            Birthday = new DateTime(2001, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "c@student.com",
                            Name = "Võ C",
                            Password = "c12345",
                            Phone = "987-654-3210"
                        },
                        new
                        {
                            Id = "22110010",
                            Birthday = new DateTime(2001, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "d@student.com",
                            Name = "Nguyễn D",
                            Password = "d12345",
                            Phone = "987-654-3210"
                        },
                        new
                        {
                            Id = "22133015",
                            Birthday = new DateTime(2001, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "e@student.com",
                            Name = "Trần E",
                            Password = "e12346",
                            Phone = "987-654-3210"
                        });
                });

            modelBuilder.Entity("ThesisManagement.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Progress")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<int>("ThesisId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ThesisId");

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
                            Name = "Quản lý ngân ",
                            ProfessorId = "P1",
                            Requirement = "",
                            StudentQuantity = 2,
                            Technology = "SQL"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Web Development",
                            Description = "Xây dựng website Quản lý công ty quy mô vừa và nhỏ",
                            Function = "Trả lương nhân viên. Giao Tasks theo các cấp.",
                            Name = "Quản lý công ty",
                            ProfessorId = "P1",
                            Requirement = "Đúng deadline, teamwork",
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

            modelBuilder.Entity("ThesisManagement.Models.Notification", b =>
                {
                    b.HasOne("ThesisManagement.Models.Thesis", "Thesis")
                        .WithMany("Notifications")
                        .HasForeignKey("ThesisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Thesis");
                });

            modelBuilder.Entity("ThesisManagement.Models.ScheduleInfo", b =>
                {
                    b.HasOne("ThesisManagement.Models.Thesis", "Thesis")
                        .WithMany("ScheduleInfos")
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
                    b.HasOne("ThesisManagement.Models.Thesis", "Thesis")
                        .WithMany("Tasks")
                        .HasForeignKey("ThesisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Thesis");
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

                    b.Navigation("Notifications");

                    b.Navigation("ScheduleInfos");

                    b.Navigation("Students");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("ThesisManagement.Models.Topic", b =>
                {
                    b.Navigation("Theses");
                });
#pragma warning restore 612, 618
        }
    }
}