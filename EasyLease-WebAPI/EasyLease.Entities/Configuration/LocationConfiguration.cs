using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLease.Entities.Configuration {
    public class LocationConfiguration : IEntityTypeConfiguration<Location> {
        public void Configure(EntityTypeBuilder<Location> builder) {
            builder.HasData(
                new Location {
                    Region = "Винницкая",
                    District = "Винницкий"
                },
                new Location {
                    Region = "Винницкая",
                    District = "Гайсинский",
                },
                new Location {
                    Region = "Винницкая",
                    District = "Жмеринский",
                },
                new Location {
                    Region = "Винницкая",
                    District = "Могилев-Подольский",
                },
                new Location {
                    Region = "Винницкая",
                    District = "Тульчинский",
                },
                new Location {
                    Region = "Винницкая",
                    District = "Хмельницкий",
                },
                //==========================
                new Location {
                    Region = "Волынская",
                    District = "Владимир-Волынский",
                },
                new Location {
                    Region = "Волынская",
                    District = "Камень-Каширский",
                },
                new Location {
                    Region = "Волынская",
                    District = "Ковальский",
                },
                new Location {
                    Region = "Волынская",
                    District = "Луцкий",
                },
                //==========================
                new Location {
                    Region = "Днепропетровская",
                    District = "Днепровский",
                },
                new Location {
                    Region = "Днепропетровская",
                    District = "Каменский",
                },
                new Location {
                    Region = "Днепропетровская",
                    District = "Криворожский",
                },
                new Location {
                    Region = "Днепропетровская",
                    District = "Никопольский",
                },
                new Location {
                    Region = "Днепропетровская",
                    District = "Новомосковский",
                },
                new Location {
                    Region = "Днепропетровская",
                    District = "Павлоградский",
                },
                new Location {
                    Region = "Днепропетровская",
                    District = "Синельниковский",
                },
                //==========================
                new Location {
                    Region = "Донецкая",
                    District = "Бахмутский",
                },
                new Location {
                    Region = "Донецкая",
                    District = "Волновахский",
                },
                new Location {
                    Region = "Донецкая",
                    District = "Горловский",
                },
                new Location {
                    Region = "Донецкая",
                    District = "Донецкий",
                },
                new Location {
                    Region = "Донецкая",
                    District = "Кальмиусский",
                },
                new Location {
                    Region = "Донецкая",
                    District = "Краматорский",
                },
                new Location {
                    Region = "Донецкая",
                    District = "Мариупольский",
                },
                new Location {
                    Region = "Донецкая",
                    District = "Покровский",
                },
                //==========================
                new Location {
                    Region = "Житомирская",
                    District = "Бердичевский",
                },
                new Location {
                    Region = "Житомирская",
                    District = "Житомирский",
                },
                new Location {
                    Region = "Житомирская",
                    District = "Коростенский",
                },
                new Location {
                    Region = "Житомирская",
                    District = "Новоград-Волынский",
                },
                //==========================
                new Location {
                    Region = "Закарпатская",
                    District = "Береговский",
                },
                new Location {
                    Region = "Закарпатская",
                    District = "Мукачевский",
                },
                new Location {
                    Region = "Закарпатская",
                    District = "Раховский",
                },
                new Location {
                    Region = "Закарпатская",
                    District = "Тячевский",
                },
                new Location {
                    Region = "Закарпатская",
                    District = "Ужгородский",
                },
                new Location {
                    Region = "Закарпатская",
                    District = "Хустский",
                },
                //==========================
                new Location {
                    Region = "Запорожская",
                    District = "Бердянский",
                },
                new Location {
                    Region = "Запорожская",
                    District = "Васильевский",
                },
                new Location {
                    Region = "Запорожская",
                    District = "Запорожский",
                },
                new Location {
                    Region = "Запорожская",
                    District = "Мелитопольский",
                },
                new Location {
                    Region = "Запорожская",
                    District = "Пологовский",
                },
                //==========================
                new Location {
                    Region = "Ивано-Франковская",
                    District = "Верховинский",
                },
                new Location {
                    Region = "Ивано-Франковская",
                    District = "Ивано-Франковский",
                },
                new Location {
                    Region = "Ивано-Франковская",
                    District = "Калушский",
                },
                new Location {
                    Region = "Ивано-Франковская",
                    District = "Коломыйский",
                },
                new Location {
                    Region = "Ивано-Франковская",
                    District = "Косовский",
                },
                new Location {
                    Region = "Ивано-Франковская",
                    District = "Надворнянский",
                },
                //==========================
                new Location {
                    Region = "Киевская",
                    District = "Белоцерковский",
                },
                new Location {
                    Region = "Киевская",
                    District = "Бориспольский",
                },
                new Location {
                    Region = "Киевская",
                    District = "Броварской",
                },
                new Location {
                    Region = "Киевская",
                    District = "Бучанский",
                },
                new Location {
                    Region = "Киевская",
                    District = "Вышгородский",
                },
                new Location {
                    Region = "Киевская",
                    District = "Обуховский",
                },
                new Location {
                    Region = "Киевская",
                    District = "Фастовский",
                },
                //==========================
                new Location {
                    Region = "Кировоградская",
                    District = "Головановский",
                },
                new Location {
                    Region = "Кировоградская",
                    District = "Кропивницкий",
                },
                new Location {
                    Region = "Кировоградская",
                    District = "Новоукраинский",
                },
                new Location {
                    Region = "Кировоградская",
                    District = "Александрийский",
                },
                //==========================
                new Location {
                    Region = "Луганская",
                    District = "Алчевский",
                },
                new Location {
                    Region = "Луганская",
                    District = "Довжанский",
                },
                new Location {
                    Region = "Луганская",
                    District = "Луганский",
                },
                new Location {
                    Region = "Луганская",
                    District = "Ровеньковский",
                },
                new Location {
                    Region = "Луганская",
                    District = "Сватовский",
                },
                new Location {
                    Region = "Луганская",
                    District = "Северодонецкий",
                },
                new Location {
                    Region = "Луганская",
                    District = "Старобельский",
                },
                new Location {
                    Region = "Луганская",
                    District = "Счастьенский",
                },
                //==========================
                new Location {
                    Region = "Львовская",
                    District = "Дрогобычский",
                },
                new Location {
                    Region = "Львовская",
                    District = "Золочевский",
                },
                new Location {
                    Region = "Львовская",
                    District = "Львовский",
                },
                new Location {
                    Region = "Львовская",
                    District = "Самборский",
                },
                new Location {
                    Region = "Львовская",
                    District = "Стрыйский ",
                },
                new Location {
                    Region = "Львовская",
                    District = "Червоноградский",
                },
                new Location {
                    Region = "Львовская",
                    District = "Яворовский",
                },
                //==========================
                new Location {
                    Region = "Николаевская",
                    District = "Баштанский",
                },
                new Location {
                    Region = "Николаевская",
                    District = "Вознесенский",
                },
                new Location {
                    Region = "Николаевская",
                    District = "Николаевский",
                },
                new Location {
                    Region = "Николаевская",
                    District = "Первомайский",
                },
                //==========================
                new Location {
                    Region = "Одесская",
                    District = "Березовский",
                },
                new Location {
                    Region = "Одесская",
                    District = "Белгород-Днестровский",
                },
                new Location {
                    Region = "Одесская",
                    District = "Болградский",
                },
                new Location {
                    Region = "Одесская",
                    District = "Измаильский",
                },
                new Location {
                    Region = "Одесская",
                    District = "Одесский",
                },
                new Location {
                    Region = "Одесская",
                    District = "Подольский",
                },
                new Location {
                    Region = "Одесская",
                    District = "Раздельнянский",
                },
                //==========================
                new Location {
                    Region = "Полтавская",
                    District = "Кременчугский",
                },
                new Location {
                    Region = "Полтавская",
                    District = "Лубенский",
                },
                new Location {
                    Region = "Полтавская",
                    District = "Миргородский",
                },
                new Location {
                    Region = "Полтавская",
                    District = "Полтавский",
                },
                //==========================
                new Location {
                    Region = "Ровенская",
                    District = "Вараский",
                },
                new Location {
                    Region = "Ровенская",
                    District = "Дубенский",
                },
                new Location {
                    Region = "Ровенская",
                    District = "Ровенский",
                },
                new Location {
                    Region = "Ровенская",
                    District = "Сарненский",
                },
                //==========================
                new Location {
                    Region = "Сумская",
                    District = "Конотопский"
                },
                new Location {
                    Region = "Сумская",
                    District = "Ахтырский",
                },
                new Location {
                    Region = "Сумская",
                    District = "Роменский",
                },
                new Location {
                    Region = "Сумская",
                    District = "Сумской",
                },
                new Location {
                    Region = "Сумская",
                    District = "Шосткинский",
                },
                //==========================
                new Location {
                    Region = "Тернопольская",
                    District = "Кременецкий",
                },
                new Location {
                    Region = "Тернопольская",
                    District = "Тернопольский",
                },
                new Location {
                    Region = "Тернопольская",
                    District = "Чертковский",
                },
                //==========================
                new Location {
                    Region = "Харьковская",
                    District = "Богодуховский",
                },
                new Location {
                    Region = "Харьковская",
                    District = "Изюмский",
                },
                new Location {
                    Region = "Харьковская",
                    District = "Красноградский",
                },
                new Location {
                    Region = "Харьковская",
                    District = "Купянский",
                },
                new Location {
                    Region = "Харьковская",
                    District = "Лозовский",
                },
                new Location {
                    Region = "Харьковская",
                    District = "Харьковский",
                },
                new Location {
                    Region = "Харьковская",
                    District = "Чугуевский",
                },
                //==========================
                new Location {
                    Region = "Херсонская",
                    District = "Бериславский",
                },
                new Location {
                    Region = "Херсонская",
                    District = "Генический",
                },
                new Location {
                    Region = "Херсонская",
                    District = "Каховский",
                },
                new Location {
                    Region = "Херсонская",
                    District = "Скадовский",
                },
                new Location {
                    Region = "Херсонская",
                    District = "Херсонский",
                },
                //==========================
                new Location {
                    Region = "Хмельницкая",
                    District = "Каменец-Подольский",
                },
                new Location {
                    Region = "Хмельницкая",
                    District = "Хмельницкий",
                },
                new Location {
                    Region = "Хмельницкая",
                    District = "Шепетовский",
                },
                //==========================
                new Location {
                    Region = "Черкасская",
                    District = "Звенигородский",
                },
                new Location {
                    Region = "Черкасская",
                    District = "Золотоношский",
                },
                new Location {
                    Region = "Черкасская",
                    District = "Уманский",
                },
                new Location {
                    Region = "Черкасская",
                    District = "Черкасский",
                },
                //==========================
                new Location {
                    Region = "Черновицкая",
                    District = "Вижницкий",
                },
                new Location {
                    Region = "Черновицкая",
                    District = "Днестровский",
                },
                new Location {
                    Region = "Черновицкая",
                    District = "Черновицкий",
                },
                //==========================
                new Location {
                    Region = "Черниговская",
                    District = "Корюковский",
                },
                new Location {
                    Region = "Черниговская",
                    District = "Нежинский",
                },
                new Location {
                    Region = "Черниговская",
                    District = "Новгород-Северский",
                },
                new Location {
                    Region = "Черниговская",
                    District = "Прилуцкий",
                },
                new Location {
                    Region = "Черниговская",
                    District = "Черниговский",
                },
                //==========================
                new Location {
                    Region = "Автономная республика Крым",
                    District = "Бахчисарайский",
                },
                new Location {
                    Region = "Автономная республика Крым",
                    District = "Белогорский",
                },
                new Location {
                    Region = "Автономная республика Крым",
                    District = "Джанкойский",
                },
                new Location {
                    Region = "Автономная республика Крым",
                    District = "Евпаторийский",
                },
                new Location {
                    Region = "Автономная республика Крым",
                    District = "Керченский",
                },
                new Location {
                    Region = "Автономная республика Крым",
                    District = "Курманский",
                },
                new Location {
                    Region = "Автономная республика Крым",
                    District = "Перекопский",
                },
                new Location {
                    Region = "Автономная республика Крым",
                    District = "Симферопольский",
                },
                new Location {
                    Region = "Автономная республика Крым",
                    District = "Феодосийский",
                },
                new Location {
                    Region = "Автономная республика Крым",
                    District = "Ялтинский",
                }
            );
        }
    }
}