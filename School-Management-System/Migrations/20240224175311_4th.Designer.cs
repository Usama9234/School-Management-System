﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using School_Management_System.Data;

#nullable disable

namespace School_Management_System.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240224175311_4th")]
    partial class _4th
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("School_Management_System.Models.Admin.Classes", b =>
                {
                    b.Property<int>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClassId"));

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("FeesFeeId")
                        .HasColumnType("int");

                    b.HasKey("ClassId");

                    b.HasIndex("FeesFeeId");

                    b.ToTable("Class");
                });

            modelBuilder.Entity("School_Management_System.Models.Admin.Fees", b =>
                {
                    b.Property<int>("FeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeeId"));

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<int>("FeeAmount")
                        .HasColumnType("int");

                    b.HasKey("FeeId");

                    b.HasIndex("ClassId");

                    b.ToTable("fees");
                });

            modelBuilder.Entity("School_Management_System.Models.Admin.Classes", b =>
                {
                    b.HasOne("School_Management_System.Models.Admin.Fees", null)
                        .WithMany("ClassesList")
                        .HasForeignKey("FeesFeeId");
                });

            modelBuilder.Entity("School_Management_System.Models.Admin.Fees", b =>
                {
                    b.HasOne("School_Management_System.Models.Admin.Classes", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("School_Management_System.Models.Admin.Fees", b =>
                {
                    b.Navigation("ClassesList");
                });
#pragma warning restore 612, 618
        }
    }
}