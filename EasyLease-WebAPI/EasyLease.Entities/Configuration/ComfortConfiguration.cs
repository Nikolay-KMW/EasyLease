using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class ComfortConfiguration : IEntityTypeConfiguration<Comfort> {
        public void Configure(EntityTypeBuilder<Comfort> builder) {
            builder.HasData(
                new Comfort { Id = "Холодильник" },
                new Comfort { Id = "ПК" },
                new Comfort { Id = "Кондиционер" },
                new Comfort { Id = "Телевизор" },
                new Comfort { Id = "Стиральная машина" },
                new Comfort { Id = "Wi-Fi" },
                new Comfort { Id = "Микроволновая печь" },
                new Comfort { Id = "Посуда и столовые приборы" },
                new Comfort { Id = "Плита" },
                new Comfort { Id = "Духовка" },
                new Comfort { Id = "Кофеварка" },
                new Comfort { Id = "Фен" },
                new Comfort { Id = "Утюг" },
                new Comfort { Id = "Переносный обогревавтель" },
                new Comfort { Id = "Кабельное ТВ" },
                new Comfort { Id = "Шкаф" },
                new Comfort { Id = "Чайник" },
                new Comfort { Id = "Морозилка" },
                new Comfort { Id = "Диван" },
                new Comfort { Id = "Кровать" },
                new Comfort { Id = "Кухонная мебель" },
                new Comfort { Id = "Мягкая мебель" },
                new Comfort { Id = "Сушитель белья" },
                new Comfort { Id = "Посудомоющая машина" },
                new Comfort { Id = "Центральное отопление" },
                new Comfort { Id = "Кастрюли" },
                new Comfort { Id = "Сковородки" },
                new Comfort { Id = "Электроплита" }
            );
        }
    }
}