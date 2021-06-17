using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class SettlementTypeConfiguration : IEntityTypeConfiguration<SettlementType> {
        public void Configure(EntityTypeBuilder<SettlementType> builder) {
            builder.HasData(
                new SettlementType {
                    Id = "село"
                },
                new SettlementType {
                    Id = "смт"
                },
                new SettlementType {
                    Id = "город"
                }
            );
        }
    }
}