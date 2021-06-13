using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class SettlementTypeConfiguration : IEntityTypeConfiguration<SettlementType> {
        public void Configure(EntityTypeBuilder<SettlementType> builder) {
            builder.HasData(
                new SettlementType {
                    Id = 1,
                    Name = "село"
                },
                new SettlementType {
                    Id = 2,
                    Name = "смт"
                },
                new SettlementType {
                    Id = 3,
                    Name = "город"
                }
            );
        }
    }
}