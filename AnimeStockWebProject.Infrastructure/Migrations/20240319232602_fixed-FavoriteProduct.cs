using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeStockWebProject.Infrastructure.Migrations
{
    public partial class fixedFavoriteProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProducts_AspNetUsers_UserId",
                table: "FavoriteProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteProducts",
                table: "FavoriteProducts");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "FavoriteProducts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "FavoriteProducts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FavoriteProducts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 20, 1, 26, 2, 115, DateTimeKind.Local).AddTicks(2681),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 18, 22, 5, 7, 418, DateTimeKind.Local).AddTicks(3222));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteProducts",
                table: "FavoriteProducts",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d32a8ee-2443-48e0-b649-61171683016e", new DateTime(2024, 3, 20, 1, 26, 2, 113, DateTimeKind.Local).AddTicks(4977), "AQAAAAEAACcQAAAAEMx0KUn+jTvdY8ApAVji4eY/gRQG3ebkajYlQW4OSX1SXBxHLRY2hnZn7Jjw48s2Og==", "ef908b7a-d161-46dd-912b-3d85a584b3b1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ee8ddd02-ce94-4f77-8608-819b08dbbb32"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "30b2c2e1-01d5-49cc-9af9-6509b8577453", new DateTime(2024, 3, 20, 1, 26, 2, 113, DateTimeKind.Local).AddTicks(4986), "AQAAAAEAACcQAAAAEPjfz6l8XdqML4CMAbLysjESX92CWxCk++A385w9Eu928WPLYjjZpXhO5DakdnpBXQ==", "63e1a138-911a-46ab-86fe-bc7ccd9a63b1" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 20, 1, 26, 2, 115, DateTimeKind.Local).AddTicks(6102));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 20, 1, 26, 2, 115, DateTimeKind.Local).AddTicks(6113));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 20, 1, 26, 2, 115, DateTimeKind.Local).AddTicks(6115));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 20, 1, 26, 2, 115, DateTimeKind.Local).AddTicks(6116));

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProducts_UserId",
                table: "FavoriteProducts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProducts_AspNetUsers_UserId",
                table: "FavoriteProducts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProducts_AspNetUsers_UserId",
                table: "FavoriteProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteProducts",
                table: "FavoriteProducts");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteProducts_UserId",
                table: "FavoriteProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FavoriteProducts");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "FavoriteProducts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "FavoriteProducts",
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
                defaultValue: new DateTime(2024, 3, 18, 22, 5, 7, 418, DateTimeKind.Local).AddTicks(3222),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 20, 1, 26, 2, 115, DateTimeKind.Local).AddTicks(2681));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteProducts",
                table: "FavoriteProducts",
                columns: new[] { "UserId", "BookId", "GameId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProducts_AspNetUsers_UserId",
                table: "FavoriteProducts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
