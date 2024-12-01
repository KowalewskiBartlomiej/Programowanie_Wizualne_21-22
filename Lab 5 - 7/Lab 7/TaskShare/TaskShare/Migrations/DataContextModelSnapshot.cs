﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskShare.Models;

#nullable disable

namespace TaskShare.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("IssueUser", b =>
                {
                    b.Property<int>("IssuesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsersId")
                        .HasColumnType("INTEGER");

                    b.HasKey("IssuesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("IssueUser");
                });

            modelBuilder.Entity("TaskShare.Models.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Label")
                        .IsUnique();

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("TaskShare.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Occurred")
                        .HasColumnType("TEXT");

                    b.Property<int>("StatusType")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TaskId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("TaskShare.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("IssueId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TimeCost")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.HasIndex("Label")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TaskShare.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Pseudonym")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Pseudonym")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IssueUser", b =>
                {
                    b.HasOne("TaskShare.Models.Issue", null)
                        .WithMany()
                        .HasForeignKey("IssuesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskShare.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskShare.Models.Status", b =>
                {
                    b.HasOne("TaskShare.Models.Task", "Task")
                        .WithMany("Statuses")
                        .HasForeignKey("TaskId");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskShare.Models.Task", b =>
                {
                    b.HasOne("TaskShare.Models.Issue", "Issue")
                        .WithMany("Tasks")
                        .HasForeignKey("IssueId");

                    b.HasOne("TaskShare.Models.User", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserId");

                    b.Navigation("Issue");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskShare.Models.Issue", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TaskShare.Models.Task", b =>
                {
                    b.Navigation("Statuses");
                });

            modelBuilder.Entity("TaskShare.Models.User", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
