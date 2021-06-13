using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class RegionConfiguration : IEntityTypeConfiguration<Region> {
        public void Configure(EntityTypeBuilder<Region> builder) {
            builder.HasData(
                new Region {
                    Id = 1,
                    Name = "Винницкая"
                },
                new Region {
                    Id = 2,
                    Name = "Волынская"
                },
                new Region {
                    Id = 3,
                    Name = "Днепропетровская"
                },
                new Region {
                    Id = 4,
                    Name = "Донецкая"
                },
                new Region {
                    Id = 5,
                    Name = "Житомирская"
                },
                new Region {
                    Id = 6,
                    Name = "Закарпатская"
                },
                new Region {
                    Id = 7,
                    Name = "Запорожская"
                },
                new Region {
                    Id = 8,
                    Name = "Ивано-Франковская"
                },
                new Region {
                    Id = 9,
                    Name = "Киевская"
                },
                new Region {
                    Id = 10,
                    Name = "Кировоградская"
                },
                new Region {
                    Id = 11,
                    Name = "Луганская"
                },
                new Region {
                    Id = 12,
                    Name = "Львовская"
                },
                new Region {
                    Id = 13,
                    Name = "Николаевская"
                },
                new Region {
                    Id = 14,
                    Name = "Одесская"
                },
                new Region {
                    Id = 15,
                    Name = "Полтавская"
                },
                new Region {
                    Id = 16,
                    Name = "Ровненская"
                },
                new Region {
                    Id = 17,
                    Name = "Сумская"
                },
                new Region {
                    Id = 18,
                    Name = "Тернопольская"
                },
                new Region {
                    Id = 19,
                    Name = "Харьковская"
                },
                new Region {
                    Id = 20,
                    Name = "Херсонская"
                },
                new Region {
                    Id = 21,
                    Name = "Хмельницкая"
                },
                new Region {
                    Id = 22,
                    Name = "Черкасская"
                },
                new Region {
                    Id = 23,
                    Name = "Черниговская"
                },
                new Region {
                    Id = 24,
                    Name = "Черновицкая"
                },
                new Region {
                    Id = 25,
                    Name = "Автономная Республика Крым"
                }
            );
        }
    }
}