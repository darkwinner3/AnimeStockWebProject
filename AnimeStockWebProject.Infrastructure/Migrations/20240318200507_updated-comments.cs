using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeStockWebProject.Infrastructure.Migrations
{
    public partial class updatedcomments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 18, 22, 5, 7, 418, DateTimeKind.Local).AddTicks(3222),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 10, 23, 35, 23, 818, DateTimeKind.Local).AddTicks(9184));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "727ceb76-c832-48b7-a459-3e7fff1ffafd", new DateTime(2024, 3, 18, 22, 5, 7, 416, DateTimeKind.Local).AddTicks(5984), "AQAAAAEAACcQAAAAEA0awQKjx1kYCEl/5jJvI4FlngWjSwUo0aB4O6YJUaIj5HHX5stI2PRQ71/wfiNVOg==", "0173f73f-fc7c-45e2-8efd-5b1876502c88" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ee8ddd02-ce94-4f77-8608-819b08dbbb32"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0aea9579-ee61-4ba2-a31a-22a483b9b2b5", new DateTime(2024, 3, 18, 22, 5, 7, 416, DateTimeKind.Local).AddTicks(5994), "AQAAAAEAACcQAAAAEK/0L3akmBC1d5p+PoNk7a1cQV+nPGch3clKTq0u9CozbJYJkJ+QHiSxs530Fy67hQ==", "d9367f2a-b93d-4cc3-b20f-0684a843891a" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 18, 22, 5, 7, 418, DateTimeKind.Local).AddTicks(6450));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 18, 22, 5, 7, 418, DateTimeKind.Local).AddTicks(6458));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 18, 22, 5, 7, 418, DateTimeKind.Local).AddTicks(6460));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 18, 22, 5, 7, 418, DateTimeKind.Local).AddTicks(6461));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 10, 23, 35, 23, 818, DateTimeKind.Local).AddTicks(9184),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 18, 22, 5, 7, 418, DateTimeKind.Local).AddTicks(3222));

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
        }
    }
}
