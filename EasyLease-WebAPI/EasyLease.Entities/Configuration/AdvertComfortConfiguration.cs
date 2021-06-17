using System;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class AdvertComfortConfiguration : IEntityTypeConfiguration<AdvertComfort> {
        public void Configure(EntityTypeBuilder<AdvertComfort> builder) {
            builder.HasData(

                new AdvertComfort {
                    AdvertId = new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
                    ComfortId = "Холодильник",
                },
                new AdvertComfort {
                    AdvertId = new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
                    ComfortId = "Кондиционер",
                },
                new AdvertComfort {
                    AdvertId = new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
                    ComfortId = "Телевизор",
                },
                new AdvertComfort {
                    AdvertId = new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"),
                    ComfortId = "Холодильник",
                },
                new AdvertComfort {
                    AdvertId = new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"),
                    ComfortId = "Посуда и столовые приборы",
                },
                new AdvertComfort {
                    AdvertId = new Guid("a2d4c053-49b6-410c-bc78-2d54a9991870"),
                    ComfortId = "Духовка",
                },
                new AdvertComfort {
                    AdvertId = new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"),
                    ComfortId = "Кастрюли",
                },
                new AdvertComfort {
                    AdvertId = new Guid("a3d4c053-49b6-410c-bc78-2d54a9991870"),
                    ComfortId = "Диван",
                },
                new AdvertComfort {
                    AdvertId = new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"),
                    ComfortId = "Кухонная мебель",
                },
                new AdvertComfort {
                    AdvertId = new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"),
                    ComfortId = "Кофеварка",
                },
                new AdvertComfort {
                    AdvertId = new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"),
                    ComfortId = "Мягкая мебель",
                },
                new AdvertComfort {
                    AdvertId = new Guid("a4d4c053-49b6-410c-bc78-2d54a9991870"),
                    ComfortId = "Wi-Fi",
                },
                new AdvertComfort {
                    AdvertId = new Guid("a5d4c053-49b6-410c-bc78-2d54a9991870"),
                    ComfortId = "Чайник",
                }
            );
        }
    }
}