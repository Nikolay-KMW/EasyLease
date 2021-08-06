using System;
using EasyLease.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>> {
        private Guid adminUserId = new Guid("7778A881-221A-1E7D-A208-0118CCC088E7");
        private Guid adminRoleId = new Guid("7771D881-221A-1E7D-B208-0118DCC088E1");

        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder) {
            builder.HasData(
                new IdentityUserRole<Guid> {
                    UserId = adminUserId,
                    RoleId = adminRoleId,
                },
                new IdentityUserRole<Guid> {
                    UserId = new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870"),
                    RoleId = new Guid("6D9B7113-A8F8-6035-99A7-A20DD400F6A3"),
                },
                new IdentityUserRole<Guid> {
                    UserId = new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                    RoleId = new Guid("6D9B7113-A8F8-6035-99A7-A20DD400F6A3"),
                },
                new IdentityUserRole<Guid> {
                    UserId = new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
                    RoleId = new Guid("6D9B7113-A8F8-6035-99A7-A20DD400F6A3"),
                }
            );
        }
    }
}