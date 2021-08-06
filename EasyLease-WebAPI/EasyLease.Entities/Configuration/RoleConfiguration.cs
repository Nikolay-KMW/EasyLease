using System;
using EasyLease.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>> {
        private Guid userRoleId = new Guid("6D9B7113-A8F8-6035-99A7-A20DD400F6A3");
        private Guid moderatorRoleId = new Guid("48A4210F-3CE5-48BA-9461-80283ED1D94D");
        private Guid adminRoleId = new Guid("7771D881-221A-1E7D-B208-0118DCC088E1");

        public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder) {
            builder.HasData(
                 new IdentityRole<Guid> {
                     Id = userRoleId,
                     Name = "User",
                     NormalizedName = "USER"
                 },
                new IdentityRole<Guid> {
                    Id = moderatorRoleId,
                    Name = "Moderator",
                    NormalizedName = "MODERATOR"
                },
                new IdentityRole<Guid> {
                    Id = adminRoleId,
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
            );
        }
    }
}