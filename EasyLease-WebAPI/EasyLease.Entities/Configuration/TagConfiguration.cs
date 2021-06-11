using System;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class TagConfiguration : IEntityTypeConfiguration<Tag> {
        public void Configure(EntityTypeBuilder<Tag> builder) {
            builder.HasData(
                new Tag {
                    Id = new Guid("b1d4c053-49b6-410c-bc78-2d54a9991870"),
                    TagName = "квартира",
                    AdvertId = new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new Tag {
                    Id = new Guid("b2d4c053-49b6-410c-bc78-2d54a9991870"),
                    TagName = "Все есть",
                    AdvertId = new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new Tag {
                    Id = new Guid("b3d4c053-49b6-410c-bc78-2d54a9991870"),
                    TagName = "дом",
                    AdvertId = new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new Tag {
                    Id = new Guid("b4d4c053-49b6-410c-bc78-2d54a9991870"),
                    TagName = "1 комнатная",
                    AdvertId = new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new Tag {
                    Id = new Guid("b5d4c053-49b6-410c-bc78-2d54a9991870"),
                    TagName = "5 комнатная",
                    AdvertId = new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new Tag {
                    Id = new Guid("b6d4c053-49b6-410c-bc78-2d54a9991870"),
                    TagName = "свежий ремонт",
                    AdvertId = new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new Tag {
                    Id = new Guid("b7d4c053-49b6-410c-bc78-2d54a9991870"),
                    TagName = "комната",
                    AdvertId = new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"),
                }
            );
        }
    }
}