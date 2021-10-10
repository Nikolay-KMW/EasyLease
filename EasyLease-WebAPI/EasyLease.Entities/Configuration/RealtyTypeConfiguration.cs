using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class RealtyTypeConfiguration : IEntityTypeConfiguration<RealtyType> {
        public void Configure(EntityTypeBuilder<RealtyType> builder) {
            builder.HasData(
                new RealtyType {
                    Id = "дом"
                },
                new RealtyType {
                    Id = "квартира"
                },
                new RealtyType {
                    Id = "комната"
                }
            );
        }
    }
}