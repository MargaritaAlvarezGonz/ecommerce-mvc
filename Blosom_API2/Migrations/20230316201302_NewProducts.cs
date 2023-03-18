using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlosomAPI2.Migrations
{
    /// <inheritdoc />
    public partial class NewProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Blossoms",
                columns: new[] { "Id", "Brand", "DateCreated", "DateUpdated", "ImageUrl", "Name", "Price", "ProductDescrip", "Stock" },
                values: new object[,]
                {
                    { 1, "Vegan World", new DateTime(2023, 3, 16, 21, 13, 2, 172, DateTimeKind.Local).AddTicks(7028), new DateTime(2023, 3, 16, 21, 13, 2, 172, DateTimeKind.Local).AddTicks(7082), "", "CC Organic Cream", 10.0, "Product 100% natural", 100 },
                    { 2, "Terpenic", new DateTime(2023, 3, 16, 21, 13, 2, 172, DateTimeKind.Local).AddTicks(7087), new DateTime(2023, 3, 16, 21, 13, 2, 172, DateTimeKind.Local).AddTicks(7089), "", "Essential oil lavender", 30.0, "Product 100% natural", 80 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blossoms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Blossoms",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
