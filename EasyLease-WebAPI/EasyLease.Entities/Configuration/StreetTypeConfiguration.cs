using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class StreetTypeConfiguration : IEntityTypeConfiguration<StreetType> {
        public void Configure(EntityTypeBuilder<StreetType> builder) {
            builder.HasData(
                new StreetType {
                    Id = "улица"
                },
                new StreetType {
                    Id = "аллея"
                },
                new StreetType {
                    Id = "бульвар"
                },
                new StreetType {
                    Id = "взвоз"
                },
                new StreetType {
                    Id = "въезд"
                },
                new StreetType {
                    Id = "заезд"
                },
                new StreetType {
                    Id = "кольцо"
                },
                new StreetType {
                    Id = "магистраль"
                },
                new StreetType {
                    Id = "набережная"
                },
                new StreetType {
                    Id = "переулок"
                },
                new StreetType {
                    Id = "площадь"
                },
                new StreetType {
                    Id = "проезд"
                },
                new StreetType {
                    Id = "проспект"
                },
                new StreetType {
                    Id = "проулок"
                },
                new StreetType {
                    Id = "разъезд"
                },
                new StreetType {
                    Id = "спуск"
                },
                new StreetType {
                    Id = "съезд"
                },
                new StreetType {
                    Id = "тупик"
                }
            );
        }
    }
}