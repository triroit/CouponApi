﻿// <auto-generated />
using System;
using CouponApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CouponApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240820150849_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("CouponApi.Models.Coupon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Percent")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            Id = new Guid("174f660f-723e-40a2-8bd1-25778c4884c7"),
                            CreatedDate = new DateTime(2024, 8, 20, 18, 8, 45, 872, DateTimeKind.Local).AddTicks(7139),
                            IsActive = true,
                            Name = "10OFF",
                            Percent = 10,
                            UpdatedDate = new DateTime(2024, 8, 20, 18, 8, 45, 872, DateTimeKind.Local).AddTicks(7153)
                        },
                        new
                        {
                            Id = new Guid("98bc6939-28b0-41a1-b86f-87677e9ba2f2"),
                            CreatedDate = new DateTime(2024, 8, 20, 18, 8, 45, 872, DateTimeKind.Local).AddTicks(7158),
                            IsActive = true,
                            Name = "20OFF",
                            Percent = 20,
                            UpdatedDate = new DateTime(2024, 8, 20, 18, 8, 45, 872, DateTimeKind.Local).AddTicks(7160)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
