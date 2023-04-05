using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlosomAPI2.Migrations
{
    /// <inheritdoc />
    public partial class AddedNumberBlossom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumberBlossom",
                columns: table => new
                {
                    BlossomNo = table.Column<int>(type: "int", nullable: false),
                    BlossomId = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberBlossom", x => x.BlossomNo);
                    table.ForeignKey(
                        name: "FK_NumberBlossom_Blossoms_BlossomId",
                        column: x => x.BlossomId,
                        principalTable: "Blossoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Blossoms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 3, 18, 21, 30, 5, 199, DateTimeKind.Local).AddTicks(3073), new DateTime(2023, 3, 18, 21, 30, 5, 199, DateTimeKind.Local).AddTicks(3123) });

            migrationBuilder.UpdateData(
                table: "Blossoms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 3, 18, 21, 30, 5, 199, DateTimeKind.Local).AddTicks(3127), new DateTime(2023, 3, 18, 21, 30, 5, 199, DateTimeKind.Local).AddTicks(3128) });

            migrationBuilder.CreateIndex(
                name: "IX_NumberBlossom_BlossomId",
                table: "NumberBlossom",
                column: "BlossomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumberBlossom");

            migrationBuilder.UpdateData(
                table: "Blossoms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 3, 16, 21, 13, 2, 172, DateTimeKind.Local).AddTicks(7028), new DateTime(2023, 3, 16, 21, 13, 2, 172, DateTimeKind.Local).AddTicks(7082) });

            migrationBuilder.UpdateData(
                table: "Blossoms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 3, 16, 21, 13, 2, 172, DateTimeKind.Local).AddTicks(7087), new DateTime(2023, 3, 16, 21, 13, 2, 172, DateTimeKind.Local).AddTicks(7089) });
        }
    }
}
