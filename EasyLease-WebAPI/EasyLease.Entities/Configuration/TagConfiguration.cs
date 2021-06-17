using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class TagConfiguration : IEntityTypeConfiguration<Tag> {
        public void Configure(EntityTypeBuilder<Tag> builder) {
            builder.HasData(
                new Tag { Id = "квартира" },
                new Tag { Id = "Евро ремонт" },
                new Tag { Id = "дом" },
                new Tag { Id = "1 комнатная" },
                new Tag { Id = "5 комнатная" },
                new Tag { Id = "свежий ремонт" },
                new Tag { Id = "комната" }
            );
        }
    }
}