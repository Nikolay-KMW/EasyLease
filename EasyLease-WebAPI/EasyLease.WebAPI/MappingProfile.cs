using System;
using System.Linq;
using AutoMapper;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;

namespace EasyLease.WebAPI {
    public class MappingProfile : Profile {
        private const double hoursOffsetForUkraine = 3;
        public MappingProfile() {
            CreateMap<Advert, AdvertsDTO>()
                .ForMember(
                    advertDTO => advertDTO.FullAddress,
                    config => config.MapFrom(advert => string.Format($"{advert.Region} обл., {advert.District} р-н., {advert.SettlementTypeId} {advert.SettlementName}, {advert.StreetTypeId} {advert.StreetName}, дом №{advert.HouseNumber}, кв. №{advert.ApartmentNumber}")))
                .ForMember(advertDTO => advertDTO.AdvertType, config => config.MapFrom(advert => advert.AdvertTypeId))
                .ForMember(advertDTO => advertDTO.CreatedAd, config => config.MapFrom(advert => advert.CreatedAd.AddHours(hoursOffsetForUkraine)))
                .ForMember(advertDTO => advertDTO.Slug, config => config.MapFrom(advert => advert.Id.ToString()))
                .ForMember(advertDTO => advertDTO.ComfortList, config => config.MapFrom(advert => advert.AdvertComforts.Select(advertComforts => advertComforts.ComfortId)))
                .ForMember(advertDTO => advertDTO.TagList, config => config.MapFrom(advert => advert.AdvertTags.Select(advertTags => advertTags.TagId)));
            //.ForMember(advertDTO => advertDTO.Favorited, config => config.MapFrom(advert => advert.AdvertFavorites));

            CreateMap<Advert, AdvertDTO>()
                .ForMember(
                    advertDTO => advertDTO.FullAddress,
                    config => config.MapFrom(advert => string.Format($"{advert.Region} обл., {advert.District} р-н., {advert.SettlementTypeId} {advert.SettlementName}, {advert.StreetTypeId} {advert.StreetName}, дом №{advert.HouseNumber}, кв. №{advert.ApartmentNumber}")))
                .ForMember(advertDTO => advertDTO.AdvertType, config => config.MapFrom(advert => advert.AdvertTypeId))
                .ForMember(advertDTO => advertDTO.CreatedAd, config => config.MapFrom(advert => advert.CreatedAd.AddHours(hoursOffsetForUkraine)))
                .ForMember(advertDTO => advertDTO.Slug, config => config.MapFrom(advert => advert.Id.ToString()))
                .ForMember(advertDTO => advertDTO.ComfortList, config => config.MapFrom(advert => advert.AdvertComforts.Select(advertComforts => advertComforts.ComfortId)))
                .ForMember(advertDTO => advertDTO.TagList, config => config.MapFrom(advert => advert.AdvertTags.Select(advertTags => advertTags.TagId)));
            //.ForMember(advertDTO => advertDTO.Favorited, config => config.MapFrom(advert => advert.AdvertFavorites));

            CreateMap<User, ProfileDTO>()
                .ForMember(
                    profileDTO => profileDTO.UserName,
                    config => config.MapFrom(user => string.Join(' ', user.FirstName, user.SecondName, user.ThirdName)));

            CreateMap<AdvertCreationDTO, Advert>()
                .ForMember(advert => advert.AdvertTypeId, config => config.MapFrom(advertCreationDTO => advertCreationDTO.AdvertType))
                .ForMember(advert => advert.AdvertType, config => config.Ignore())
                .ForMember(advert => advert.SettlementTypeId, config => config.MapFrom(advertCreationDTO => advertCreationDTO.SettlementType))
                .ForMember(advert => advert.SettlementType, config => config.Ignore())
                .ForMember(advert => advert.StreetTypeId, config => config.MapFrom(advertCreationDTO => advertCreationDTO.StreetType))
                .ForMember(advert => advert.StreetType, config => config.Ignore());

            CreateMap<AdvertComfortCreationDTO, AdvertComfort>()
                .ForMember(advertComfort => advertComfort.ComfortId, config => config.MapFrom(adComfortCreationDTO => adComfortCreationDTO.Comfort))
                .ForMember(advertComfort => advertComfort.Comfort, config => config.Ignore());
        }
    }
}