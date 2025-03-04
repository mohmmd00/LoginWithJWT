﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TA.Infrastructure.Sqlite;

#nullable disable

namespace TA.Infrastructure.Sqlite.Migrations
{
    [DbContext(typeof(AuthContext))]
    [Migration("20250304121201_mig01")]
    partial class mig01
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("TA.Domain.Entities.Session", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SessionId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("TA.Domain.Entities.User", b =>
                {
                    b.Property<int>("PrimaryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PrimaryId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TA.Domain.Entities.Session", b =>
                {
                    b.HasOne("TA.Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("TA.Domain.Entities.Session", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
