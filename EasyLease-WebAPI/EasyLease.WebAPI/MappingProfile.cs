using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                    advertsDTO => advertsDTO.FullAddress,
                    config => config.MapFrom(advert => string.Format($"{advert.Region} обл., {advert.District} р-н., {advert.SettlementTypeId} {advert.SettlementName}, {advert.StreetTypeId} {advert.StreetName}, дом №{advert.HouseNumber}, кв. №{advert.ApartmentNumber}")))
                .ForMember(advertsDTO => advertsDTO.AdvertType, config => config.MapFrom(advert => advert.AdvertTypeId))
                .ForMember(advertsDTO => advertsDTO.CreatedAd, config => config.MapFrom(advert => advert.CreatedAd.AddHours(hoursOffsetForUkraine)))
                .ForMember(advertsDTO => advertsDTO.Slug, config => config.MapFrom(advert => advert.Id.ToString()))
                .ForMember(advertsDTO => advertsDTO.ComfortList, config => config.MapFrom(advert => advert.AdvertComforts.Select(advertComforts => advertComforts.ComfortId)))
                .ForMember(advertsDTO => advertsDTO.TagList, config => config.MapFrom(advert => advert.AdvertTags.Select(advertTags => advertTags.TagId)));
            //.ForMember(advertsDTO => advertsDTO.Favorited, config => config.MapFrom(advert => advert.AdvertFavorites));

            CreateMap<Advert, AdvertDTO>()
                .ForMember(
                    advertDTO => advertDTO.FullAddress,
                    config => config.MapFrom(advert => string.Format($"{advert.Region} обл., {advert.District} р-н., {advert.SettlementTypeId} {advert.SettlementName}, {advert.StreetTypeId} {advert.StreetName}, дом №{advert.HouseNumber}, кв. №{advert.ApartmentNumber}")))
                .ForMember(advertDTO => advertDTO.AdvertType, config => config.MapFrom(advert => advert.AdvertTypeId))
                .ForMember(advertDTO => advertDTO.CreatedAd, config => config.MapFrom(advert => advert.CreatedAd.AddHours(hoursOffsetForUkraine)))
                .ForMember(advertDTO => advertDTO.UpdatedAd, config => config.MapFrom(advert => Convert.ToDateTime(advert.UpdatedAd).AddHours(hoursOffsetForUkraine)))
                .ForMember(advertDTO => advertDTO.StartOfLease, config => config.MapFrom(advert => advert.StartOfLease.AddHours(hoursOffsetForUkraine)))
                .ForMember(advertDTO => advertDTO.EndOfLease, config => config.MapFrom(advert => Convert.ToDateTime(advert.EndOfLease).AddHours(hoursOffsetForUkraine)))
                .ForMember(advertDTO => advertDTO.Images, config => config.MapFrom(advert => advert.Images.Select(images => images.Path)))
                .ForMember(advertDTO => advertDTO.Slug, config => config.MapFrom(advert => advert.Id.ToString()))
                .ForMember(advertDTO => advertDTO.ComfortList, config => config.MapFrom(advert => advert.AdvertComforts.Select(advertComforts => advertComforts.ComfortId)))
                .ForMember(advertDTO => advertDTO.TagList, config => config.MapFrom(advert => advert.AdvertTags.Select(advertTags => advertTags.TagId)));
            //.ForMember(advertDTO => advertDTO.Favorited, config => config.MapFrom(advert => advert.AdvertFavorites));

            CreateMap<User, ProfileDTO>()
                .ForMember(
                    profileDTO => profileDTO.UserName,
                    config => config.MapFrom(user => string.Join(' ', user.FirstName, user.SecondName, user.ThirdName)));

            CreateMap<AdvertCreationDTO, Advert>()
                .ForMember(advert => advert.StartOfLease, config => config.MapFrom(advertCreationDTO => advertCreationDTO.StartOfLease.AddHours(-hoursOffsetForUkraine)))
                .ForMember(advert => advert.EndOfLease, config => config.MapFrom(advertCreationDTO => Convert.ToDateTime(advertCreationDTO.EndOfLease).AddHours(hoursOffsetForUkraine)))
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