using System;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite> {
        public void Configure(EntityTypeBuilder<Favorite> builder) {
            builder.HasData(
                new Favorite {
                    Id = new Guid("d1d4c053-49b6-410c-bc78-2d54a9991870"),
                    UserId = new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new Favorite {
                    Id = new Guid("d2d4c053-49b6-410c-bc78-2d54a9991870"),
                    UserId = new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870"),
                }
            );
        }
    }
}