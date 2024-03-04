using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeStockWebProject.Infrastructure.Migrations
{
    public partial class I : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePicturePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoinTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 2, 25, 21, 46, 59, 259, DateTimeKind.Local).AddTicks(892)),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ReleaseDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrintType = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    platformType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Illustrator = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    BookTypeId = table.Column<int>(type: "int", nullable: false),
                    ReleaseDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pages = table.Column<int>(type: "int", maxLength: 10000, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PrintType = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsFavorite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_BookTypes_BookTypeId",
                        column: x => x.BookTypeId,
                        principalTable: "BookTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameGenres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenres", x => new { x.GameId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_GameGenres_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GameGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookTags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTags", x => new { x.BookId, x.TagId });
                    table.ForeignKey(
                        name: "FK_BookTags_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FavoriteProducts",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteProducts", x => new { x.UserId, x.BookId, x.GameId });
                    table.ForeignKey(
                        name: "FK_FavoriteProducts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FavoriteProducts_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FavoriteProducts_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: true),
                    GameId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pictures_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pictures_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "JoinTime", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicturePath", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("b9a4d407-7518-4aea-a72d-b94c7e389b70"), 0, "cea23ad4-6d07-4b9a-9dc5-c03a4df863a6", "testuser123@gmail.com", false, null, new DateTime(2024, 2, 25, 21, 46, 59, 257, DateTimeKind.Local).AddTicks(3109), false, null, "TESTUSER123@GMAIL.COM", "TEST USER", "AQAAAAEAACcQAAAAELglxkQra5sJq6WgR1OgnkOJBE1P/PnTwJsacwm/s76tUIKO33Zg4LOrWCHGLyAlBg==", null, false, null, "88493d4c-779f-4c71-a32c-c896bad68a3f", false, "Test User" },
                    { new Guid("ee8ddd02-ce94-4f77-8608-819b08dbbb32"), 0, "d485997e-eedb-4400-b4bc-ce42df688354", "admin123@gmail.com", false, null, new DateTime(2024, 2, 25, 21, 46, 59, 257, DateTimeKind.Local).AddTicks(3122), false, null, "ADMIN123@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEFNT9NP0KkR/RgpL6irF5MVHVkXWA6lQUZzmz5Sv4mfvtbi9h4V7jbN2kKQwZ/bS6g==", null, false, null, "b0ed4b88-9c72-44b3-be97-90793c0c1a02", false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "BookTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Light Novel" },
                    { 2, "Manga" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "Developer", "Name", "Price", "PrintType", "Publisher", "Quantity", "ReleaseDate", "platformType" },
                values: new object[,]
                {
                    { 1, "This story takes place three years after the events of the previous game “Atelier Ryza: Ever Darkness & the Secret Hideout,” and depicts the reunion of Ryza and her friends, who go through new encounters and goodbyes to discover a true priceless treasure.", "KOEI TECMO GAMES CO., LTD.", "Atelier Ryza 2: Lost Legends & the Secret Fairy", 59.99m, 0, "KOEI TECMO GAMES CO., LTD.", 0, "Jan 26, 2021", 2 },
                    { 2, "STEINS;GATE follows a rag-tag band of tech-savvy young students who discover the means of changing the past via mail, using a modified microwave. Their experiments into how far they can go with their discovery begin to spiral out of control as they become entangled in a conspiracy surrounding SERN, the organisation behind the Large Hadron Collider, and John Titor who claims to be from a dystopian future.", "MAGES. Inc.", "STEINS;GATE", 26.99m, 0, "Spike Chunsoft Co., Ltd.", 0, "Sep 9, 2016", 2 },
                    { 3, "Final FANTASY VII remake intergrade is an enhanced and expanded version of the critically acclaimed and award-winning final FANTASY VII remake for the PlayStation 5. Final FANTASY VII remake intergrade comes bundled with the brand-new episode featuring yuffie as the main character which introduces an exhilarating new story Arc, and numerous gameplay additions for players to enjoy. The world has fallen under the control of the Shinra electric power company, a shadowy Corporation controlling the planet's very life force as Mako energy. In the sprawling city of Midgar, an anti-shinra organization calling themselves Avalanche have stepped up their resistance. Cloud strife, a former member of shinra's Elite soldier unit now turned mercenary, lends his aid to the group, Unaware of the epic consequences that await him. The new episode featuring yuffie is a brand-new adventure in the world of FINAL FANTASY VII remake intergrade. Play as wutai Ninja yuffie kisaragi as she infiltrates midgar and conspiracies with Avalanche HQ to steal the ultimate materia from the Shinra electric power company. Play alongside new characters and enjoy an expanded gameplay experience featuring multiple new combat and gameplay additions. This adventure brings new perspective to the final FANTASY VII remake story that cannot be missed.", "Square Enix", "FINAL FANTASY VII REMAKE INTERGRADE", 35.99m, 1, "Square Enix", 0, "March 1, 2021", 0 },
                    { 4, "The Little Witch Nobeta is a 3D action shooting game. Players will explore ancient, unknown castles and use different magic elements to fight against the soul!\r\nThe game uses a comfortable Japanese art style, but the battles are quite challenging despite its cute looks. Underestimating your foes will lead to troublesome encounters. You must discover enemies' weaknesses and learn the precise time to dodge attacks in order to gain the advantage in combat.", "Pupuya Games, SimonCreative, Justdan", "Little Witch Nobeta", 34.00m, 1, "Pupuya Games", 0, "November 2, 2022", 3 }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "Visual Novel" },
                    { 2, false, "Dating Sim" },
                    { 3, false, "JRPG" },
                    { 4, false, "Puzzle" },
                    { 5, false, "Party-Based RPG" },
                    { 6, false, "Interactive Fiction" },
                    { 7, false, "Action" },
                    { 8, false, "Story Rich" },
                    { 9, false, "RPG" },
                    { 10, false, "Adventure" },
                    { 11, false, "Psychological Horror" },
                    { 12, false, "Casual" },
                    { 13, false, "Multiplayer" },
                    { 14, false, "Singleplayer" },
                    { 15, false, "Open World" },
                    { 16, false, "Fantasy" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "Action" },
                    { 2, false, "Adventure" },
                    { 3, false, "Comedy" },
                    { 4, false, "Drama" },
                    { 5, false, "Ecchi" },
                    { 6, false, "Fantasy" },
                    { 7, false, "Harem" },
                    { 8, false, "Historical" },
                    { 9, false, "Horror" },
                    { 10, false, "Martial Arts" },
                    { 11, false, "Mecha" },
                    { 12, false, "Mystery" },
                    { 13, false, "Psychological" },
                    { 14, false, "Romance" },
                    { 15, false, "School Life" },
                    { 16, false, "Sci-fi" },
                    { 17, false, "Supernatural" },
                    { 18, false, "Sports" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { 19, false, "Slice of Life" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { 20, false, "Shounen" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { 21, false, "Isekai" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "BookTypeId", "Description", "Illustrator", "IsDeleted", "IsFavorite", "Pages", "Price", "PrintType", "Publisher", "Quantity", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, "Koushi Tachibana", 1, "April 10. The first day of school. Shido Itsuka is rudely awoken by his personal alarm clock—his little sister. It’s shaping up to be another typical day…well, as typical as it gets on a planet plagued by massive spatial quakes. Little does Shido know, however, his life is about to take a sudden turn when he encounters the source of this destructive phenomenon—a girl his age, apparently known as a Spirit. Turns out, there are only two ways he can stop her from unleashing hell on the world: Eliminate her by force or placate her…by taking her out on a date and making her fall in love with him!", "Tsunako", false, false, 176, 12.23m, 1, "Yen On", 7, "March 23, 2021", "Date A Live, Vol. 1" },
                    { 2, "Koushi Tachibana", 1, "Things have only gotten stranger for Shido ever since Tohka transferred to his school. These days, his life teeters between heaven and hell, and the forecast today points toward the latter. He’s already been caught in the line of fire between Tohka and Origami, and a sudden downpour leaves him sopping wet. Just his luck. Lately, it seems the only place he can catch a break is in the comfort of his own home. That’s all about to change, however, when a new “training” regimen calls for him to live with Tohka—and while Shido is at his most vulnerable, a second Spirit emerges from the storm…", "Tsunako", false, false, 184, 11.34m, 1, "Yen On", 35, "May 25, 2021", "Date A Live, Vol. 2" },
                    { 3, "Yuri Kitayama", 1, "Rio and Sora journey to the site of Saint Erica’s summoning to find clues about the transcendent rules. As Rio walks in the Saint’s footsteps, more questions arise about the power that the heroes wield.\r\n\r\nMeanwhile, the four heroes gather at the Galarc Castle, but Sendo Takahisa refuses to train with the others and sets out on his own lonely path to repair his relationship with Miharu. After all, she’s forgiven Aki... So why not him? Will the actions of one pining hero distort the very fabric of the world itself?", "Riv", false, false, 223, 6.54m, 0, "J-Novel Club", 0, "September 15, 2023", "Seirei Gensouki: Spirit Chronicles Volume 23" },
                    { 4, "Rifujin na Magonote", 1, "Kicked out by his family and wandering the streets, an unemployed 34-year-old shut-in thinks he’s hit rock-bottom—just as he’s hit and killed by a speeding truck! Awakening to find himself reborn as an infant in a world of swords and sorcery, but with the memories of his first life intact, Rudeus Greyrat is determined not to repeat his past mistakes. He’s going to make the most of this reincarnation as he sets off on the adventure of a second lifetime!", "Shirotaka", false, false, 266, 7.12m, 0, "Seven Seas Entertainment, Seven Seas Siren", 0, "April 4, 2019", "Mushoku Tensei: Jobless Reincarnation, Vol. 1" },
                    { 5, "Rifujin Na Magonote", 2, "An unemployed otaku has just reached the lowest point in his life. He wants nothing more than the ability to start over, but just as he thinks it may be possible...he gets hit by a truck and dies! Shockingly, he finds himself reborn into an infant’s body in a strange new world of swords and magic. His identity now is Rudeus Greyrat, yet he still retains the memories of his previous life. Reborn into a new family, Rudeus makes use of his past experiences to forge ahead in this fantasy world as a true prodigy, gifted with maturity beyond his years and a natural born talent for magic. With swords instead of chopsticks, and spell books instead of the internet, can Rudeus redeem himself in this wondrous yet dangerous land?", "Fujikawa Yuka", false, false, 180, 10.23m, 1, "Seven Seas", 13, "November 24, 2015", "Mushoku Tensei: Jobless Reincarnation, Vol. 1" },
                    { 6, "Tatsuki Fujimoto", 2, "Denji was a small-time devil hunter just trying to survive in a harsh world. After being killed on a job, he is revived by his pet devil Pochita and becomes something new and dangerous—Chainsaw Man! Denji is desperate to tell the world that he’s Chainsaw Man, but is he competent enough to pull off a proper reveal? Meanwhile, Asa has made a friend! But this new friendship may be hiding a dark secret.", "Tatsuki Fujimoto", false, false, 184, 9.76m, 1, "VIZ Media LLC", 53, "December 5, 2023", "Chainsaw Man, Vol. 13" }
                });

            migrationBuilder.InsertData(
                table: "GameGenres",
                columns: new[] { "GameId", "GenreId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 1, 5 },
                    { 1, 8 },
                    { 1, 10 },
                    { 1, 15 },
                    { 2, 1 },
                    { 2, 8 },
                    { 2, 11 },
                    { 3, 3 },
                    { 3, 5 },
                    { 3, 7 },
                    { 3, 8 },
                    { 3, 10 },
                    { 3, 16 },
                    { 4, 7 },
                    { 4, 10 },
                    { 4, 14 },
                    { 4, 16 }
                });

            migrationBuilder.InsertData(
                table: "Pictures",
                columns: new[] { "Id", "BookId", "GameId", "Path" },
                values: new object[,]
                {
                    { 18, null, 1, "/img/Games/Atelier Ryza 2/MV5BMTAxMDQzNDMtMjFkMC00NWNmLWE0NjItZDdkMWNjYmZkNjhlXkEyXkFqcGdeQXVyNzgxNDk0NTI@._V1_.jpg" },
                    { 19, null, 1, "/img/Games/Atelier Ryza 2/ss_38608a834caa5243f66945693ccc1eddee35942b.1920x1080.jpg" },
                    { 20, null, 1, "/img/Games/Atelier Ryza 2/ss_a0d278df03b540c0f21d2cae130ca30b296353ca.1920x1080.jpg" },
                    { 21, null, 1, "/img/Games/Atelier Ryza 2/ss_e6b1fd3eb46f1ea035b758c991bda2f5e15673c4.1920x1080.jpg" },
                    { 22, null, 2, "/img/Games/STEINS GATE/2611526-untitled.png" },
                    { 23, null, 2, "/img/Games/ss_18e33276a72437c7728c969fe079e7bda6b7fb08.1920x1080.png" },
                    { 24, null, 2, "/img/Games/STEINS GATE/ss_48523dbba66bc7ea79c8bc46eabaee6190e005e9.1920x1080.png" },
                    { 25, null, 2, "/img/Games/STEINS GATE/ss_56d0a07b2ca57c72eb9c74ca918d49e3cf7a06a6.1920x1080.png" },
                    { 26, null, 3, "/img/Games/FINAL FANTASY VII REMAKE INTERGRADE/71d7tEoKY6L._SL1500_.jpg" },
                    { 27, null, 3, "/img/Games/FINAL FANTASY VII REMAKE INTERGRADE/71SUTICB69L._SL1500_.jpg" },
                    { 28, null, 3, "/img/Games/FINAL FANTASY VII REMAKE INTERGRADE/81VGg56E0zL._SL1500_.jpg" },
                    { 29, null, 3, "/img/Games/FINAL FANTASY VII REMAKE INTERGRADE/81W8CAno24L._SL1500_.jpg" },
                    { 30, null, 4, "/img/Games/Little Witch Nobeta/71+q7XyABPL._AC_SL1500_.jpg" },
                    { 31, null, 4, "/img/Games/Little Witch Nobeta/71rB0zyXdVL._AC_SL1500_.jpg" },
                    { 32, null, 4, "/img/Games/Little Witch Nobeta/71zb3knEnaL._AC_SL1500_.jpg" },
                    { 33, null, 4, "/img/Games/Little Witch Nobeta/91f4gT-pGyL._AC_SL1500_.jpg" }
                });

            migrationBuilder.InsertData(
                table: "BookTags",
                columns: new[] { "BookId", "TagId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 7 },
                    { 1, 14 },
                    { 1, 15 },
                    { 2, 1 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 7 },
                    { 2, 14 },
                    { 2, 15 },
                    { 2, 16 },
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 6 },
                    { 3, 7 },
                    { 3, 14 },
                    { 3, 21 },
                    { 4, 1 },
                    { 4, 2 },
                    { 4, 5 },
                    { 4, 6 },
                    { 4, 7 },
                    { 4, 14 },
                    { 4, 21 },
                    { 5, 1 },
                    { 5, 2 },
                    { 5, 5 },
                    { 5, 6 },
                    { 5, 7 },
                    { 5, 14 },
                    { 5, 21 },
                    { 6, 1 },
                    { 6, 4 },
                    { 6, 9 },
                    { 6, 13 },
                    { 6, 15 },
                    { 6, 17 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "BookId", "CreatedDate", "Description", "GameId", "IsDeleted", "UserId", "UserName" },
                values: new object[] { 1, 1, new DateTime(2024, 2, 25, 21, 46, 59, 259, DateTimeKind.Local).AddTicks(4176), "Test Comment", 1, false, new Guid("b9a4d407-7518-4aea-a72d-b94c7e389b70"), "Test User" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "BookId", "CreatedDate", "Description", "GameId", "IsDeleted", "UserId", "UserName" },
                values: new object[,]
                {
                    { 2, 2, new DateTime(2024, 2, 25, 21, 46, 59, 259, DateTimeKind.Local).AddTicks(4189), "Test Comment", 2, false, new Guid("b9a4d407-7518-4aea-a72d-b94c7e389b70"), "Test User" },
                    { 3, 3, new DateTime(2024, 2, 25, 21, 46, 59, 259, DateTimeKind.Local).AddTicks(4191), "Test Comment", 3, false, new Guid("b9a4d407-7518-4aea-a72d-b94c7e389b70"), "Test User" },
                    { 4, 4, new DateTime(2024, 2, 25, 21, 46, 59, 259, DateTimeKind.Local).AddTicks(4192), "Test Comment", 4, false, new Guid("b9a4d407-7518-4aea-a72d-b94c7e389b70"), "Test User" }
                });

            migrationBuilder.InsertData(
                table: "Pictures",
                columns: new[] { "Id", "BookId", "GameId", "Path" },
                values: new object[,]
                {
                    { 1, 1, null, "/img/Books/Date a Live Vol 1(Light Novel)/picture1.png" },
                    { 2, 1, null, "/img/Books/Date a Live Vol 1(Light Novel)/picture2.png" },
                    { 3, 1, null, "/img/Books/Date a Live Vol 1(Light Novel)/picture3.png" },
                    { 4, 2, null, "/img/Books/Date a Live Vol 2(Light Novel)/Picture_1.png" },
                    { 5, 2, null, "/img/Books/Date a Live Vol 2(Light Novel)/Picture_2.png" },
                    { 6, 2, null, "/img/Books/Date a Live Vol 2(Light Novel)/Picture_3.png" },
                    { 7, 3, null, "/img/Books/Spirit Chronicles Vol 23(Light Nove)/picture1.png" },
                    { 8, 3, null, "/img/Books/Spirit Chronicles Vol 23(Light Nove)/picture2.png" },
                    { 9, 3, null, "/img/Books/Spirit Chronicles Vol 23(Light Nove)/picture3.png" },
                    { 10, 4, null, "/img/Books/Mushoku Tensei Vol 1 (Light Novel)/picture 1.png" },
                    { 11, 4, null, "/img/Books/Mushoku Tensei Vol 1 (Light Novel)/picture 2.png" },
                    { 12, 4, null, "/img/Books/Mushoku Tensei Vol 1 (Light Novel)/picture 3.png" },
                    { 13, 5, null, "/img/Books/Mushoku Tensei Vol 1 (Manga)/81vgliRXgRL._SL1500_.jpg" },
                    { 14, 5, null, "/img/Books/Mushoku Tensei Vol 1 (Manga)/91yoqb7q5nL._SL1500_.jpg" },
                    { 15, 5, null, "/img/Books/Mushoku Tensei Vol 1 (Manga)/91yoqb7q5nL._SL1500_.jpg" },
                    { 16, 6, null, "/img/Books/Chainsaw Man, Vol. 13(Manga)/8194kTgN5iL._SL1500_.jpg" },
                    { 17, 6, null, "/img/Books/Chainsaw Man, Vol. 13(Manga)/81WO4SsaNzL._SL1500_.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookTypeId",
                table: "Books",
                column: "BookTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTags_TagId",
                table: "BookTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BookId",
                table: "Comments",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_GameId",
                table: "Comments",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProducts_BookId",
                table: "FavoriteProducts",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProducts_GameId",
                table: "FavoriteProducts",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenres_GenreId",
                table: "GameGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BookId",
                table: "Orders",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_GameId",
                table: "Orders",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_BookId",
                table: "Pictures",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_GameId",
                table: "Pictures",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BookTags");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FavoriteProducts");

            migrationBuilder.DropTable(
                name: "GameGenres");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "BookTypes");
        }
    }
}
