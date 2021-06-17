using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class AdvertTypeConfiguration : IEntityTypeConfiguration<AdvertType> {
        public void Configure(EntityTypeBuilder<AdvertType> builder) {
            builder.HasData(
                new AdvertType {
                    Id = "дом"
                },
                new AdvertType {
                    Id = "квартира"
                },
                new AdvertType {
                    Id = "комната"
                }
            );
        }
    }
}