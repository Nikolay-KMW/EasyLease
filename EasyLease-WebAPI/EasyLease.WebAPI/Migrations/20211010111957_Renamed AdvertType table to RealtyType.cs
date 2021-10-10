using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyLease.WebAPI.Migrations {
    public partial class RenamedAdvertTypetabletoRealtyType : Migration {
        // !!! DataBase corrected manually !!!!
        protected override void Up(MigrationBuilder migrationBuilder) {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_Adverts_AdvertType_AdvertTypeId",
            //     table: "Adverts");

            // migrationBuilder.DropTable(
            //     name: "AdvertType");

            // migrationBuilder.DropIndex(
            //     name: "IX_Adverts_AdvertTypeId",
            //     table: "Adverts");

            // migrationBuilder.DropColumn(
            //     name: "AdvertTypeId",
            //     table: "Adverts");

            // migrationBuilder.AlterColumn<string>(
            //     name: "Description",
            //     table: "Adverts",
            //     maxLength: 10000,
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(1000)",
            //     oldMaxLength: 1000);

            // migrationBuilder.AddColumn<string>(
            //     name: "RealtyTypeId",
            //     table: "Adverts",
            //     nullable: false,
            //     defaultValue: "");

            // migrationBuilder.CreateTable(
            //     name: "RealtyType",
            //     columns: table => new
            //     {
            //         RealtyTypeId = table.Column<string>(maxLength: 50, nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_RealtyType", x => x.RealtyTypeId);
            //     });

            // migrationBuilder.UpdateData(
            //     table: "AdvertTag",
            //     keyColumns: new[] { "AdvertId", "TagId" },
            //     keyValues: new object[] { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), "Евро ремонт" },
            //     column: "CreatedTag",
            //     value: new DateTime(2021, 10, 10, 11, 19, 56, 599, DateTimeKind.Utc).AddTicks(569));

            // migrationBuilder.UpdateData(
            //     table: "AdvertTag",
            //     keyColumns: new[] { "AdvertId", "TagId" },
            //     keyValues: new object[] { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), "квартира" },
            //     column: "CreatedTag",
            //     value: new DateTime(2021, 10, 10, 11, 19, 56, 599, DateTimeKind.Utc).AddTicks(569));

            // migrationBuilder.UpdateData(
            //     table: "AdvertTag",
            //     keyColumns: new[] { "AdvertId", "TagId" },
            //     keyValues: new object[] { new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"), "дом" },
            //     column: "CreatedTag",
            //     value: new DateTime(2021, 10, 10, 11, 19, 56, 599, DateTimeKind.Utc).AddTicks(569));

            // migrationBuilder.UpdateData(
            //     table: "AdvertTag",
            //     keyColumns: new[] { "AdvertId", "TagId" },
            //     keyValues: new object[] { new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"), "1 комнатная" },
            //     column: "CreatedTag",
            //     value: new DateTime(2021, 10, 10, 11, 19, 56, 599, DateTimeKind.Utc).AddTicks(569));

            // migrationBuilder.UpdateData(
            //     table: "AdvertTag",
            //     keyColumns: new[] { "AdvertId", "TagId" },
            //     keyValues: new object[] { new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"), "5 комнатная" },
            //     column: "CreatedTag",
            //     value: new DateTime(2021, 10, 10, 11, 19, 56, 599, DateTimeKind.Utc).AddTicks(569));

            // migrationBuilder.UpdateData(
            //     table: "AdvertTag",
            //     keyColumns: new[] { "AdvertId", "TagId" },
            //     keyValues: new object[] { new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"), "свежий ремонт" },
            //     column: "CreatedTag",
            //     value: new DateTime(2021, 10, 10, 11, 19, 56, 599, DateTimeKind.Utc).AddTicks(569));

            // migrationBuilder.UpdateData(
            //     table: "AdvertTag",
            //     keyColumns: new[] { "AdvertId", "TagId" },
            //     keyValues: new object[] { new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"), "комната" },
            //     column: "CreatedTag",
            //     value: new DateTime(2021, 10, 10, 11, 19, 56, 599, DateTimeKind.Utc).AddTicks(569));

            // migrationBuilder.UpdateData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: new Guid("48a4210f-3ce5-48ba-9461-80283ed1d94d"),
            //     column: "ConcurrencyStamp",
            //     value: "3bc72fdf-dca5-4ecc-829b-b45a999e8d33");

            // migrationBuilder.UpdateData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: new Guid("6d9b7113-a8f8-6035-99a7-a20dd400f6a3"),
            //     column: "ConcurrencyStamp",
            //     value: "bf4aa71c-5d9a-4ff8-a4ea-1c6bf94e0e91");

            // migrationBuilder.UpdateData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: new Guid("7771d881-221a-1e7d-b208-0118dcc088e1"),
            //     column: "ConcurrencyStamp",
            //     value: "a4fb71da-ff10-4fc0-b007-13122e0dacb3");

            // migrationBuilder.UpdateData(
            //     table: "AspNetUsers",
            //     keyColumn: "Id",
            //     keyValue: new Guid("7778a881-221a-1e7d-a208-0118ccc088e7"),
            //     columns: new[] { "ConcurrencyStamp", "CreatedUser", "PasswordHash", "SecurityStamp" },
            //     values: new object[] { "c86253a5-ac0e-49b2-81bc-0f1718618fb8", new DateTime(2021, 10, 10, 11, 19, 56, 535, DateTimeKind.Utc).AddTicks(532), "AQAAAAEAACcQAAAAEI1nvZJiO+5h7r6ROVru0mtcjh79SBTk3lFBx6aLcJeYU/E6HCQ6zFjuWYgI1V9kow==", "1c0c1a09-8d31-4e3c-b6f7-e410689926be" });

            // migrationBuilder.UpdateData(
            //     table: "AspNetUsers",
            //     keyColumn: "Id",
            //     keyValue: new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "ConcurrencyStamp", "CreatedUser" },
            //     values: new object[] { "b80ea3b5-f7aa-4793-9b60-f165aac65879", new DateTime(2021, 10, 11, 11, 19, 56, 593, DateTimeKind.Utc).AddTicks(566) });

            // migrationBuilder.UpdateData(
            //     table: "AspNetUsers",
            //     keyColumn: "Id",
            //     keyValue: new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "ConcurrencyStamp", "CreatedUser" },
            //     values: new object[] { "c7e9ac93-5ee1-4704-8622-3f2d5a977a78", new DateTime(2021, 10, 10, 11, 19, 56, 593, DateTimeKind.Utc).AddTicks(566) });

            // migrationBuilder.UpdateData(
            //     table: "AspNetUsers",
            //     keyColumn: "Id",
            //     keyValue: new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "ConcurrencyStamp", "CreatedUser" },
            //     values: new object[] { "2718b749-3a20-43b5-b1a7-32b97cf019c9", new DateTime(2021, 10, 10, 14, 19, 56, 593, DateTimeKind.Utc).AddTicks(566) });

            // migrationBuilder.InsertData(
            //     table: "RealtyType",
            //     column: "RealtyTypeId",
            //     values: new object[]
            //     {
            //         "комната",
            //         "квартира",
            //         "дом"
            //     });

            // migrationBuilder.UpdateData(
            //     table: "Adverts",
            //     keyColumn: "AdvertId",
            //     keyValue: new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "CreatedAd", "RealtyTypeId", "StartOfLease" },
            //     values: new object[] { new DateTime(2021, 10, 10, 21, 19, 56, 597, DateTimeKind.Utc).AddTicks(568), "квартира", new DateTime(2021, 10, 10, 21, 19, 56, 597, DateTimeKind.Utc).AddTicks(568) });

            // migrationBuilder.UpdateData(
            //     table: "Adverts",
            //     keyColumn: "AdvertId",
            //     keyValue: new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "CreatedAd", "RealtyTypeId", "StartOfLease" },
            //     values: new object[] { new DateTime(2021, 10, 10, 11, 19, 56, 597, DateTimeKind.Utc).AddTicks(568), "дом", new DateTime(2021, 10, 10, 11, 19, 56, 597, DateTimeKind.Utc).AddTicks(568) });

            // migrationBuilder.UpdateData(
            //     table: "Adverts",
            //     keyColumn: "AdvertId",
            //     keyValue: new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "CreatedAd", "RealtyTypeId", "StartOfLease" },
            //     values: new object[] { new DateTime(2021, 10, 10, 11, 19, 56, 597, DateTimeKind.Utc).AddTicks(568), "квартира", new DateTime(2021, 10, 10, 11, 19, 56, 597, DateTimeKind.Utc).AddTicks(568) });

            // migrationBuilder.UpdateData(
            //     table: "Adverts",
            //     keyColumn: "AdvertId",
            //     keyValue: new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "CreatedAd", "RealtyTypeId", "StartOfLease" },
            //     values: new object[] { new DateTime(2021, 10, 10, 16, 19, 56, 597, DateTimeKind.Utc).AddTicks(568), "квартира", new DateTime(2021, 10, 10, 16, 19, 56, 597, DateTimeKind.Utc).AddTicks(568) });

            // migrationBuilder.UpdateData(
            //     table: "Adverts",
            //     keyColumn: "AdvertId",
            //     keyValue: new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "CreatedAd", "RealtyTypeId", "StartOfLease" },
            //     values: new object[] { new DateTime(2021, 10, 13, 11, 19, 56, 597, DateTimeKind.Utc).AddTicks(568), "комната", new DateTime(2021, 10, 13, 11, 19, 56, 597, DateTimeKind.Utc).AddTicks(568) });

            // migrationBuilder.UpdateData(
            //     table: "Adverts",
            //     keyColumn: "AdvertId",
            //     keyValue: new Guid("a6d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "CreatedAd", "RealtyTypeId", "StartOfLease" },
            //     values: new object[] { new DateTime(2021, 10, 11, 11, 19, 56, 597, DateTimeKind.Utc).AddTicks(568), "квартира", new DateTime(2021, 10, 11, 11, 19, 56, 597, DateTimeKind.Utc).AddTicks(568) });

            // migrationBuilder.UpdateData(
            //     table: "Adverts",
            //     keyColumn: "AdvertId",
            //     keyValue: new Guid("a7d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "CreatedAd", "RealtyTypeId", "StartOfLease" },
            //     values: new object[] { new DateTime(2021, 10, 12, 11, 19, 56, 597, DateTimeKind.Utc).AddTicks(568), "квартира", new DateTime(2021, 10, 12, 11, 19, 56, 597, DateTimeKind.Utc).AddTicks(568) });

            // migrationBuilder.CreateIndex(
            //     name: "IX_Adverts_RealtyTypeId",
            //     table: "Adverts",
            //     column: "RealtyTypeId");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Adverts_RealtyType_RealtyTypeId",
            //     table: "Adverts",
            //     column: "RealtyTypeId",
            //     principalTable: "RealtyType",
            //     principalColumn: "RealtyTypeId",
            //     onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_Adverts_RealtyType_RealtyTypeId",
            //     table: "Adverts");

            // migrationBuilder.DropTable(
            //     name: "RealtyType");

            // migrationBuilder.DropIndex(
            //     name: "IX_Adverts_RealtyTypeId",
            //     table: "Adverts");

            // migrationBuilder.DropColumn(
            //     name: "RealtyTypeId",
            //     table: "Adverts");

            // migrationBuilder.AlterColumn<string>(
            //     name: "Description",
            //     table: "Adverts",
            //     type: "character varying(1000)",
            //     maxLength: 1000,
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldMaxLength: 10000);

            // migrationBuilder.AddColumn<string>(
            //     name: "AdvertTypeId",
            //     table: "Adverts",
            //     type: "character varying(50)",
            //     nullable: false,
            //     defaultValue: "");

            // migrationBuilder.CreateTable(
            //     name: "AdvertType",
            //     columns: table => new
            //     {
            //         AdvertTypeId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AdvertType", x => x.AdvertTypeId);
            //     });

            // migrationBuilder.UpdateData(
            //     table: "AdvertTag",
            //     keyColumns: new[] { "AdvertId", "TagId" },
            //     keyValues: new object[] { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), "Евро ремонт" },
            //     column: "CreatedTag",
            //     value: new DateTime(2021, 9, 5, 22, 6, 19, 813, DateTimeKind.Utc).AddTicks(2114));

            // migrationBuilder.UpdateData(
            //     table: "AdvertTag",
            //     keyColumns: new[] { "AdvertId", "TagId" },
            //     keyValues: new object[] { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), "квартира" },
            //     column: "CreatedTag",
            //     value: new DateTime(2021, 9, 5, 22, 6, 19, 813, DateTimeKind.Utc).AddTicks(2114));

            // migrationBuilder.UpdateData(
            //     table: "AdvertTag",
            //     keyColumns: new[] { "AdvertId", "TagId" },
            //     keyValues: new object[] { new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"), "дом" },
            //     column: "CreatedTag",
            //     value: new DateTime(2021, 9, 5, 22, 6, 19, 813, DateTimeKind.Utc).AddTicks(2114));

            // migrationBuilder.UpdateData(
            //     table: "AdvertTag",
            //     keyColumns: new[] { "AdvertId", "TagId" },
            //     keyValues: new object[] { new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"), "1 комнатная" },
            //     column: "CreatedTag",
            //     value: new DateTime(2021, 9, 5, 22, 6, 19, 813, DateTimeKind.Utc).AddTicks(2114));

            // migrationBuilder.UpdateData(
            //     table: "AdvertTag",
            //     keyColumns: new[] { "AdvertId", "TagId" },
            //     keyValues: new object[] { new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"), "5 комнатная" },
            //     column: "CreatedTag",
            //     value: new DateTime(2021, 9, 5, 22, 6, 19, 813, DateTimeKind.Utc).AddTicks(2114));

            // migrationBuilder.UpdateData(
            //     table: "AdvertTag",
            //     keyColumns: new[] { "AdvertId", "TagId" },
            //     keyValues: new object[] { new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"), "свежий ремонт" },
            //     column: "CreatedTag",
            //     value: new DateTime(2021, 9, 5, 22, 6, 19, 813, DateTimeKind.Utc).AddTicks(2114));

            // migrationBuilder.UpdateData(
            //     table: "AdvertTag",
            //     keyColumns: new[] { "AdvertId", "TagId" },
            //     keyValues: new object[] { new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"), "комната" },
            //     column: "CreatedTag",
            //     value: new DateTime(2021, 9, 5, 22, 6, 19, 813, DateTimeKind.Utc).AddTicks(2114));

            // migrationBuilder.InsertData(
            //     table: "AdvertType",
            //     column: "AdvertTypeId",
            //     values: new object[]
            //     {
            //         "дом",
            //         "комната",
            //         "квартира"
            //     });

            // migrationBuilder.UpdateData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: new Guid("48a4210f-3ce5-48ba-9461-80283ed1d94d"),
            //     column: "ConcurrencyStamp",
            //     value: "3dce83b9-e786-45cc-b585-4feb5b012ca3");

            // migrationBuilder.UpdateData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: new Guid("6d9b7113-a8f8-6035-99a7-a20dd400f6a3"),
            //     column: "ConcurrencyStamp",
            //     value: "41683f80-4bd6-4860-9064-269056bb8bff");

            // migrationBuilder.UpdateData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: new Guid("7771d881-221a-1e7d-b208-0118dcc088e1"),
            //     column: "ConcurrencyStamp",
            //     value: "71bef788-762d-4332-b177-458c15d3f656");

            // migrationBuilder.UpdateData(
            //     table: "AspNetUsers",
            //     keyColumn: "Id",
            //     keyValue: new Guid("7778a881-221a-1e7d-a208-0118ccc088e7"),
            //     columns: new[] { "ConcurrencyStamp", "CreatedUser", "PasswordHash", "SecurityStamp" },
            //     values: new object[] { "d5c14d17-980e-4d86-9e05-45a23a302841", new DateTime(2021, 9, 5, 22, 6, 19, 781, DateTimeKind.Utc).AddTicks(2096), "AQAAAAEAACcQAAAAEIVARMa18ptvA+lY9/elEVKE2vJDEOWNUZE5WqtRFxgwONIQtvuFy1uLL3qPKiHjGA==", "564129ab-28ca-40e5-b67b-63228b05c34e" });

            // migrationBuilder.UpdateData(
            //     table: "AspNetUsers",
            //     keyColumn: "Id",
            //     keyValue: new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "ConcurrencyStamp", "CreatedUser" },
            //     values: new object[] { "49b438d6-8e88-4887-a250-374fbc1bf3aa", new DateTime(2021, 9, 6, 22, 6, 19, 807, DateTimeKind.Utc).AddTicks(2111) });

            // migrationBuilder.UpdateData(
            //     table: "AspNetUsers",
            //     keyColumn: "Id",
            //     keyValue: new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "ConcurrencyStamp", "CreatedUser" },
            //     values: new object[] { "fdd58510-a997-4b23-9e08-a35cb09d18fe", new DateTime(2021, 9, 5, 22, 6, 19, 807, DateTimeKind.Utc).AddTicks(2111) });

            // migrationBuilder.UpdateData(
            //     table: "AspNetUsers",
            //     keyColumn: "Id",
            //     keyValue: new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "ConcurrencyStamp", "CreatedUser" },
            //     values: new object[] { "d03f6016-71e5-4f9b-9c77-8d84b93ba482", new DateTime(2021, 9, 6, 1, 6, 19, 807, DateTimeKind.Utc).AddTicks(2111) });

            // migrationBuilder.UpdateData(
            //     table: "Adverts",
            //     keyColumn: "AdvertId",
            //     keyValue: new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "AdvertTypeId", "CreatedAd", "StartOfLease" },
            //     values: new object[] { "квартира", new DateTime(2021, 9, 6, 8, 6, 19, 811, DateTimeKind.Utc).AddTicks(2113), new DateTime(2021, 9, 6, 8, 6, 19, 811, DateTimeKind.Utc).AddTicks(2113) });

            // migrationBuilder.UpdateData(
            //     table: "Adverts",
            //     keyColumn: "AdvertId",
            //     keyValue: new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "AdvertTypeId", "CreatedAd", "StartOfLease" },
            //     values: new object[] { "дом", new DateTime(2021, 9, 5, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113), new DateTime(2021, 9, 5, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113) });

            // migrationBuilder.UpdateData(
            //     table: "Adverts",
            //     keyColumn: "AdvertId",
            //     keyValue: new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "AdvertTypeId", "CreatedAd", "StartOfLease" },
            //     values: new object[] { "квартира", new DateTime(2021, 9, 5, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113), new DateTime(2021, 9, 5, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113) });

            // migrationBuilder.UpdateData(
            //     table: "Adverts",
            //     keyColumn: "AdvertId",
            //     keyValue: new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "AdvertTypeId", "CreatedAd", "StartOfLease" },
            //     values: new object[] { "квартира", new DateTime(2021, 9, 6, 3, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113), new DateTime(2021, 9, 6, 3, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113) });

            // migrationBuilder.UpdateData(
            //     table: "Adverts",
            //     keyColumn: "AdvertId",
            //     keyValue: new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "AdvertTypeId", "CreatedAd", "StartOfLease" },
            //     values: new object[] { "комната", new DateTime(2021, 9, 8, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113), new DateTime(2021, 9, 8, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113) });

            // migrationBuilder.UpdateData(
            //     table: "Adverts",
            //     keyColumn: "AdvertId",
            //     keyValue: new Guid("a6d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "AdvertTypeId", "CreatedAd", "StartOfLease" },
            //     values: new object[] { "квартира", new DateTime(2021, 9, 6, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113), new DateTime(2021, 9, 6, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113) });

            // migrationBuilder.UpdateData(
            //     table: "Adverts",
            //     keyColumn: "AdvertId",
            //     keyValue: new Guid("a7d4c053-49b6-410c-bc78-2d54a9991870"),
            //     columns: new[] { "AdvertTypeId", "CreatedAd", "StartOfLease" },
            //     values: new object[] { "квартира", new DateTime(2021, 9, 7, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113), new DateTime(2021, 9, 7, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113) });

            // migrationBuilder.CreateIndex(
            //     name: "IX_Adverts_AdvertTypeId",
            //     table: "Adverts",
            //     column: "AdvertTypeId");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Adverts_AdvertType_AdvertTypeId",
            //     table: "Adverts",
            //     column: "AdvertTypeId",
            //     principalTable: "AdvertType",
            //     principalColumn: "AdvertTypeId",
            //     onDelete: ReferentialAction.Cascade);
        }
    }
}
