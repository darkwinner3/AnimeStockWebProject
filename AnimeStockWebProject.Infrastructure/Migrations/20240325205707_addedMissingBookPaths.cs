using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeStockWebProject.Infrastructure.Migrations
{
    public partial class addedMissingBookPaths : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 25, 22, 57, 7, 229, DateTimeKind.Local).AddTicks(4012),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 24, 22, 51, 42, 203, DateTimeKind.Local).AddTicks(576));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "32a6160e-c937-45e8-8448-ae9917c9b788", new DateTime(2024, 3, 25, 22, 57, 7, 227, DateTimeKind.Local).AddTicks(6440), "AQAAAAEAACcQAAAAEMohYriSZt6EXO3y1sR0FXdpocC8kG4r13ReQ8g9fVd8tXyFdmZY+htP+nrQ5ttxWQ==", "567e7bab-5404-4c23-910c-655e058be6c1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ee8ddd02-ce94-4f77-8608-819b08dbbb32"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de95078f-5e19-4461-a194-a83e7e507253", new DateTime(2024, 3, 25, 22, 57, 7, 227, DateTimeKind.Local).AddTicks(6451), "AQAAAAEAACcQAAAAEGaG+cq6PMZdJtGxMgnf2GEhtohyGvtP+CXHDDicOlZ0fSgl2EIluyrGcWn6JRLfLg==", "8c30429d-ee46-4f22-88a6-d9ecb1859009" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "FilePath",
                value: "/Books/Mushoku Tensei(Manga)/Mushoku Tensei - Jobless Reincarnation v01 (2016) (Digital) (goldenagato).cbz");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "FilePath",
                value: "/Books/Chainsaw Man/Chainsaw Man v13 (2023) (Goldenagato).cbz");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 25, 22, 57, 7, 229, DateTimeKind.Local).AddTicks(7293));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 25, 22, 57, 7, 229, DateTimeKind.Local).AddTicks(7303));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 25, 22, 57, 7, 229, DateTimeKind.Local).AddTicks(7304));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 25, 22, 57, 7, 229, DateTimeKind.Local).AddTicks(7306));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 24, 22, 51, 42, 203, DateTimeKind.Local).AddTicks(576),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 25, 22, 57, 7, 229, DateTimeKind.Local).AddTicks(4012));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d2998b4-50e8-4079-8cc2-6718e2740f2d", new DateTime(2024, 3, 24, 22, 51, 42, 201, DateTimeKind.Local).AddTicks(2439), "AQAAAAEAACcQAAAAEBDz+0jb+M5vPp/3Mf7o9CSDcBkmRcywPweP6pLrjrQ6HSc12DNdiMUoftBbrWmiZg==", "9eb8085f-3f00-4366-8bc3-6762020d0599" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ee8ddd02-ce94-4f77-8608-819b08dbbb32"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4433a829-fbf8-4788-a125-8521d026640d", new DateTime(2024, 3, 24, 22, 51, 42, 201, DateTimeKind.Local).AddTicks(2494), "AQAAAAEAACcQAAAAEIfpf1dL3DVmOwdjyds6s7ZKSRr/uKsmFRIFmt97OSuUGEoreJWy5+kkIbF4dPvvUw==", "3b208c6b-1f8a-4d8b-999c-f9128386d638" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "FilePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "FilePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 24, 22, 51, 42, 203, DateTimeKind.Local).AddTicks(4712));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 24, 22, 51, 42, 203, DateTimeKind.Local).AddTicks(4727));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 24, 22, 51, 42, 203, DateTimeKind.Local).AddTicks(4729));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 24, 22, 51, 42, 203, DateTimeKind.Local).AddTicks(4731));
        }
    }
}
