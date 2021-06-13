using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class DistrictConfiguration : IEntityTypeConfiguration<District> {
        public void Configure(EntityTypeBuilder<District> builder) {
            builder.HasData(
                new District {
                    Id = 1,
                    Name = "Сумской"
                },
                new District {
                    Id = 2,
                    Name = "Конотопский"
                },
                new District {
                    Id = 3,
                    Name = "Ахтырский"
                },
                new District {
                    Id = 4,
                    Name = "Роменский"
                },
                new District {
                    Id = 5,
                    Name = "Шосткинский"
                },
                new District {
                    Id = 6,
                    Name = "Корюковский"
                },
                new District {
                    Id = 7,
                    Name = "Нежинский"
                },
                new District {
                    Id = 8,
                    Name = "Новгород-Северский"
                },
                new District {
                    Id = 9,
                    Name = "Прилуцкий"
                },
                new District {
                    Id = 10,
                    Name = "Черниговский"
                }
            );
        }
    }
}