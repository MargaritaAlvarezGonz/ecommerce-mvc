﻿// <auto-generated />
using System;
using Blosom_API2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlosomAPI2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230319201924_Controller")]
    partial class Controller
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Blosom_API2.Models.NumberBlossom", b =>
                {
                    b.Property<int>("BlossomNo")
                        .HasColumnType("int");

                    b.Property<int>("BlossomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BlossomNo");

                    b.HasIndex("BlossomId");

                    b.ToTable("NumberBlossoms");
                });

            modelBuilder.Entity("Blossom_API.Models.Blossom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProductDescrip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Blossoms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Vegan World",
                            DateCreated = new DateTime(2023, 3, 19, 21, 19, 24, 49, DateTimeKind.Local).AddTicks(4861),
                            DateUpdated = new DateTime(2023, 3, 19, 21, 19, 24, 49, DateTimeKind.Local).AddTicks(4910),
                            ImageUrl = "",
                            Name = "CC Organic Cream",
                            Price = 10.0,
                            ProductDescrip = "Product 100% natural",
                            Stock = 100
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Terpenic",
                            DateCreated = new DateTime(2023, 3, 19, 21, 19, 24, 49, DateTimeKind.Local).AddTicks(4914),
                            DateUpdated = new DateTime(2023, 3, 19, 21, 19, 24, 49, DateTimeKind.Local).AddTicks(4916),
                            ImageUrl = "",
                            Name = "Essential oil lavender",
                            Price = 30.0,
                            ProductDescrip = "Product 100% natural",
                            Stock = 80
                        });
                });

            modelBuilder.Entity("Blosom_API2.Models.NumberBlossom", b =>
                {
                    b.HasOne("Blossom_API.Models.Blossom", "Blossom")
                        .WithMany()
                        .HasForeignKey("BlossomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blossom");
                });
#pragma warning restore 612, 618
        }
    }
}
