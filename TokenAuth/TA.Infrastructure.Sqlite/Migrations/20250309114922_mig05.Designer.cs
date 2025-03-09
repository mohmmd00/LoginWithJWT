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
    [Migration("20250309114922_mig05")]
    partial class mig05
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
                    b.HasOne("TA.Domain.Entities.User", "User")
                        .WithOne("Session")
                        .HasForeignKey("TA.Domain.Entities.Session", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TA.Domain.Entities.User", b =>
                {
                    b.Navigation("Session")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
