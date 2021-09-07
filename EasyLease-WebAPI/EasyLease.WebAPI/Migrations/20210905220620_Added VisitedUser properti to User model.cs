using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyLease.WebAPI.Migrations
{
    public partial class AddedVisitedUserpropertitoUsermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "VisitedUser",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AdvertTag",
                keyColumns: new[] { "AdvertId", "TagId" },
                keyValues: new object[] { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), "Евро ремонт" },
                column: "CreatedTag",
                value: new DateTime(2021, 9, 5, 22, 6, 19, 813, DateTimeKind.Utc).AddTicks(2114));

            migrationBuilder.UpdateData(
                table: "AdvertTag",
                keyColumns: new[] { "AdvertId", "TagId" },
                keyValues: new object[] { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), "квартира" },
                column: "CreatedTag",
                value: new DateTime(2021, 9, 5, 22, 6, 19, 813, DateTimeKind.Utc).AddTicks(2114));

            migrationBuilder.UpdateData(
                table: "AdvertTag",
                keyColumns: new[] { "AdvertId", "TagId" },
                keyValues: new object[] { new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"), "дом" },
                column: "CreatedTag",
                value: new DateTime(2021, 9, 5, 22, 6, 19, 813, DateTimeKind.Utc).AddTicks(2114));

            migrationBuilder.UpdateData(
                table: "AdvertTag",
                keyColumns: new[] { "AdvertId", "TagId" },
                keyValues: new object[] { new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"), "1 комнатная" },
                column: "CreatedTag",
                value: new DateTime(2021, 9, 5, 22, 6, 19, 813, DateTimeKind.Utc).AddTicks(2114));

            migrationBuilder.UpdateData(
                table: "AdvertTag",
                keyColumns: new[] { "AdvertId", "TagId" },
                keyValues: new object[] { new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"), "5 комнатная" },
                column: "CreatedTag",
                value: new DateTime(2021, 9, 5, 22, 6, 19, 813, DateTimeKind.Utc).AddTicks(2114));

            migrationBuilder.UpdateData(
                table: "AdvertTag",
                keyColumns: new[] { "AdvertId", "TagId" },
                keyValues: new object[] { new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"), "свежий ремонт" },
                column: "CreatedTag",
                value: new DateTime(2021, 9, 5, 22, 6, 19, 813, DateTimeKind.Utc).AddTicks(2114));

            migrationBuilder.UpdateData(
                table: "AdvertTag",
                keyColumns: new[] { "AdvertId", "TagId" },
                keyValues: new object[] { new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"), "комната" },
                column: "CreatedTag",
                value: new DateTime(2021, 9, 5, 22, 6, 19, 813, DateTimeKind.Utc).AddTicks(2114));

            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "AdvertId",
                keyValue: new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAd", "StartOfLease" },
                values: new object[] { new DateTime(2021, 9, 6, 8, 6, 19, 811, DateTimeKind.Utc).AddTicks(2113), new DateTime(2021, 9, 6, 8, 6, 19, 811, DateTimeKind.Utc).AddTicks(2113) });

            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "AdvertId",
                keyValue: new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAd", "StartOfLease" },
                values: new object[] { new DateTime(2021, 9, 5, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113), new DateTime(2021, 9, 5, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113) });

            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "AdvertId",
                keyValue: new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAd", "StartOfLease" },
                values: new object[] { new DateTime(2021, 9, 5, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113), new DateTime(2021, 9, 5, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113) });

            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "AdvertId",
                keyValue: new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAd", "StartOfLease" },
                values: new object[] { new DateTime(2021, 9, 6, 3, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113), new DateTime(2021, 9, 6, 3, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113) });

            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "AdvertId",
                keyValue: new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAd", "StartOfLease" },
                values: new object[] { new DateTime(2021, 9, 8, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113), new DateTime(2021, 9, 8, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113) });

            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "AdvertId",
                keyValue: new Guid("a6d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAd", "StartOfLease" },
                values: new object[] { new DateTime(2021, 9, 6, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113), new DateTime(2021, 9, 6, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113) });

            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "AdvertId",
                keyValue: new Guid("a7d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAd", "StartOfLease" },
                values: new object[] { new DateTime(2021, 9, 7, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113), new DateTime(2021, 9, 7, 22, 6, 19, 812, DateTimeKind.Utc).AddTicks(2113) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("48a4210f-3ce5-48ba-9461-80283ed1d94d"),
                column: "ConcurrencyStamp",
                value: "3dce83b9-e786-45cc-b585-4feb5b012ca3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6d9b7113-a8f8-6035-99a7-a20dd400f6a3"),
                column: "ConcurrencyStamp",
                value: "41683f80-4bd6-4860-9064-269056bb8bff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7771d881-221a-1e7d-b208-0118dcc088e1"),
                column: "ConcurrencyStamp",
                value: "71bef788-762d-4332-b177-458c15d3f656");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7778a881-221a-1e7d-a208-0118ccc088e7"),
                columns: new[] { "ConcurrencyStamp", "CreatedUser", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5c14d17-980e-4d86-9e05-45a23a302841", new DateTime(2021, 9, 5, 22, 6, 19, 781, DateTimeKind.Utc).AddTicks(2096), "AQAAAAEAACcQAAAAEIVARMa18ptvA+lY9/elEVKE2vJDEOWNUZE5WqtRFxgwONIQtvuFy1uLL3qPKiHjGA==", "564129ab-28ca-40e5-b67b-63228b05c34e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "ConcurrencyStamp", "CreatedUser" },
                values: new object[] { "49b438d6-8e88-4887-a250-374fbc1bf3aa", new DateTime(2021, 9, 6, 22, 6, 19, 807, DateTimeKind.Utc).AddTicks(2111) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "ConcurrencyStamp", "CreatedUser" },
                values: new object[] { "fdd58510-a997-4b23-9e08-a35cb09d18fe", new DateTime(2021, 9, 5, 22, 6, 19, 807, DateTimeKind.Utc).AddTicks(2111) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "ConcurrencyStamp", "CreatedUser" },
                values: new object[] { "d03f6016-71e5-4f9b-9c77-8d84b93ba482", new DateTime(2021, 9, 6, 1, 6, 19, 807, DateTimeKind.Utc).AddTicks(2111) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitedUser",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AdvertTag",
                keyColumns: new[] { "AdvertId", "TagId" },
                keyValues: new object[] { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), "Евро ремонт" },
                column: "CreatedTag",
                value: new DateTime(2021, 8, 25, 15, 11, 49, 49, DateTimeKind.Utc).AddTicks(859));

            migrationBuilder.UpdateData(
                table: "AdvertTag",
                keyColumns: new[] { "AdvertId", "TagId" },
                keyValues: new object[] { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), "квартира" },
                column: "CreatedTag",
                value: new DateTime(2021, 8, 25, 15, 11, 49, 49, DateTimeKind.Utc).AddTicks(859));

            migrationBuilder.UpdateData(
                table: "AdvertTag",
                keyColumns: new[] { "AdvertId", "TagId" },
                keyValues: new object[] { new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"), "дом" },
                column: "CreatedTag",
                value: new DateTime(2021, 8, 25, 15, 11, 49, 49, DateTimeKind.Utc).AddTicks(859));

            migrationBuilder.UpdateData(
                table: "AdvertTag",
                keyColumns: new[] { "AdvertId", "TagId" },
                keyValues: new object[] { new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"), "1 комнатная" },
                column: "CreatedTag",
                value: new DateTime(2021, 8, 25, 15, 11, 49, 49, DateTimeKind.Utc).AddTicks(859));

            migrationBuilder.UpdateData(
                table: "AdvertTag",
                keyColumns: new[] { "AdvertId", "TagId" },
                keyValues: new object[] { new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"), "5 комнатная" },
                column: "CreatedTag",
                value: new DateTime(2021, 8, 25, 15, 11, 49, 49, DateTimeKind.Utc).AddTicks(859));

            migrationBuilder.UpdateData(
                table: "AdvertTag",
                keyColumns: new[] { "AdvertId", "TagId" },
                keyValues: new object[] { new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"), "свежий ремонт" },
                column: "CreatedTag",
                value: new DateTime(2021, 8, 25, 15, 11, 49, 49, DateTimeKind.Utc).AddTicks(859));

            migrationBuilder.UpdateData(
                table: "AdvertTag",
                keyColumns: new[] { "AdvertId", "TagId" },
                keyValues: new object[] { new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"), "комната" },
                column: "CreatedTag",
                value: new DateTime(2021, 8, 25, 15, 11, 49, 49, DateTimeKind.Utc).AddTicks(859));

            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "AdvertId",
                keyValue: new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAd", "StartOfLease" },
                values: new object[] { new DateTime(2021, 8, 26, 1, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), new DateTime(2021, 8, 26, 1, 11, 49, 47, DateTimeKind.Utc).AddTicks(858) });

            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "AdvertId",
                keyValue: new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAd", "StartOfLease" },
                values: new object[] { new DateTime(2021, 8, 25, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), new DateTime(2021, 8, 25, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858) });

            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "AdvertId",
                keyValue: new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAd", "StartOfLease" },
                values: new object[] { new DateTime(2021, 8, 25, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), new DateTime(2021, 8, 25, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858) });

            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "AdvertId",
                keyValue: new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAd", "StartOfLease" },
                values: new object[] { new DateTime(2021, 8, 25, 20, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), new DateTime(2021, 8, 25, 20, 11, 49, 47, DateTimeKind.Utc).AddTicks(858) });

            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "AdvertId",
                keyValue: new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAd", "StartOfLease" },
                values: new object[] { new DateTime(2021, 8, 28, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), new DateTime(2021, 8, 28, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858) });

            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "AdvertId",
                keyValue: new Guid("a6d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAd", "StartOfLease" },
                values: new object[] { new DateTime(2021, 8, 26, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), new DateTime(2021, 8, 26, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858) });

            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "AdvertId",
                keyValue: new Guid("a7d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAd", "StartOfLease" },
                values: new object[] { new DateTime(2021, 8, 27, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858), new DateTime(2021, 8, 27, 15, 11, 49, 47, DateTimeKind.Utc).AddTicks(858) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("48a4210f-3ce5-48ba-9461-80283ed1d94d"),
                column: "ConcurrencyStamp",
                value: "6e814029-953a-4dba-a872-0e96774bb008");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6d9b7113-a8f8-6035-99a7-a20dd400f6a3"),
                column: "ConcurrencyStamp",
                value: "037fea62-20d5-4047-90dc-1dd3e2f401b6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7771d881-221a-1e7d-b208-0118dcc088e1"),
                column: "ConcurrencyStamp",
                value: "5101130a-ccae-456b-a113-506e24ee4b36");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7778a881-221a-1e7d-a208-0118ccc088e7"),
                columns: new[] { "ConcurrencyStamp", "CreatedUser", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0ff9c218-f680-4b8b-b0af-2a35f8f0e43b", new DateTime(2021, 8, 25, 15, 11, 49, 14, DateTimeKind.Utc).AddTicks(839), "AQAAAAEAACcQAAAAECyJlu13nnaSmESQfSieenqVXXfZ3/YLhw2b+mEgcIYTXwjtbsvyQ8XxXQBWUPGDXA==", "fbb386af-9068-410d-9654-58a4ca31a783" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "ConcurrencyStamp", "CreatedUser" },
                values: new object[] { "1d1dc9fe-7633-45bb-af9a-c04fc154f3d3", new DateTime(2021, 8, 26, 15, 11, 49, 42, DateTimeKind.Utc).AddTicks(855) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "ConcurrencyStamp", "CreatedUser" },
                values: new object[] { "ad34f344-efcd-4dbc-b3c2-26e328fd858d", new DateTime(2021, 8, 25, 15, 11, 49, 42, DateTimeKind.Utc).AddTicks(855) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "ConcurrencyStamp", "CreatedUser" },
                values: new object[] { "3d5c7d92-6951-4093-ad45-97e98c9dba01", new DateTime(2021, 8, 25, 18, 11, 49, 42, DateTimeKind.Utc).AddTicks(855) });
        }
    }
}
