using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeStockWebProject.Infrastructure.Migrations
{
    public partial class update_book_class : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 21, 19, 36, 8, 662, DateTimeKind.Local).AddTicks(454),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 20, 1, 26, 2, 115, DateTimeKind.Local).AddTicks(2681));

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
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "FilePath",
                value: "/Books/Date A Live/Date A Live, Vol. 1_ Dead-End Tohka.pdf");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "FilePath",
                value: "/Books/Date A Live/Date A Live, Vol. 2_ Puppet Yoshino.pdf");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "FilePath",
                value: "/Books/Seirei Gensouki Spirit Chronicles/Seirei Gensouki_ Spirit Chronicles Volume 23.pdf");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "FilePath",
                value: "/Books/Mushoku Tensei/Mushoku Tensei_ Jobless Reincarnation (Light Novel) Vol. 1.pdf");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Books");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 20, 1, 26, 2, 115, DateTimeKind.Local).AddTicks(2681),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 21, 19, 36, 8, 662, DateTimeKind.Local).AddTicks(454));

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
        }
    }
}
