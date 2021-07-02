using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using AutoMapper;
using EasyLease.Entities.AppSettingsModels;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;

namespace EasyLease.WebAPI {
    public class MappingProfile : Profile {
        private readonly double hoursOffset;

        private string ArrayBytesToJsonSerialize(byte[] source) =>
            source != null ? JsonSerializer.Serialize(source) : null;

        private string BuildFullAddress(Advert advert) =>
            string.Format($"{advert.Region} обл., {advert.District} р-н., {advert.SettlementTypeId} {advert.SettlementName}, {advert.StreetTypeId} {advert.StreetName}, дом №{advert.HouseNumber}, кв. №{advert.ApartmentNumber}");

        private DateTime? ConvertToTimeZone(DateTime? sourceDataTime) =>
            sourceDataTime.HasValue ? (DateTime?)DateTime.SpecifyKind(sourceDataTime.Value, DateTimeKind.Unspecified).AddHours(hoursOffset) : null;

        private DateTime? ConvertToUTC(DateTime? sourceDataTime) =>
            sourceDataTime.HasValue ? (DateTime?)DateTime.SpecifyKind(sourceDataTime.Value, DateTimeKind.Unspecified).AddHours(-hoursOffset) : null;

        public MappingProfile(GeneralSettings generalSettings) {
            hoursOffset = generalSettings.HoursOffsetForUkraine;

            CreateMap<Advert, AdvertsDTO>()
                .ForMember(advertsDTO => advertsDTO.FullAddress, config => config.MapFrom(advert => BuildFullAddress(advert)))
                .ForMember(advertsDTO => advertsDTO.AdvertType, config => config.MapFrom(advert => advert.AdvertTypeId))
                .ForMember(advertsDTO => advertsDTO.CreatedAd, config => config.MapFrom(advert => ConvertToTimeZone(advert.CreatedAd)))
                .ForMember(advertsDTO => advertsDTO.Image, config => config.MapFrom(advert => advert.Images.FirstOrDefault().Path.Replace("\\", "/")))
                .ForMember(advertsDTO => advertsDTO.Slug, config => config.MapFrom(advert => advert.Id.ToString()))
                .ForMember(advertsDTO => advertsDTO.ComfortList, config => config.MapFrom(advert => advert.AdvertComforts.Select(advertComforts => advertComforts.ComfortId)))
                .ForMember(advertsDTO => advertsDTO.TagList, config => config.MapFrom(advert => advert.AdvertTags.Select(advertTags => advertTags.TagId)));
            //.ForMember(advertsDTO => advertsDTO.Favorited, config => config.MapFrom(advert => advert.AdvertFavorites));

            CreateMap<Advert, AdvertDTO>()
                .ForMember(advertDTO => advertDTO.FullAddress, config => config.MapFrom(advert => BuildFullAddress(advert)))
                .ForMember(advertDTO => advertDTO.AdvertType, config => config.MapFrom(advert => advert.AdvertTypeId))
                .ForMember(advertDTO => advertDTO.CreatedAd, config => config.MapFrom(advert => ConvertToTimeZone(advert.CreatedAd)))
                .ForMember(advertDTO => advertDTO.UpdatedAd, config => config.MapFrom(advert => ConvertToTimeZone(advert.UpdatedAd)))
                .ForMember(advertDTO => advertDTO.StartOfLease, config => config.MapFrom(advert => ConvertToTimeZone(advert.StartOfLease)))
                .ForMember(advertDTO => advertDTO.EndOfLease, config => config.MapFrom(advert => ConvertToTimeZone(advert.EndOfLease)))
                .ForMember(advertDTO => advertDTO.Images, config => config.MapFrom(advert => advert.Images.Select(images => images.Path.Replace("\\", "/"))))
                .ForMember(advertDTO => advertDTO.Slug, config => config.MapFrom(advert => advert.Id.ToString()))
                .ForMember(advertDTO => advertDTO.ComfortList, config => config.MapFrom(advert => advert.AdvertComforts.Select(advertComforts => advertComforts.ComfortId)))
                .ForMember(advertDTO => advertDTO.TagList, config => config.MapFrom(advert => advert.AdvertTags.Select(advertTags => advertTags.TagId)));
            //.ForMember(advertDTO => advertDTO.Favorited, config => config.MapFrom(advert => advert.AdvertFavorites));

            CreateMap<User, ProfileDTO>()
                .ForMember(profileDTO => profileDTO.UserName, config => config.MapFrom(user => string.Join(' ', user.FirstName, user.SecondName, user.ThirdName)))
                .ForMember(profileDTO => profileDTO.Image, config => config.MapFrom(user => ArrayBytesToJsonSerialize(user.Avatar)));

            CreateMap<AdvertCreationDTO, Advert>()
                .ForMember(advert => advert.StartOfLease, config => config.MapFrom(advertCreationDTO => ConvertToUTC(advertCreationDTO.StartOfLease)))
                .ForMember(advert => advert.EndOfLease, config => config.MapFrom(advertCreationDTO => ConvertToUTC(advertCreationDTO.EndOfLease)))
                .ForMember(advert => advert.AdvertTypeId, config => config.MapFrom(advertCreationDTO => advertCreationDTO.AdvertType))
                .ForMember(advert => advert.AdvertType, config => config.Ignore())
                .ForMember(advert => advert.SettlementTypeId, config => config.MapFrom(advertCreationDTO => advertCreationDTO.SettlementType))
                .ForMember(advert => advert.SettlementType, config => config.Ignore())
                .ForMember(advert => advert.StreetTypeId, config => config.MapFrom(advertCreationDTO => advertCreationDTO.StreetType))
                .ForMember(advert => advert.StreetType, config => config.Ignore())
                .ForMember(advert => advert.AdvertComforts, config => config.MapFrom(advertCreationDTO => advertCreationDTO.ComfortList))
                .ForMember(advert => advert.AdvertTags, config => config.MapFrom(advertCreationDTO => advertCreationDTO.TagList.Select(tag => new AdvertTag() { TagId = tag, CreatedTag = DateTime.UtcNow })));

            CreateMap<AdvertComfortCreationDTO, AdvertComfort>()
                .ForMember(advertComfort => advertComfort.ComfortId, config => config.MapFrom(adComfortCreationDTO => adComfortCreationDTO.Comfort))
                .ForMember(advertComfort => advertComfort.Comfort, config => config.Ignore());

            CreateMap<AdvertTagCreationDTO, AdvertTag>()
                .ForMember(advertTag => advertTag.TagId, config => config.MapFrom(adTagCreationDTO => adTagCreationDTO.TagList))
                .ForMember(advertTag => advertTag.Tag, config => config.Ignore());
        }
    }
}