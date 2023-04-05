using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlosomAPI2.Migrations
{
    /// <inheritdoc />
    public partial class UsersMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Blossoms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 3, 20, 10, 31, 49, 245, DateTimeKind.Local).AddTicks(6435), new DateTime(2023, 3, 20, 10, 31, 49, 245, DateTimeKind.Local).AddTicks(6490) });

            migrationBuilder.UpdateData(
                table: "Blossoms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 3, 20, 10, 31, 49, 245, DateTimeKind.Local).AddTicks(6495), new DateTime(2023, 3, 20, 10, 31, 49, 245, DateTimeKind.Local).AddTicks(6497) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.UpdateData(
                table: "Blossoms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 3, 19, 21, 19, 24, 49, DateTimeKind.Local).AddTicks(4861), new DateTime(2023, 3, 19, 21, 19, 24, 49, DateTimeKind.Local).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "Blossoms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 3, 19, 21, 19, 24, 49, DateTimeKind.Local).AddTicks(4914), new DateTime(2023, 3, 19, 21, 19, 24, 49, DateTimeKind.Local).AddTicks(4916) });
        }
    }
}
