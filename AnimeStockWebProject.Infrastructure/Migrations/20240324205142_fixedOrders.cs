using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeStockWebProject.Infrastructure.Migrations
{
    public partial class fixedOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 24, 22, 51, 42, 203, DateTimeKind.Local).AddTicks(576),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 21, 19, 36, 8, 662, DateTimeKind.Local).AddTicks(454));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Orders",
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
                defaultValue: new DateTime(2024, 3, 21, 19, 36, 8, 662, DateTimeKind.Local).AddTicks(454),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 24, 22, 51, 42, 203, DateTimeKind.Local).AddTicks(576));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "88e36b83-488e-45b4-a305-34c612f73614", new DateTime(2024, 3, 21, 19, 36, 8, 660, DateTimeKind.Local).AddTicks(2698), "AQAAAAEAACcQAAAAEJ2rcrUfd+9Ed/ULdwrbsf8tmM7DOxO1b1EjXilkNLAizsPG+i6WCrKbhGQ5Z0q/FA==", "4277cdce-ce0f-4fdd-a14f-55b094645101" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ee8ddd02-ce94-4f77-8608-819b08dbbb32"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1366fe2b-a86e-4910-ab16-65c853cd9c73", new DateTime(2024, 3, 21, 19, 36, 8, 660, DateTimeKind.Local).AddTicks(2716), "AQAAAAEAACcQAAAAEFjw1S3f+Br3TPgcRtmPnzAQx94SByVUBigJ0zdcap1AKrFcICOvCijNM8Hv/qE5dQ==", "8aa45bdf-fcef-4365-8f7b-030d1ad72e58" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 21, 19, 36, 8, 662, DateTimeKind.Local).AddTicks(3745));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 21, 19, 36, 8, 662, DateTimeKind.Local).AddTicks(3755));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 21, 19, 36, 8, 662, DateTimeKind.Local).AddTicks(3757));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 21, 19, 36, 8, 662, DateTimeKind.Local).AddTicks(3758));
        }
    }
}
