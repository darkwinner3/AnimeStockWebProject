using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeStockWebProject.Infrastructure.Migrations
{
    public partial class fixed_Enums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 10, 23, 35, 23, 818, DateTimeKind.Local).AddTicks(9184),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 10, 19, 50, 50, 336, DateTimeKind.Local).AddTicks(9153));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1c08882-79bf-4398-83c4-a20b5e785b29", new DateTime(2024, 3, 10, 23, 35, 23, 817, DateTimeKind.Local).AddTicks(1451), "AQAAAAEAACcQAAAAEDdjcV6IND099Hp6EnxMkHJB8oMF3RSMjk7mL9k0KTf6gQfAotSWI1PwV4enB/ZPyQ==", "d010fa0e-55a4-4a47-ab5a-d41467cd2303" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ee8ddd02-ce94-4f77-8608-819b08dbbb32"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "639ff9c4-bea5-43b7-bfd0-68d2287a72ee", new DateTime(2024, 3, 10, 23, 35, 23, 817, DateTimeKind.Local).AddTicks(1460), "AQAAAAEAACcQAAAAEMLaF4uGBtaIwMOQyitTX9ZIJkBcGfRp/qCnjxEjVVDlFgDKwLUhG5FmPCte3osbnQ==", "91555806-8d5a-4827-a5e2-f2fd0dd30921" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "PrintType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "PrintType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 10, 23, 35, 23, 819, DateTimeKind.Local).AddTicks(4484));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 10, 23, 35, 23, 819, DateTimeKind.Local).AddTicks(4498));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 10, 23, 35, 23, 819, DateTimeKind.Local).AddTicks(4500));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 10, 23, 35, 23, 819, DateTimeKind.Local).AddTicks(4502));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "PrintType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "PrintType",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 10, 19, 50, 50, 336, DateTimeKind.Local).AddTicks(9153),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 10, 23, 35, 23, 818, DateTimeKind.Local).AddTicks(9184));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35a846b3-2ed8-4e52-abf4-f7064f1a0ecd", new DateTime(2024, 3, 10, 19, 50, 50, 335, DateTimeKind.Local).AddTicks(1414), "AQAAAAEAACcQAAAAENDD/sN62aXSmS2oRGh0XpJxsEqlHS+MUSRFLk1LXCxBVwAGcdMSEPVNEYlwCSGmsA==", "143f4fa6-61c6-4a81-9da6-7cc148332c21" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ee8ddd02-ce94-4f77-8608-819b08dbbb32"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c99a3a76-0b51-494e-ab3e-95956f06a4d1", new DateTime(2024, 3, 10, 19, 50, 50, 335, DateTimeKind.Local).AddTicks(1428), "AQAAAAEAACcQAAAAEDh9wie2kd45ATehmuuk1b0pfdXgKHhKz0pnQek6pc8xnJ597Qm4MrhcqF3qwVQIIA==", "780a3e61-bc0b-4ea4-a919-0e5c8d2bd136" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "PrintType",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "PrintType",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 10, 19, 50, 50, 337, DateTimeKind.Local).AddTicks(2725));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 10, 19, 50, 50, 337, DateTimeKind.Local).AddTicks(2737));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 10, 19, 50, 50, 337, DateTimeKind.Local).AddTicks(2739));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 10, 19, 50, 50, 337, DateTimeKind.Local).AddTicks(2740));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "PrintType",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "PrintType",
                value: 0);
        }
    }
}
