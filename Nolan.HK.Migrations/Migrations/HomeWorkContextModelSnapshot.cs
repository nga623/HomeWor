﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nolan.HK.Migrations;
using Nolan.Infra.EfCore.PostGresSql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nolan.HK.Migrations.Migrations
{
    [DbContext(typeof(HomeWorkContext))]
    partial class HomeWorkContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Nolan.HK.Domain.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ProjectName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Nolan.HK.Domain.Entities.TimeSheet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApproveStatus")
                        .HasColumnType("text");

                    b.Property<int>("ApproveStatusEnum")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ApproveTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("TotalCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TimeSheet");
                });

            modelBuilder.Entity("Nolan.HK.Domain.Entities.TimeSheetDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectID")
                        .HasColumnType("uuid");

                    b.Property<int>("TimesheetCount")
                        .HasColumnType("integer");

                    b.Property<Guid>("TimesheetID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Userid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("TimeSheetDetail");
                });

            modelBuilder.Entity("Nolan.HK.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("EditTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("UserType")
                        .HasColumnType("text");

                    b.Property<int>("UserTypeEnum")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
