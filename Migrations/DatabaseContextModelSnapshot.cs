﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestApp.DbModels;

namespace TestApp.Migrations;

[DbContext(typeof(DatabaseContext))]
partial class DatabaseContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

        modelBuilder.Entity("TestApp.DbModels.AdditionalColumn", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Description");

                b.Property<string>("Name");

                b.HasKey("Id");

                b.ToTable("AdditionalColumns");
            });

        modelBuilder.Entity("TestApp.DbModels.AdditionalValue", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int?>("ColumnId");

                b.Property<int?>("HeadId");

                b.Property<string>("Value");

                b.HasKey("Id");

                b.HasIndex("ColumnId");

                b.HasIndex("HeadId");

                b.ToTable("AdditionalValues");
            });

        modelBuilder.Entity("TestApp.DbModels.Head", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("CronExp");

                b.Property<string>("Hall");

                b.Property<string>("Ip");

                b.Property<string>("Location");

                b.Property<string>("Name");

                b.HasKey("Id");

                b.ToTable("Heads");
            });

        modelBuilder.Entity("TestApp.DbModels.JobLog", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Exception");

                b.Property<DateTime>("Finish");

                b.Property<int?>("HeadId");

                b.Property<string>("JobLogs");

                b.Property<DateTime>("Start");

                b.Property<bool>("WithoutException");

                b.HasKey("Id");

                b.HasIndex("HeadId");

                b.ToTable("JobLogs");
            });

        modelBuilder.Entity("TestApp.DbModels.AdditionalValue", b =>
            {
                b.HasOne("TestApp.DbModels.AdditionalColumn", "Column")
                    .WithMany()
                    .HasForeignKey("ColumnId");

                b.HasOne("TestApp.DbModels.Head", "Head")
                    .WithMany("AddidionalValues")
                    .HasForeignKey("HeadId");
            });

        modelBuilder.Entity("TestApp.DbModels.JobLog", b =>
            {
                b.HasOne("TestApp.DbModels.Head", "Head")
                    .WithMany()
                    .HasForeignKey("HeadId");
            });
#pragma warning restore 612, 618
    }
}
