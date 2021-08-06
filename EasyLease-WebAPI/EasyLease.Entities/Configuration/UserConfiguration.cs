using System;
using System.Collections;
using EasyLease.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class UserConfiguration : IEntityTypeConfiguration<User> {
        private Guid adminId = new Guid("7778A881-221A-1E7D-A208-0118CCC088E7");

        public void Configure(EntityTypeBuilder<User> builder) {
            User admin = new User {
                Id = adminId,
                UserName = "MasterAdmin",
                NormalizedUserName = "MASTERADMIN",
                FirstName = "Master",
                SecondName = "Admin",
                ThirdName = "",
                Email = "ELAdmin@com.ua",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Biography = "",
                Avatar = null,
                CreatedUser = DateTime.UtcNow,
                UpdatedUser = null,
            };

            admin.PasswordHash = PassGenerate(admin);

            User[] users = new User[] {
                admin,
                new User {
                    Id = new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870"),
                    FirstName = "Максим",
                    SecondName = "",
                    ThirdName = "",
                    Email = "max@com.ua",
                    Biography = "Просто собственник",
                    Avatar = null,
                    CreatedUser = DateTime.UtcNow.AddDays(1),
                    UpdatedUser = null,
                },
                new User {
                    Id = new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                    FirstName = "Nik",
                    Email = "Nik@com.ua",
                    Biography = "",
                    Avatar = null,
                    CreatedUser = DateTime.UtcNow,
                    UpdatedUser = null,
                },
                new User {
                    Id = new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
                    FirstName = "Влад",
                    Email = "vlad@com.ua",
                    Biography = "Занимаюсь сдачей недвижимости",
                    Avatar = null,
                    CreatedUser = DateTime.UtcNow.AddHours(3),
                    UpdatedUser = null,
                }};

            builder.HasData(users);
        }

        public string PassGenerate(User user) {
            var passHash = new PasswordHasher<User>();
            return passHash.HashPassword(user, "AlphaBetaGamma");
        }
    }
}