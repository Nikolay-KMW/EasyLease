using System;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class AdvertTagConfiguration : IEntityTypeConfiguration<AdvertTag> {
        public void Configure(EntityTypeBuilder<AdvertTag> builder) {
            builder.HasData(

                new AdvertTag {
                    AdvertId = new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
                    TagId = "квартира",
                    CreatedTag = DateTime.UtcNow
                },
                new AdvertTag {
                    AdvertId = new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
                    TagId = "Евро ремонт",
                    CreatedTag = DateTime.UtcNow
                },
                new AdvertTag {
                    AdvertId = new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"),
                    TagId = "дом",
                    CreatedTag = DateTime.UtcNow
                },
                new AdvertTag {
                    AdvertId = new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"),
                    TagId = "1 комнатная",
                    CreatedTag = DateTime.UtcNow
                },
                new AdvertTag {
                    AdvertId = new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"),
                    TagId = "5 комнатная",
                    CreatedTag = DateTime.UtcNow
                },
                new AdvertTag {
                    AdvertId = new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"),
                    TagId = "свежий ремонт",
                    CreatedTag = DateTime.UtcNow
                },
                new AdvertTag {
                    AdvertId = new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"),
                    TagId = "комната",
                    CreatedTag = DateTime.UtcNow
                }
            );
        }
    }
}