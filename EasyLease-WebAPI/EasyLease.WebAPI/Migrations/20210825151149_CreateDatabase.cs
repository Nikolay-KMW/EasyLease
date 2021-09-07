using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EasyLease.WebAPI.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvertType",
                columns: table => new
                {
                    AdvertTypeId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertType", x => x.AdvertTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    SecondName = table.Column<string>(maxLength: 50, nullable: true),
                    ThirdName = table.Column<string>(maxLength: 50, nullable: true),
                    Biography = table.Column<string>(maxLength: 500, nullable: true),
                    Avatar = table.Column<byte[]>(maxLength: 126976, nullable: true),
                    CreatedUser = table.Column<DateTime>(nullable: false),
                    UpdatedUser = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comforts",
                columns: table => new
                {
                    ComfortId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comforts", x => x.ComfortId);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Region = table.Column<string>(maxLength: 50, nullable: false),
                    District = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => new { x.Region, x.District });
                });

            migrationBuilder.CreateTable(
                name: "SettlementType",
                columns: table => new
                {
                    SettlementTypeId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementType", x => x.SettlementTypeId);
                });

            migrationBuilder.CreateTable(
                name: "StreetType",
                columns: table => new
                {
                    StreetTypeId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreetType", x => x.StreetTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
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
                name: "Adverts",
                columns: table => new
                {
                    AdvertId = table.Column<Guid>(nullable: false),
                    AdvertTypeId = table.Column<string>(nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    NumberOfRooms = table.Column<int>(nullable: false),
                    Area = table.Column<int>(nullable: false),
                    Storey = table.Column<int>(nullable: true),
                    NumberOfStoreys = table.Column<int>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    SettlementTypeId = table.Column<string>(nullable: false),
                    SettlementName = table.Column<string>(maxLength: 100, nullable: false),
                    StreetTypeId = table.Column<string>(nullable: false),
                    StreetName = table.Column<string>(maxLength: 150, nullable: false),
                    HouseNumber = table.Column<string>(maxLength: 50, nullable: true),
                    ApartmentNumber = table.Column<int>(nullable: true),
                    Status = table.Column<string>(type: "varchar(24)", nullable: false),
                    PriceType = table.Column<string>(type: "varchar(24)", nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    StartOfLease = table.Column<DateTime>(nullable: false),
                    EndOfLease = table.Column<DateTime>(nullable: true),
                    CreatedAd = table.Column<DateTime>(nullable: false),
                    UpdatedAd = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adverts", x => x.AdvertId);
                    table.ForeignKey(
                        name: "FK_Adverts_AdvertType_AdvertTypeId",
                        column: x => x.AdvertTypeId,
                        principalTable: "AdvertType",
                        principalColumn: "AdvertTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adverts_SettlementType_SettlementTypeId",
                        column: x => x.SettlementTypeId,
                        principalTable: "SettlementType",
                        principalColumn: "SettlementTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adverts_StreetType_StreetTypeId",
                        column: x => x.StreetTypeId,
                        principalTable: "StreetType",
                        principalColumn: "StreetTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adverts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adverts_Location_Region_District",
                        columns: x => new { x.Region, x.District },
                        principalTable: "Location",
                        principalColumns: new[] { "Region", "District" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertComfort",
                columns: table => new
                {
                    AdvertId = table.Column<Guid>(nullable: false),
                    ComfortId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertComfort", x => new { x.AdvertId, x.ComfortId });
                    table.ForeignKey(
                        name: "FK_AdvertComfort_Adverts_AdvertId",
                        column: x => x.AdvertId,
                        principalTable: "Adverts",
                        principalColumn: "AdvertId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertComfort_Comforts_ComfortId",
                        column: x => x.ComfortId,
                        principalTable: "Comforts",
                        principalColumn: "ComfortId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertTag",
                columns: table => new
                {
                    AdvertId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<string>(nullable: false),
                    CreatedTag = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertTag", x => new { x.AdvertId, x.TagId });
                    table.ForeignKey(
                        name: "FK_AdvertTag_Adverts_AdvertId",
                        column: x => x.AdvertId,
                        principalTable: "Adverts",
                        principalColumn: "AdvertId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteAdvert",
                columns: table => new
                {
                    AdvertId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteAdvert", x => new { x.AdvertId, x.UserId });
                    table.ForeignKey(
                        name: "FK_FavoriteAdvert_Adverts_AdvertId",
                        column: x => x.AdvertId,
                        principalTable: "Adverts",
                        principalColumn: "AdvertId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteAdvert_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Path = table.Column<string>(maxLength: 200, nullable: false),
                    AdvertId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Image_Adverts_AdvertId",
                        column: x => x.AdvertId,
                        principalTable: "Adverts",
                        principalColumn: "AdvertId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AdvertType",
                column: "AdvertTypeId",
                values: new object[]
                {
                    "дом",
                    "квартира",
                    "комната"
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6d9b7113-a8f8-6035-99a7-a20dd400f6a3"), "037fea62-20d5-4047-90dc-1dd3e2f401b6", "User", "USER" },
                    { new Guid("48a4210f-3ce5-48ba-9461-80283ed1d94d"), "6e814029-953a-4dba-a872-0e96774bb008", "Moderator", "MODERATOR" },
                    { new Guid("7771d881-221a-1e7d-b208-0118dcc088e1"), "5101130a-ccae-456b-a113-506e24ee4b36", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "Biography", "ConcurrencyStamp", "CreatedUser", "Email", "EmailConfirmed", "FirstName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecondName", "SecurityStamp", "ThirdName", "TwoFactorEnabled", "UpdatedUser", "UserName" },
                values: new object[,]
                {
                    { new Guid("7778a881-221a-1e7d-a208-0118ccc088e7"), 0, null, "", "0ff9c218-f680-4b8b-b0af-2a35f8f0e43b", new DateTime(2021, 8, 25, 15, 11, 49, 14, DateTimeKind.Utc).AddTicks(839), "ELAdmin@com.ua", true, "Master", false, null, null, "MASTERADMIN", "AQAAAAEAACcQAAAAECyJlu13nnaSmESQfSieenqVXXfZ3/YLhw2b+mEgcIYTXwjtbsvyQ8XxXQBWUPGDXA==", null, false, "Admin", "fbb386af-9068-410d-9654-58a4ca31a783", "", false, null, "MasterAdmin" },
                    { new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870"), 0, null, "Просто собственник", "1d1dc9fe-7633-45bb-af9a-c04fc154f3d3", new DateTime(2021, 8, 26, 15, 11, 49, 42, DateTimeKind.Utc).AddTicks(855), "max@com.ua", false, "Максим", false, null, null, null, null, null, false, "", null, "", false, null, null },
                    { new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"), 0, null, "", "ad34f344-efcd-4dbc-b3c2-26e328fd858d", new DateTime(2021, 8, 25, 15, 11, 49, 42, DateTimeKind.Utc).AddTicks(855), "Nik@com.ua", false, "Nik", false, null, null, null, null, null, false, null, null, null, false, null, null },
                    { new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"), 0, null, "Занимаюсь сдачей недвижимости", "3d5c7d92-6951-4093-ad45-97e98c9dba01", new DateTime(2021, 8, 25, 18, 11, 49, 42, DateTimeKind.Utc).AddTicks(855), "vlad@com.ua", false, "Влад", false, null, null, null, null, null, false, null, null, null, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Comforts",
                column: "ComfortId",
                values: new object[]
                {
                    "Телевизор",
                    "ПК",
                    "Холодильник",
                    "Электроплита",
                    "Сковородки",
                    "Кастрюли",
                    "Центральное отопление",
                    "Посудомоющая машина",
                    "Сушитель белья",
                    "Мягкая мебель",
                    "Кухонная мебель",
                    "Кровать",
                    "Кондиционер",
                    "Морозилка",
                    "Диван",
                    "Стиральная машина",
                    "Wi-Fi",
                    "Микроволновая печь",
                    "Посуда и столовые приборы",
                    "Духовка",
                    "Плита",
                    "Фен",
                    "Утюг",
                    "Переносный обогревавтель",
                    "Кабельное ТВ",
                    "Шкаф",
                    "Чайник",
                    "Кофеварка"
                });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Region", "District" },
                values: new object[,]
                {
                    { "Сумская", "Ахтырский" },
                    { "Ровенская", "Ровенский" },
                    { "Ровенская", "Сарненский" },
                    { "Сумская", "Конотопский" },
                    { "Сумская", "Роменский" },
                    { "Харьковская", "Изюмский" },
                    { "Сумская", "Шосткинский" },
                    { "Тернопольская", "Кременецкий" },
                    { "Тернопольская", "Тернопольский" },
                    { "Тернопольская", "Чертковский" },
                    { "Харьковская", "Богодуховский" },
                    { "Сумская", "Сумской" },
                    { "Ровенская", "Дубенский" },
                    { "Одесская", "Подольский" },
                    { "Полтавская", "Полтавский" },
                    { "Полтавская", "Миргородский" },
                    { "Полтавская", "Лубенский" },
                    { "Полтавская", "Кременчугский" },
                    { "Одесская", "Раздельнянский" },
                    { "Одесская", "Одесский" },
                    { "Одесская", "Измаильский" },
                    { "Одесская", "Болградский" },
                    { "Одесская", "Белгород-Днестровский" },
                    { "Одесская", "Березовский" },
                    { "Николаевская", "Первомайский" },
                    { "Николаевская", "Николаевский" },
                    { "Николаевская", "Вознесенский" },
                    { "Харьковская", "Красноградский" },
                    { "Ровенская", "Вараский" },
                    { "Харьковская", "Купянский" },
                    { "Черновицкая", "Вижницкий" },
                    { "Харьковская", "Харьковский" },
                    { "Автономная республика Крым", "Ялтинский" },
                    { "Автономная республика Крым", "Феодосийский" },
                    { "Автономная республика Крым", "Симферопольский" },
                    { "Автономная республика Крым", "Перекопский" },
                    { "Автономная республика Крым", "Курманский" },
                    { "Автономная республика Крым", "Керченский" },
                    { "Автономная республика Крым", "Евпаторийский" },
                    { "Автономная республика Крым", "Джанкойский" },
                    { "Автономная республика Крым", "Белогорский" },
                    { "Автономная республика Крым", "Бахчисарайский" },
                    { "Черниговская", "Черниговский" },
                    { "Черниговская", "Прилуцкий" },
                    { "Черниговская", "Новгород-Северский" },
                    { "Черниговская", "Нежинский" },
                    { "Харьковская", "Лозовский" },
                    { "Черниговская", "Корюковский" },
                    { "Черновицкая", "Днестровский" },
                    { "Черкасская", "Черкасский" },
                    { "Черкасская", "Уманский" },
                    { "Черкасская", "Золотоношский" },
                    { "Черкасская", "Звенигородский" },
                    { "Хмельницкая", "Шепетовский" },
                    { "Хмельницкая", "Хмельницкий" },
                    { "Хмельницкая", "Каменец-Подольский" },
                    { "Херсонская", "Херсонский" },
                    { "Херсонская", "Скадовский" },
                    { "Херсонская", "Каховский" },
                    { "Херсонская", "Генический" },
                    { "Херсонская", "Бериславский" },
                    { "Харьковская", "Чугуевский" },
                    { "Черновицкая", "Черновицкий" },
                    { "Львовская", "Яворовский" },
                    { "Николаевская", "Баштанский" },
                    { "Львовская", "Самборский" },
                    { "Донецкая", "Волновахский" },
                    { "Львовская", "Червоноградский" },
                    { "Донецкая", "Донецкий" },
                    { "Донецкая", "Кальмиусский" },
                    { "Донецкая", "Краматорский" },
                    { "Донецкая", "Мариупольский" },
                    { "Донецкая", "Бахмутский" },
                    { "Донецкая", "Покровский" },
                    { "Житомирская", "Житомирский" },
                    { "Житомирская", "Коростенский" },
                    { "Житомирская", "Новоград-Волынский" },
                    { "Закарпатская", "Береговский" },
                    { "Закарпатская", "Мукачевский" },
                    { "Закарпатская", "Раховский" },
                    { "Житомирская", "Бердичевский" },
                    { "Закарпатская", "Тячевский" },
                    { "Днепропетровская", "Синельниковский" },
                    { "Днепропетровская", "Новомосковский" },
                    { "Винницкая", "Винницкий" },
                    { "Винницкая", "Гайсинский" },
                    { "Винницкая", "Жмеринский" },
                    { "Винницкая", "Могилев-Подольский" },
                    { "Винницкая", "Тульчинский" },
                    { "Винницкая", "Хмельницкий" },
                    { "Днепропетровская", "Павлоградский" },
                    { "Волынская", "Владимир-Волынский" },
                    { "Волынская", "Ковальский" },
                    { "Волынская", "Луцкий" },
                    { "Днепропетровская", "Днепровский" },
                    { "Днепропетровская", "Каменский" },
                    { "Днепропетровская", "Криворожский" },
                    { "Днепропетровская", "Никопольский" },
                    { "Волынская", "Камень-Каширский" },
                    { "Закарпатская", "Ужгородский" },
                    { "Донецкая", "Горловский" },
                    { "Запорожская", "Бердянский" },
                    { "Кировоградская", "Новоукраинский" },
                    { "Кировоградская", "Александрийский" },
                    { "Луганская", "Алчевский" },
                    { "Луганская", "Довжанский" },
                    { "Луганская", "Луганский" },
                    { "Луганская", "Ровеньковский" },
                    { "Кировоградская", "Кропивницкий" },
                    { "Луганская", "Сватовский" },
                    { "Закарпатская", "Хустский" },
                    { "Луганская", "Старобельский" },
                    { "Луганская", "Счастьенский" },
                    { "Львовская", "Дрогобычский" },
                    { "Львовская", "Золочевский" },
                    { "Львовская", "Львовский" },
                    { "Луганская", "Северодонецкий" },
                    { "Кировоградская", "Головановский" },
                    { "Львовская", "Стрыйский " },
                    { "Киевская", "Обуховский" },
                    { "Запорожская", "Васильевский" },
                    { "Ивано-Франковская", "Ивано-Франковский" },
                    { "Запорожская", "Пологовский" },
                    { "Ивано-Франковская", "Верховинский" },
                    { "Киевская", "Фастовский" },
                    { "Ивано-Франковская", "Калушский" },
                    { "Запорожская", "Запорожский" },
                    { "Ивано-Франковская", "Коломыйский" },
                    { "Ивано-Франковская", "Надворнянский" },
                    { "Киевская", "Белоцерковский" },
                    { "Киевская", "Бориспольский" },
                    { "Киевская", "Броварской" },
                    { "Киевская", "Бучанский" },
                    { "Киевская", "Вышгородский" },
                    { "Ивано-Франковская", "Косовский" },
                    { "Запорожская", "Мелитопольский" }
                });

            migrationBuilder.InsertData(
                table: "SettlementType",
                column: "SettlementTypeId",
                values: new object[]
                {
                    "город",
                    "смт",
                    "село"
                });

            migrationBuilder.InsertData(
                table: "StreetType",
                column: "StreetTypeId",
                values: new object[]
                {
                    "тупик",
                    "съезд",
                    "спуск",
                    "разъезд",
                    "проулок",
                    "переулок",
                    "проезд",
                    "аллея",
                    "бульвар",
                    "взвоз",
                    "въезд",
                    "заезд",
                    "проспект",
                    "улица",
                    "кольцо",
                    "магистраль",
                    "набережная",
                    "площадь"
                });

            migrationBuilder.InsertData(
                table: "Tags",
                column: "TagId",
                values: new object[]
                {
                    "1 комнатная",
                    "Евро ремонт",
                    "свежий ремонт",
                    "комната",
                    "квартира",
                    "дом",
                    "5 комнатная"
                });

            migrationBuilder.InsertData(
                table: "Adverts",
                columns: new[] { "AdvertId", "AdvertTypeId", "ApartmentNumber", "Area", "CreatedAd", "Description", "District", "EndOfLease", "HouseNumber", "NumberOfRooms", "NumberOfStoreys", "Price", "PriceType", "Region", "SettlementName", "SettlementTypeId", "StartOfLease", "Status", "Storey", "StreetName", "StreetTypeId", "Title", "UpdatedAd", "UserId" },
                values: new object[,]
                {
                    { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), "квартира", 5, 60, new DateTime(2021, 8, 26, 1, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), "Есть все необходимое", "Сумской", null, "10", 2, 9, 100m, "PricePerDay", "Сумская", "Сумы", "город", new DateTime(2021, 8, 26, 1, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), "Active", 3, "Соборна", "улица", "Сдам 2-х комнатную квартиру", null, new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870") },
                    { new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"), "дом", null, 280, new DateTime(2021, 8, 25, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), "Дом 280 м2. Элитный дизайн.", "Сумской", null, "8", 8, 2, 3000m, "PricePerMonth", "Сумская", "Сумы", "город", new DateTime(2021, 8, 25, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), "Active", null, "Мира", "улица", "Сдам дом", null, new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870") },
                    { new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"), "квартира", 13, 100, new DateTime(2021, 8, 25, 20, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), "Сделанный свежий ремонт", "Сумской", null, "2", 5, 5, 300m, "PricePerDay", "Сумская", "Сумы", "город", new DateTime(2021, 8, 25, 20, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), "Active", 5, "Супруна", "улица", "Сдам 5-х комнатную квартиру", null, new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870") },
                    { new Guid("a6d4c053-49b6-410c-bc78-2d54a9991870"), "квартира", 5, 70, new DateTime(2021, 8, 26, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), "Евро ремонт.", "Сумской", null, "2", 3, 9, 150m, "PricePerDay", "Сумская", "Сумы", "город", new DateTime(2021, 8, 26, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), "Active", 8, "Соборна", "улица", "Сдам 3-х комнатную квартиру", null, new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870") },
                    { new Guid("a7d4c053-49b6-410c-bc78-2d54a9991870"), "квартира", 7, 85, new DateTime(2021, 8, 27, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), "Сделан культурный ремонт", "Новгород-Северский", null, "1", 4, 9, 300m, "PricePerDay", "Черниговская", "Новгород", "город", new DateTime(2021, 8, 27, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), "Active", 2, "Победы", "улица", "Сдам 4-х комнатную квартиру", null, new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870") },
                    { new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"), "квартира", 7, 45, new DateTime(2021, 8, 25, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), "Есть все кроме холодильника", "Прилуцкий", null, "25", 1, 9, 50m, "PricePerDay", "Черниговская", "Прилуки", "город", new DateTime(2021, 8, 25, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), "Active", 7, "Вовка", "улица", "Сдам 1 комнатную квартиру", null, new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870") },
                    { new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"), "комната", 5, 25, new DateTime(2021, 8, 28, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), "Все уютненько.", "Сумской", null, "31", 1, 5, 500m, "PricePerMonth", "Сумская", "Сумы", "город", new DateTime(2021, 8, 28, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), "Active", 3, "Стрелки", "набережная", "Сдам комнату в общежитие", null, new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870") }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870"), new Guid("6d9b7113-a8f8-6035-99a7-a20dd400f6a3") },
                    { new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"), new Guid("6d9b7113-a8f8-6035-99a7-a20dd400f6a3") },
                    { new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"), new Guid("6d9b7113-a8f8-6035-99a7-a20dd400f6a3") },
                    { new Guid("7778a881-221a-1e7d-a208-0118ccc088e7"), new Guid("7771d881-221a-1e7d-b208-0118dcc088e1") }
                });

            migrationBuilder.InsertData(
                table: "AdvertComfort",
                columns: new[] { "AdvertId", "ComfortId" },
                values: new object[,]
                {
                    { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), "Холодильник" },
                    { new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"), "Чайник" },
                    { new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"), "Диван" },
                    { new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"), "Кастрюли" },
                    { new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"), "Wi-Fi" },
                    { new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"), "Мягкая мебель" },
                    { new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"), "Кофеварка" },
                    { new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"), "Духовка" },
                    { new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"), "Кухонная мебель" },
                    { new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"), "Холодильник" },
                    { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), "Телевизор" },
                    { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), "Кондиционер" },
                    { new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"), "Посуда и столовые приборы" }
                });

            migrationBuilder.InsertData(
                table: "AdvertTag",
                columns: new[] { "AdvertId", "TagId", "CreatedTag" },
                values: new object[,]
                {
                    { new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"), "дом", new DateTime(2021, 8, 25, 15, 11, 49, 49, DateTimeKind.Utc).AddTicks(859) },
                    { new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"), "комната", new DateTime(2021, 8, 25, 15, 11, 49, 49, DateTimeKind.Utc).AddTicks(859) },
                    { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), "Евро ремонт", new DateTime(2021, 8, 25, 15, 11, 49, 49, DateTimeKind.Utc).AddTicks(859) },
                    { new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"), "5 комнатная", new DateTime(2021, 8, 25, 15, 11, 49, 49, DateTimeKind.Utc).AddTicks(859) },
                    { new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"), "свежий ремонт", new DateTime(2021, 8, 25, 15, 11, 49, 49, DateTimeKind.Utc).AddTicks(859) },
                    { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), "квартира", new DateTime(2021, 8, 25, 15, 11, 49, 49, DateTimeKind.Utc).AddTicks(859) },
                    { new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"), "1 комнатная", new DateTime(2021, 8, 25, 15, 11, 49, 49, DateTimeKind.Utc).AddTicks(859) }
                });

            migrationBuilder.InsertData(
                table: "FavoriteAdvert",
                columns: new[] { "AdvertId", "UserId" },
                values: new object[,]
                {
                    { new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"), new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870") },
                    { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870") },
                    { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870") },
                    { new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"), new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870") },
                    { new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"), new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertComfort_ComfortId",
                table: "AdvertComfort",
                column: "ComfortId");

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_AdvertTypeId",
                table: "Adverts",
                column: "AdvertTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_SettlementTypeId",
                table: "Adverts",
                column: "SettlementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_StreetTypeId",
                table: "Adverts",
                column: "StreetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_UserId",
                table: "Adverts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_Region_District",
                table: "Adverts",
                columns: new[] { "Region", "District" });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertTag_TagId",
                table: "AdvertTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteAdvert_UserId",
                table: "FavoriteAdvert",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_AdvertId",
                table: "Image",
                column: "AdvertId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertComfort");

            migrationBuilder.DropTable(
                name: "AdvertTag");

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
                name: "FavoriteAdvert");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Comforts");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Adverts");

            migrationBuilder.DropTable(
                name: "AdvertType");

            migrationBuilder.DropTable(
                name: "SettlementType");

            migrationBuilder.DropTable(
                name: "StreetType");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
