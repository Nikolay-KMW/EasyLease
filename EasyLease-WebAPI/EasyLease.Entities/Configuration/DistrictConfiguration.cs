using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class DistrictConfiguration : IEntityTypeConfiguration<District> {
        public void Configure(EntityTypeBuilder<District> builder) {
            builder.HasData(
                new District {
                    Id = 1,
                    Name = "Винницкая"
                },
                new District {
                    Id = 2,
                    Name = "Волынская"
                },
                new District {
                    Id = 3,
                    Name = "Днепропетровская"
                },
                new District {
                    Id = 4,
                    Name = "Донецкая"
                },
                new District {
                    Id = 5,
                    Name = "Житомирская"
                },
                new District {
                    Id = 6,
                    Name = "Закарпатская"
                },
                new District {
                    Id = 7,
                    Name = "Запорожская"
                },
                new District {
                    Id = 8,
                    Name = "Ивано-Франковская"
                },
                new District {
                    Id = 9,
                    Name = "Киевская"
                },
                new District {
                    Id = 10,
                    Name = "Кировоградская"
                },
                new District {
                    Id = 11,
                    Name = "Луганская"
                },
                new District {
                    Id = 12,
                    Name = "Львовская"
                },
                new District {
                    Id = 13,
                    Name = "Николаевская"
                },
                new District {
                    Id = 14,
                    Name = "Одесская"
                },
                new District {
                    Id = 15,
                    Name = "Полтавская"
                },
                new District {
                    Id = 16,
                    Name = "Ровненская"
                },
                new District {
                    Id = 17,
                    Name = "Сумская"
                },
                new District {
                    Id = 18,
                    Name = "Тернопольская"
                },
                new District {
                    Id = 19,
                    Name = "Харьковская"
                },
                new District {
                    Id = 20,
                    Name = "Херсонская"
                },
                new District {
                    Id = 21,
                    Name = "Хмельницкая"
                },
                new District {
                    Id = 22,
                    Name = "Черкасская"
                },
                new District {
                    Id = 23,
                    Name = "Черниговская"
                },
                new District {
                    Id = 24,
                    Name = "Черновицкая"
                },
                new District {
                    Id = 25,
                    Name = "Автономная Республика Крым"
                }
            );
        }
    }
}