using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class StreetTypeConfiguration : IEntityTypeConfiguration<Street> {
        public void Configure(EntityTypeBuilder<Street> builder) {
            builder.HasData(
                new Street {
                    Id = 1,
                    Name = "улица"
                },
                new Street {
                    Id = 2,
                    Name = "аллея"
                },
                new Street {
                    Id = 3,
                    Name = "бульвар"
                },
                new Street {
                    Id = 4,
                    Name = "взвоз"
                },
                new Street {
                    Id = 5,
                    Name = "въезд"
                },
                new Street {
                    Id = 6,
                    Name = "заезд"
                },
                new Street {
                    Id = 7,
                    Name = "кольцо"
                },
                new Street {
                    Id = 8,
                    Name = "магистраль"
                },
                new Street {
                    Id = 9,
                    Name = "набережная"
                },
                new Street {
                    Id = 10,
                    Name = "переулок"
                },
                new Street {
                    Id = 11,
                    Name = "площадь"
                },
                new Street {
                    Id = 12,
                    Name = "проезд"
                },
                new Street {
                    Id = 13,
                    Name = "проспект"
                },
                new Street {
                    Id = 14,
                    Name = "проулок"
                },
                new Street {
                    Id = 15,
                    Name = "разъезд"
                },
                new Street {
                    Id = 16,
                    Name = "спуск"
                },
                new Street {
                    Id = 17,
                    Name = "съезд"
                },
                new Street {
                    Id = 18,
                    Name = "тупик"
                }
            );
        }
    }
}