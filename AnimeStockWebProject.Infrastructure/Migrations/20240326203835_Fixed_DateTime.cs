using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeStockWebProject.Infrastructure.Migrations
{
    public partial class Fixed_DateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserOrders",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 26, 22, 38, 35, 352, DateTimeKind.Local).AddTicks(7275),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 26, 21, 14, 36, 743, DateTimeKind.Local).AddTicks(8026));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7caf580f-c1cc-4c95-bee9-a379a2df03be", new DateTime(2024, 3, 26, 22, 38, 35, 350, DateTimeKind.Local).AddTicks(9753), "AQAAAAEAACcQAAAAEIuBJMeMD82ukm/RJvBjokCjACgiPFSdbroa4kUnTL/4uvZ5nAPynWIm6Jhoqz/diw==", "92558122-45a5-4b23-9ec2-4a4100418f45" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ee8ddd02-ce94-4f77-8608-819b08dbbb32"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2df858af-dc53-43cf-8c4c-ae504811cf25", new DateTime(2024, 3, 26, 22, 38, 35, 350, DateTimeKind.Local).AddTicks(9770), "AQAAAAEAACcQAAAAENbYuywdfypTuonnWYSro65sLHRgVjSk7N8g8qWKtBhZcED3YigepklkUCALBjNxTw==", "462476cf-bb7c-4846-bd52-b3fc218fe189" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 26, 22, 38, 35, 353, DateTimeKind.Local).AddTicks(454));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 26, 22, 38, 35, 353, DateTimeKind.Local).AddTicks(465));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 26, 22, 38, 35, 353, DateTimeKind.Local).AddTicks(467));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 26, 22, 38, 35, 353, DateTimeKind.Local).AddTicks(469));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserOrders",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 26, 21, 14, 36, 743, DateTimeKind.Local).AddTicks(8026),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 26, 22, 38, 35, 352, DateTimeKind.Local).AddTicks(7275));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d77efe20-d561-4eb1-b796-a4e48107bbca", new DateTime(2024, 3, 26, 21, 14, 36, 742, DateTimeKind.Local).AddTicks(297), "AQAAAAEAACcQAAAAEAdjNdantSuYm/il7WY0TLhJli85kNBoAlndIwkzj854MH8Ty9Lf+l8SdAiDNiiUxw==", "fea0ba89-f473-4497-a504-d49751bc42bf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ee8ddd02-ce94-4f77-8608-819b08dbbb32"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a939302-8d0b-4195-b1ab-98aafbda2949", new DateTime(2024, 3, 26, 21, 14, 36, 742, DateTimeKind.Local).AddTicks(313), "AQAAAAEAACcQAAAAEK2rZAOW3N0qzV2HkVQ9gChySEXJcMpKUFEQOrfd9Kmz81YiEqoYU+hUVEQQU7x80g==", "092d0884-9164-4990-a729-37de3229e07e" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 26, 21, 14, 36, 744, DateTimeKind.Local).AddTicks(1238));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 26, 21, 14, 36, 744, DateTimeKind.Local).AddTicks(1247));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 26, 21, 14, 36, 744, DateTimeKind.Local).AddTicks(1249));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 26, 21, 14, 36, 744, DateTimeKind.Local).AddTicks(1250));
        }
    }
}
