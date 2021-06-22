using System;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class AdvertConfiguration : IEntityTypeConfiguration<Advert> {
        public void Configure(EntityTypeBuilder<Advert> builder) {
            builder.HasData(
                new Advert {
                    Id = new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
                    AdvertTypeId = "квартира",
                    Title = "Сдам 2-х комнатную квартиру",
                    Description = "Есть все необходимое",

                    NumberOfRooms = 2,
                    Area = 60,
                    Storey = 3,
                    NumberOfStoreys = 9,

                    Region = "Сумская",
                    District = "Сумской",
                    SettlementTypeId = "город",
                    SettlementName = "Сумы",
                    StreetTypeId = "улица",
                    StreetName = "Соборна",
                    HouseNumber = "10",
                    ApartmentNumber = 5,

                    Images = "src/Сдам-2-х-комнатную-квартиру-о1",
                    Status = Status.Active,
                    PriceType = PriceType.PricePerDay,
                    Price = 100,
                    StartOfLease = DateTime.UtcNow.AddHours(10),
                    EndOfLease = null,

                    CreatedAd = DateTime.UtcNow.AddHours(10),
                    UpdatedAd = null,
                    Slug = "Сдам-2-х-комнатную-квартиру-о1",
                    UserId = new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                 new Advert {
                     Id = new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"),
                     AdvertTypeId = "дом",
                     Title = "Сдам дом",
                     Description = "Дом 280 м2. Элитный дизайн.",

                     NumberOfRooms = 8,
                     Area = 280,
                     NumberOfStoreys = 2,

                     Region = "Сумская",
                     District = "Сумской",
                     SettlementTypeId = "город",
                     SettlementName = "Сумы",
                     StreetTypeId = "улица",
                     StreetName = "Мира",
                     HouseNumber = "8",
                     ApartmentNumber = null,

                     Images = "src/Сдам-дом-о2",
                     Status = Status.Active,
                     PriceType = PriceType.PricePerMonth,
                     Price = 3000,
                     StartOfLease = DateTime.UtcNow,
                     EndOfLease = null,

                     CreatedAd = DateTime.UtcNow,
                     UpdatedAd = null,
                     Slug = "Сдам-дом-о2",
                     UserId = new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                 },
                 new Advert {
                     Id = new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"),
                     AdvertTypeId = "квартира",
                     Title = "Сдам 1 комнатную квартиру",
                     Description = "Есть все кроме холодильника",

                     NumberOfRooms = 1,
                     Area = 45,
                     Storey = 7,
                     NumberOfStoreys = 9,

                     Region = "Черниговская",
                     District = "Прилуцкий",
                     SettlementTypeId = "город",
                     SettlementName = "Прилуки",
                     StreetTypeId = "улица",
                     StreetName = "Вовка",
                     HouseNumber = "25",
                     ApartmentNumber = 7,

                     Images = "src/Сдам-1-комнатную-квартиру-о3",
                     Status = Status.Active,
                     PriceType = PriceType.PricePerDay,
                     Price = 50,
                     StartOfLease = DateTime.UtcNow,
                     EndOfLease = null,

                     CreatedAd = DateTime.UtcNow,
                     UpdatedAd = null,
                     Slug = "Сдам-1-комнатную-квартиру-о3",
                     UserId = new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
                 },
                new Advert {
                    Id = new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"),
                    AdvertTypeId = "квартира",
                    Title = "Сдам 5-х комнатную квартиру",
                    Description = "Сделанный свежий ремонт",

                    NumberOfRooms = 5,
                    Area = 100,
                    Storey = 5,
                    NumberOfStoreys = 5,

                    Region = "Сумская",
                    District = "Сумской",
                    SettlementTypeId = "город",
                    SettlementName = "Сумы",
                    StreetTypeId = "улица",
                    StreetName = "Супруна",
                    HouseNumber = "2",
                    ApartmentNumber = 13,

                    Images = "src/Сдам-5-х-комнатную-квартиру-о4",
                    Status = Status.Active,
                    PriceType = PriceType.PricePerDay,
                    Price = 300,
                    StartOfLease = DateTime.UtcNow.AddHours(5),
                    EndOfLease = null,

                    CreatedAd = DateTime.UtcNow.AddHours(5),
                    UpdatedAd = null,
                    Slug = "Сдам-5-х-комнатную-квартиру-о4",
                    UserId = new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new Advert {
                    Id = new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"),
                    AdvertTypeId = "комната",
                    Title = "Сдам комнату в общежитие",
                    Description = "Все уютненько.",

                    NumberOfRooms = 1,
                    Area = 25,
                    Storey = 3,
                    NumberOfStoreys = 5,

                    Region = "Сумская",
                    District = "Сумской",
                    SettlementTypeId = "город",
                    SettlementName = "Сумы",
                    StreetTypeId = "набережная",
                    StreetName = "Стрелки",
                    HouseNumber = "31",
                    ApartmentNumber = 5,

                    Images = "src/Сдам-комнату-в-общежитие-о5",
                    Status = Status.Active,
                    PriceType = PriceType.PricePerMonth,
                    Price = 500,
                    StartOfLease = DateTime.UtcNow.AddDays(3),
                    EndOfLease = null,

                    CreatedAd = DateTime.UtcNow.AddDays(3),
                    UpdatedAd = null,
                    Slug = "Сдам-комнату-в-общежитие-о5",
                    UserId = new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new Advert {
                    Id = new Guid("a6d4c053-49b6-410c-bc78-2d54a9991870"),
                    AdvertTypeId = "квартира",
                    Title = "Сдам 3-х комнатную квартиру",
                    Description = "Евро ремонт.",

                    NumberOfRooms = 3,
                    Area = 70,
                    Storey = 8,
                    NumberOfStoreys = 9,

                    Region = "Сумская",
                    District = "Сумской",
                    SettlementTypeId = "город",
                    SettlementName = "Сумы",
                    StreetTypeId = "улица",
                    StreetName = "Соборна",
                    HouseNumber = "2",
                    ApartmentNumber = 5,

                    Images = "src/Сдам-3-х-комнатную-квартиру-о6",
                    Status = Status.Active,
                    PriceType = PriceType.PricePerDay,
                    Price = 150,
                    StartOfLease = DateTime.UtcNow.AddDays(1),
                    EndOfLease = null,

                    CreatedAd = DateTime.UtcNow.AddDays(1),
                    UpdatedAd = null,
                    Slug = "Сдам-3-х-комнатную-квартиру-о6",
                    UserId = new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                 new Advert {
                     Id = new Guid("a7d4c053-49b6-410c-bc78-2d54a9991870"),
                     AdvertTypeId = "квартира",
                     Title = "Сдам 4-х комнатную квартиру",
                     Description = "Сделан культурный ремонт",

                     NumberOfRooms = 4,
                     Area = 85,
                     Storey = 2,
                     NumberOfStoreys = 9,

                     Region = "Черниговская",
                     District = "Новгород-Северский",
                     SettlementTypeId = "город",
                     SettlementName = "Новгород",
                     StreetTypeId = "улица",
                     StreetName = "Победы",
                     HouseNumber = "1",
                     ApartmentNumber = 7,

                     Images = "src/Сдам-3-х-комнатную-квартиру-о7",
                     Status = Status.Active,
                     PriceType = PriceType.PricePerDay,
                     Price = 300,
                     StartOfLease = DateTime.UtcNow.AddDays(2),
                     EndOfLease = null,

                     CreatedAd = DateTime.UtcNow.AddDays(2),
                     UpdatedAd = null,
                     Slug = "Сдам-5-х-комнатную-квартиру-о7",
                     UserId = new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                 }
            );
        }
    }
}