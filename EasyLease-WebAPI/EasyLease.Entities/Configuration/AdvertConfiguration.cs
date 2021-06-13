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
                    Title = "Сдам 2-х комнатную квартиру",
                    Description = "Есть все необходимое",

                    RegionId = 1,
                    DistrictId = 1,
                    SettlementTypeId = 3,
                    SettlementName = "Сумы",
                    StreetTypeId = 1,
                    StreetName = "Соборна",
                    HouseNumber = "10",
                    ApartmentNumber = 5,

                    Images = "src/Сдам-2-х-комнатную-квартиру-о1",
                    ActiveAd = true,
                    Price = 100,
                    PriceTypeId = 1,
                    StartOfLease = DateTime.Now,
                    EndOfLease = null,

                    CreatedAd = DateTime.Now,
                    UpdatedAd = null,
                    Slug = "Сдам-2-х-комнатную-квартиру-о1",
                    FavoriteUserId = new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870"),
                    AuthorUserId = new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                 new Advert {
                     Id = new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"),
                     Title = "Сдам дом",
                     Description = "Дом 280 м2. Элитный дизайн.",

                     RegionId = 1,
                     DistrictId = 1,
                     SettlementTypeId = 3,
                     SettlementName = "Сумы",
                     StreetTypeId = 1,
                     StreetName = "Мира",
                     HouseNumber = "8",
                     ApartmentNumber = null,

                     Images = "src/Сдам-дом-о2",
                     ActiveAd = true,
                     Price = 3000,
                     PriceTypeId = 2,
                     StartOfLease = DateTime.Now,
                     EndOfLease = null,

                     CreatedAd = DateTime.Now,
                     UpdatedAd = null,
                     Slug = "Сдам-дом-о2",
                     FavoriteUserId = null,
                     AuthorUserId = new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                 },
                 new Advert {
                     Id = new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"),
                     Title = "Сдам 1 комнатную квартиру",
                     Description = "Есть все кроме холодильника",

                     RegionId = 23,
                     DistrictId = 9,
                     SettlementTypeId = 3,
                     SettlementName = "Прилуки",
                     StreetTypeId = 1,
                     StreetName = "Вовка",
                     HouseNumber = "25",
                     ApartmentNumber = 7,

                     Images = "src/Сдам-1-комнатную-квартиру-о3",
                     ActiveAd = true,
                     Price = 50,
                     PriceTypeId = 1,
                     StartOfLease = DateTime.Now,
                     EndOfLease = null,

                     CreatedAd = DateTime.Now,
                     UpdatedAd = null,
                     Slug = "Сдам-1-комнатную-квартиру-о3",
                     FavoriteUserId = new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                     AuthorUserId = new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
                 },
                new Advert {
                    Id = new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"),
                    Title = "Сдам 5-х комнатную квартиру",
                    Description = "Сделанный свежий ремонт",

                    RegionId = 1,
                    DistrictId = 1,
                    SettlementTypeId = 3,
                    SettlementName = "Сумы",
                    StreetTypeId = 1,
                    StreetName = "Супруна",
                    HouseNumber = "2",
                    ApartmentNumber = 13,

                    Images = "src/Сдам-5-х-комнатную-квартиру-о4",
                    ActiveAd = true,
                    Price = 300,
                    PriceTypeId = 1,
                    StartOfLease = DateTime.Now,
                    EndOfLease = null,

                    CreatedAd = DateTime.Now,
                    UpdatedAd = null,
                    Slug = "Сдам-5-х-комнатную-квартиру-о4",
                    FavoriteUserId = null,
                    AuthorUserId = new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new Advert {
                    Id = new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"),
                    Title = "Сдам комнату в общежитие",
                    Description = "Все уютненько.",

                    RegionId = 1,
                    DistrictId = 1,
                    SettlementTypeId = 3,
                    SettlementName = "Сумы",
                    StreetTypeId = 9,
                    StreetName = "Стрелки",
                    HouseNumber = "31",
                    ApartmentNumber = 5,

                    Images = "src/Сдам-комнату-в-общежитие-о5",
                    ActiveAd = true,
                    Price = 500,
                    PriceTypeId = 2,
                    StartOfLease = DateTime.Now,
                    EndOfLease = null,

                    CreatedAd = DateTime.Now,
                    UpdatedAd = null,
                    Slug = "Сдам-комнату-в-общежитие-о5",
                    FavoriteUserId = null,
                    AuthorUserId = new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new Advert {
                    Id = new Guid("a6d4c053-49b6-410c-bc78-2d54a9991870"),
                    Title = "Сдам 3-х комнатную квартиру",
                    Description = "Евро ремонт.",

                    RegionId = 1,
                    DistrictId = 1,
                    SettlementTypeId = 3,
                    SettlementName = "Сумы",
                    StreetTypeId = 1,
                    StreetName = "Соборна",
                    HouseNumber = "2",
                    ApartmentNumber = 5,

                    Images = "src/Сдам-3-х-комнатную-квартиру-о6",
                    ActiveAd = true,
                    Price = 150,
                    PriceTypeId = 1,
                    StartOfLease = DateTime.Now,
                    EndOfLease = null,

                    CreatedAd = DateTime.Now,
                    UpdatedAd = null,
                    Slug = "Сдам-3-х-комнатную-квартиру-о6",
                    FavoriteUserId = new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
                    AuthorUserId = new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                }
            );
        }
    }
}