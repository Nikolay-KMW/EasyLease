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
                    CreatedAd = DateTime.Now,
                    UpdatedAd = null,
                    Slug = "Сдам-2-х-комнатную-квартиру-о1",
                    UserId = new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                 new Advert {
                     Id = new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"),
                     Title = "Сдам дом",
                     Description = "Дом 280 м2. Элитный дизайн.",
                     CreatedAd = DateTime.Now,
                     UpdatedAd = null,
                     Slug = "Сдам-дом-о2",
                     UserId = new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                 },
                 new Advert {
                     Id = new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"),
                     Title = "Сдам 1 комнатную квартиру",
                     Description = "Есть все кроме холодильника",
                     CreatedAd = DateTime.Now,
                     UpdatedAd = null,
                     Slug = "Сдам-1-комнатную-квартиру-о3",
                     UserId = new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
                 },
                new Advert {
                    Id = new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"),
                    Title = "Сдам 5-х комнатную квартиру",
                    Description = "Сделанный свежий ремонт",
                    CreatedAd = DateTime.Now,
                    UpdatedAd = null,
                    Slug = "Сдам-5-х-комнатную-квартиру-о4",
                    UserId = new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new Advert {
                    Id = new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"),
                    Title = "Сдам комнату в общежитие",
                    Description = "Все уютненько.",
                    CreatedAd = DateTime.Now,
                    UpdatedAd = null,
                    Slug = "Сдам-комнату-в-общежитие-о5",
                    UserId = new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new Advert {
                    Id = new Guid("a6d4c053-49b6-410c-bc78-2d54a9991870"),
                    Title = "Сдам 3-х комнатную квартиру",
                    Description = "Евро ремонт.",
                    CreatedAd = DateTime.Now,
                    UpdatedAd = null,
                    Slug = "Сдам-3-х-комнатную-квартиру-о6",
                    UserId = new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                }
            );
        }
    }
}