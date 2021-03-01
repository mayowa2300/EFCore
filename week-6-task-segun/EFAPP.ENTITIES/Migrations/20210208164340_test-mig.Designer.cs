﻿// <auto-generated />
using System;
using EFAPP.ENTITIES;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFAPP.ENTITIES.Migrations
{
    [DbContext(typeof(CompanyContext))]
    [Migration("20210208164340_test-mig")]
    partial class testmig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("EFAPP.ENTITIES.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Department_Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("EFAPP.ENTITIES.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("EMAIL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FIRST_NAME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HIRE_DATE")
                        .HasColumnType("datetime2");

                    b.Property<string>("PHONE_NUMBER")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SALARY")
                        .HasColumnType("float");

                    b.Property<string>("STATE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lAST_NAME")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EFAPP.ENTITIES.Employee", b =>
                {
                    b.HasOne("EFAPP.ENTITIES.Department", null)
                        .WithMany("EMPLOYEES")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EFAPP.ENTITIES.Department", b =>
                {
                    b.Navigation("EMPLOYEES");
                });
#pragma warning restore 612, 618
        }
    }
}
