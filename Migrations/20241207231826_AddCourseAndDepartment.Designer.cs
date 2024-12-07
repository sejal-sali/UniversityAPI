﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversityAPI.Context;

#nullable disable

namespace UniversityAPI.Migrations
{
    [DbContext(typeof(UniversityContext))]
    [Migration("20241207231826_AddCourseAndDepartment")]
    partial class AddCourseAndDepartment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("UniversityAPI.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("CreditHours")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.HasKey("CourseId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("UniversityAPI.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DepartmentHead")
                        .HasColumnType("longtext");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UniversityId")
                        .HasColumnType("int");

                    b.HasKey("DepartmentId");

                    b.HasIndex("UniversityId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("UniversityAPI.Models.University", b =>
                {
                    b.Property<int>("UniversityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Contact")
                        .HasColumnType("longtext");

                    b.Property<int>("EstablishmentYear")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("longtext");

                    b.Property<string>("UniversityName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UniversityType")
                        .HasColumnType("longtext");

                    b.HasKey("UniversityId");

                    b.ToTable("University");
                });

            modelBuilder.Entity("UniversityAPI.Models.Course", b =>
                {
                    b.HasOne("UniversityAPI.Models.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("UniversityAPI.Models.Department", b =>
                {
                    b.HasOne("UniversityAPI.Models.University", "University")
                        .WithMany("Departments")
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("University");
                });

            modelBuilder.Entity("UniversityAPI.Models.Department", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("UniversityAPI.Models.University", b =>
                {
                    b.Navigation("Departments");
                });
#pragma warning restore 612, 618
        }
    }
}
