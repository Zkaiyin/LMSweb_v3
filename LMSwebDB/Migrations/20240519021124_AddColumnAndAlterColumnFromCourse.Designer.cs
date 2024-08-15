﻿// <auto-generated />
using System;
using LMSwebDB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LMSwebDB.Migrations
{
    [DbContext(typeof(LMSContext))]
    [Migration("20240519021124_AddColumnAndAlterColumnFromCourse")]
    partial class AddColumnAndAlterColumnFromCourse
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LMSwebDB.Models.Assistant", b =>
                {
                    b.Property<string>("AssistantId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("AssistantID");

                    b.Property<string>("AssistantName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.HasKey("AssistantId");

                    b.ToTable("Assistants");
                });

            modelBuilder.Entity("LMSwebDB.Models.Course", b =>
                {
                    b.Property<string>("CourseId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("CourseID");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("CourseName");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("GreetingMessage")
                        .HasColumnType("ntext")
                        .HasColumnName("GreetingMessage");

                    b.Property<bool>("IsNeedContext")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LLMModel")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("LLMModel");

                    b.Property<string>("SystemPrompt")
                        .HasColumnType("ntext")
                        .HasColumnName("SystemPrompt");

                    b.Property<string>("TeacherId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("TeacherID");

                    b.Property<double>("Temperature")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0.0);

                    b.Property<string>("UserPrompt")
                        .HasColumnType("ntext")
                        .HasColumnName("UserPrompt");

                    b.HasKey("CourseId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("LMSwebDB.Models.Manage", b =>
                {
                    b.Property<string>("AssistantId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("AssistantID");

                    b.Property<string>("CourseId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("CourseID");

                    b.HasKey("AssistantId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("Manages");
                });

            modelBuilder.Entity("LMSwebDB.Models.Material", b =>
                {
                    b.Property<string>("MaterialId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("MaterialID");

                    b.Property<string>("CourseID")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("CourseID");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("UploadTime")
                        .HasColumnType("datetime");

                    b.HasKey("MaterialId")
                        .HasName("PK_Material");

                    b.HasIndex("CourseID");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("LMSwebDB.Models.QnA", b =>
                {
                    b.Property<string>("QnAId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("QnAID");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("CourseId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("CourseID");

                    b.Property<string>("Embeddings")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("MaterialId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("MaterialID");

                    b.HasKey("QnAId");

                    b.HasIndex("MaterialId");

                    b.ToTable("QnAs");
                });

            modelBuilder.Entity("LMSwebDB.Models.Student", b =>
                {
                    b.Property<string>("StudentId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("StudentID");

                    b.Property<string>("CourseId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("CourseID");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.HasKey("StudentId")
                        .HasName("PK_Student");

                    b.HasIndex("CourseId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            StudentId = "S001",
                            StudentName = "林小楷"
                        },
                        new
                        {
                            StudentId = "S002",
                            StudentName = "李阿禎"
                        },
                        new
                        {
                            StudentId = "S003",
                            StudentName = "許小琪"
                        },
                        new
                        {
                            StudentId = "S004",
                            StudentName = "Kevin"
                        },
                        new
                        {
                            StudentId = "S005",
                            StudentName = "Vivian"
                        },
                        new
                        {
                            StudentId = "S006",
                            StudentName = "Amy"
                        });
                });

            modelBuilder.Entity("LMSwebDB.Models.Teacher", b =>
                {
                    b.Property<string>("TeacherId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("TeacherID");

                    b.Property<string>("TeacherName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.HasKey("TeacherId")
                        .HasName("PK_Teacher");

                    b.ToTable("Teachers");

                    b.HasData(
                        new
                        {
                            TeacherId = "T001",
                            TeacherName = "陳立維"
                        },
                        new
                        {
                            TeacherId = "T002",
                            TeacherName = "曾老師"
                        },
                        new
                        {
                            TeacherId = "T003",
                            TeacherName = "李偉老師"
                        },
                        new
                        {
                            TeacherId = "T004",
                            TeacherName = "焰超老師"
                        },
                        new
                        {
                            TeacherId = "T005",
                            TeacherName = "蔡老師"
                        });
                });

            modelBuilder.Entity("LMSwebDB.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("UserID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Upassword")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("UPassword");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = "S001",
                            Name = "林小楷",
                            RoleName = "Student",
                            Upassword = "c5e0e70cb9001fc326a9d5b3c39c1d3b48919bf3adacc633729bfff7c27f1d26"
                        },
                        new
                        {
                            UserId = "S002",
                            Name = "李阿禎",
                            RoleName = "Student",
                            Upassword = "c842f8af9e82946217a5f35c046c0470ce855b145a093448295d758810c68303"
                        },
                        new
                        {
                            UserId = "S003",
                            Name = "許小琪",
                            RoleName = "Student",
                            Upassword = "f598aee2eda0ebd461a60eb24ecc6378674be0d06a591ef8f25b201e4f619e48"
                        },
                        new
                        {
                            UserId = "S004",
                            Name = "Kevin",
                            RoleName = "Student",
                            Upassword = "db3ea858db39f2f6eafd7ad39f7798428d9e6244a430919f84c7dc8b905081ad"
                        },
                        new
                        {
                            UserId = "S005",
                            Name = "Vivian",
                            RoleName = "Student",
                            Upassword = "09fd191dc08a0375f4f10fd8ce970d8193a0b475bb3d75db4b8221e8f0d74979"
                        },
                        new
                        {
                            UserId = "S006",
                            Name = "Amy",
                            RoleName = "Student",
                            Upassword = "19a85017e5a5057f9cb3104e7afde89aea6c4d74f544ba5eaeaab256bcf937af"
                        },
                        new
                        {
                            UserId = "T001",
                            Name = "林廣學",
                            RoleName = "Teacher",
                            Upassword = "15152d459354c17470fbeba5c03aa9b0790b237b04f190aba04b2a3d1afe64bf"
                        },
                        new
                        {
                            UserId = "T002",
                            Name = "洪子秀 老師",
                            RoleName = "Teacher",
                            Upassword = "9ba7d0652682e1fe75b90bd1ea8a1a69e679a0039c80fc9c85e10e2ff7ddc793"
                        },
                        new
                        {
                            UserId = "T003",
                            Name = "曾秋蓉 老師",
                            RoleName = "Teacher",
                            Upassword = "963606fbc3791a6c3053264f977ce910821a69680e5e41de99e6b3f04d7d0471"
                        },
                        new
                        {
                            UserId = "T004",
                            Name = "李偉 老師",
                            RoleName = "Teacher",
                            Upassword = "877b4011250e9bc6afe05dcddfa93ec093139527a094a8fb0f8f6f80bf0cce2e"
                        },
                        new
                        {
                            UserId = "T005",
                            Name = "蔡老師",
                            RoleName = "Teacher",
                            Upassword = "369be97f36a54ac72f4e7e3a69ca6860b6bf17d148b0686d0ff9e18a1bd32249"
                        });
                });

            modelBuilder.Entity("LMSwebDB.Models.UserQALog", b =>
                {
                    b.Property<string>("LogId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("LogID");

                    b.Property<string>("AnswerFromGPT")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<int>("CompletionToken")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("CourseId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("CourseID");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MaterialId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("MaterialID");

                    b.Property<int>("PromptToken")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<float>("Score")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(0f);

                    b.Property<string>("SystemPrompt")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<int>("TotalToken")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("StudentID");

                    b.Property<string>("UserPrompt")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("UserQuestion")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.HasKey("LogId")
                        .HasName("PK_Log");

                    b.ToTable("UserQALogs");
                });

            modelBuilder.Entity("LMSwebDB.Models.Assistant", b =>
                {
                    b.HasOne("LMSwebDB.Models.User", "AssistantNavigation")
                        .WithOne("Assistant")
                        .HasForeignKey("LMSwebDB.Models.Assistant", "AssistantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Assistants_Users");

                    b.Navigation("AssistantNavigation");
                });

            modelBuilder.Entity("LMSwebDB.Models.Course", b =>
                {
                    b.HasOne("LMSwebDB.Models.Teacher", "Teacher")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_Courses_Teachers");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("LMSwebDB.Models.Manage", b =>
                {
                    b.HasOne("LMSwebDB.Models.Assistant", "Assistant")
                        .WithMany("Manage")
                        .HasForeignKey("AssistantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Assistants_Manage");

                    b.HasOne("LMSwebDB.Models.Course", "Course")
                        .WithMany("Manages")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Courses_Manage");

                    b.Navigation("Assistant");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("LMSwebDB.Models.Material", b =>
                {
                    b.HasOne("LMSwebDB.Models.Course", "Course")
                        .WithMany("Materials")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Material_Course");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("LMSwebDB.Models.QnA", b =>
                {
                    b.HasOne("LMSwebDB.Models.Material", "Material")
                        .WithMany("QnAs")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_QnA_Material");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("LMSwebDB.Models.Student", b =>
                {
                    b.HasOne("LMSwebDB.Models.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Student_Course");

                    b.HasOne("LMSwebDB.Models.User", "StudentNavigation")
                        .WithOne("Student")
                        .HasForeignKey("LMSwebDB.Models.Student", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Students_Users");

                    b.Navigation("Course");

                    b.Navigation("StudentNavigation");
                });

            modelBuilder.Entity("LMSwebDB.Models.Teacher", b =>
                {
                    b.HasOne("LMSwebDB.Models.User", "TeacherNavigation")
                        .WithOne("Teacher")
                        .HasForeignKey("LMSwebDB.Models.Teacher", "TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Teachers_Users");

                    b.Navigation("TeacherNavigation");
                });

            modelBuilder.Entity("LMSwebDB.Models.Assistant", b =>
                {
                    b.Navigation("Manage");
                });

            modelBuilder.Entity("LMSwebDB.Models.Course", b =>
                {
                    b.Navigation("Manages");

                    b.Navigation("Materials");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("LMSwebDB.Models.Material", b =>
                {
                    b.Navigation("QnAs");
                });

            modelBuilder.Entity("LMSwebDB.Models.Teacher", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("LMSwebDB.Models.User", b =>
                {
                    b.Navigation("Assistant");

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });
#pragma warning restore 612, 618
        }
    }
}
