using System;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class UserConfiguration : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {
            builder.HasData(
                new User {
                    Id = new Guid("c1d4c053-49b6-410c-bc78-2d54a9991870"),
                    UserName = "Максим",
                    Email = "max@com.ua",
                    Biography = "Просто собственник",
                    Image = "",
                    CreatedUser = DateTime.Now,
                    UpdatedUser = null,
                },
                new User {
                    Id = new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"),
                    UserName = "Nik",
                    Email = "Nik@com.ua",
                    Biography = "",
                    Image = "",
                    CreatedUser = DateTime.Now,
                    UpdatedUser = null,
                },
                new User {
                    Id = new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
                    UserName = "Влад",
                    Email = "vlad@com.ua",
                    Biography = "Занимаюсь сдачей недвижимости",
                    Image = "",
                    CreatedUser = DateTime.Now,
                    UpdatedUser = null,
                }
            );
        }
    }
}