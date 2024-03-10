using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeStockWebProject.Infrastructure.Migrations
{
    public partial class updatedDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 10, 19, 50, 50, 336, DateTimeKind.Local).AddTicks(9153),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 9, 21, 4, 36, 820, DateTimeKind.Local).AddTicks(8040));

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
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "Path",
                value: "/img/Books/Date a Live Vol 1(Light Novel)/cover/picture1.png");

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 4,
                column: "Path",
                value: "/img/Books/Date a Live Vol 2(Light Novel)/cover/Picture_1.png");

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 7,
                column: "Path",
                value: "/img/Books/Spirit Chronicles Vol 23(Light Nove)/cover/picture1.png");

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 10,
                column: "Path",
                value: "/img/Books/Mushoku Tensei Vol 1 (Light Novel)/cover/picture 1.png");

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 13,
                column: "Path",
                value: "/img/Books/Mushoku Tensei Vol 1 (Manga)/cover/81vgliRXgRL._SL1500_.jpg");

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 17,
                column: "Path",
                value: "/img/Books/Chainsaw Man, Vol. 13(Manga)/cover/81WO4SsaNzL._SL1500_.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 9, 21, 4, 36, 820, DateTimeKind.Local).AddTicks(8040),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 10, 19, 50, 50, 336, DateTimeKind.Local).AddTicks(9153));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99e6e089-4c3c-4abc-a0cc-6b86b9129e12", new DateTime(2024, 3, 9, 21, 4, 36, 819, DateTimeKind.Local).AddTicks(398), "AQAAAAEAACcQAAAAEGXzgJ4dPnmMBUZ2NbDg6Qv2sk5G4SopmMiMIHX8EyIpx7BeETVpYQ2Ge9chj6o2gw==", "dd77235c-3abe-41ca-a7e2-dc52ea53a35c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ee8ddd02-ce94-4f77-8608-819b08dbbb32"),
                columns: new[] { "ConcurrencyStamp", "JoinTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e6ca96f9-9aaa-4600-8a08-0ec5c4094f11", new DateTime(2024, 3, 9, 21, 4, 36, 819, DateTimeKind.Local).AddTicks(412), "AQAAAAEAACcQAAAAECMnICNn3gwYEPhkwiAadJjioMTZLc1KUvzp1PPtebER8vHTl0co/EoNzsPC5kJ8Kg==", "fd8e8ce6-0ebe-4055-be95-a5a31cb5d2d1" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 9, 21, 4, 36, 821, DateTimeKind.Local).AddTicks(1669));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 9, 21, 4, 36, 821, DateTimeKind.Local).AddTicks(1679));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 9, 21, 4, 36, 821, DateTimeKind.Local).AddTicks(1681));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 9, 21, 4, 36, 821, DateTimeKind.Local).AddTicks(1683));

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "Path",
                value: "/img/Books/Date a Live Vol 1(Light Novel)/picture1.png");

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 4,
                column: "Path",
                value: "/img/Books/Date a Live Vol 2(Light Novel)/Picture_1.png");

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 7,
                column: "Path",
                value: "/img/Books/Spirit Chronicles Vol 23(Light Nove)/picture1.png");

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 10,
                column: "Path",
                value: "/img/Books/Mushoku Tensei Vol 1 (Light Novel)/picture 1.png");

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 13,
                column: "Path",
                value: "/img/Books/Mushoku Tensei Vol 1 (Manga)/81vgliRXgRL._SL1500_.jpg");

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 17,
                column: "Path",
                value: "/img/Books/Chainsaw Man, Vol. 13(Manga)/81WO4SsaNzL._SL1500_.jpg");
        }
    }
}
