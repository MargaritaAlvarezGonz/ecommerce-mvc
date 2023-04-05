using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlosomAPI2.Migrations
{
    /// <inheritdoc />
    public partial class Controller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumberBlossom_Blossoms_BlossomId",
                table: "NumberBlossom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NumberBlossom",
                table: "NumberBlossom");

            migrationBuilder.RenameTable(
                name: "NumberBlossom",
                newName: "NumberBlossoms");

            migrationBuilder.RenameIndex(
                name: "IX_NumberBlossom_BlossomId",
                table: "NumberBlossoms",
                newName: "IX_NumberBlossoms_BlossomId");

            migrationBuilder.AlterColumn<string>(
                name: "ProductDescrip",
                table: "Blossoms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Blossoms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Blossoms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Blossoms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "NumberBlossoms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumberBlossoms",
                table: "NumberBlossoms",
                column: "BlossomNo");

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

            migrationBuilder.AddForeignKey(
                name: "FK_NumberBlossoms_Blossoms_BlossomId",
                table: "NumberBlossoms",
                column: "BlossomId",
                principalTable: "Blossoms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumberBlossoms_Blossoms_BlossomId",
                table: "NumberBlossoms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NumberBlossoms",
                table: "NumberBlossoms");

            migrationBuilder.RenameTable(
                name: "NumberBlossoms",
                newName: "NumberBlossom");

            migrationBuilder.RenameIndex(
                name: "IX_NumberBlossoms_BlossomId",
                table: "NumberBlossom",
                newName: "IX_NumberBlossom_BlossomId");

            migrationBuilder.AlterColumn<string>(
                name: "ProductDescrip",
                table: "Blossoms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Blossoms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Blossoms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Blossoms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "NumberBlossom",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumberBlossom",
                table: "NumberBlossom",
                column: "BlossomNo");

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

            migrationBuilder.AddForeignKey(
                name: "FK_NumberBlossom_Blossoms_BlossomId",
                table: "NumberBlossom",
                column: "BlossomId",
                principalTable: "Blossoms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
