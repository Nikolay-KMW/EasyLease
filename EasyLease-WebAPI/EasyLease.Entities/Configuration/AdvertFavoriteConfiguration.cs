using System;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class AdvertFavoriteConfiguration : IEntityTypeConfiguration<AdvertFavorite> {
        public void Configure(EntityTypeBuilder<AdvertFavorite> builder) {
            builder.HasData(

                new AdvertFavorite {
                    AdvertId = new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
                    UserId = new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new AdvertFavorite {
                    AdvertId = new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
                    UserId = new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new AdvertFavorite {
                    AdvertId = new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"),
                    UserId = new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new AdvertFavorite {
                    AdvertId = new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"),
                    UserId = new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new AdvertFavorite {
                    AdvertId = new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"),
                    UserId = new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
                }
            );
        }
    }
}