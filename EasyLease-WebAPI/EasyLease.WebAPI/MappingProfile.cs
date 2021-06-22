using System;
using System.Linq;
using AutoMapper;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;

namespace EasyLease.WebAPI {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<Advert, AdvertDTO>()
                .ForMember(
                    advertDTO => advertDTO.FullAddress,
                    config => config.MapFrom(advert => string.Format($"{advert.Region} обл., {advert.District} р-н., {advert.SettlementTypeId} {advert.SettlementName}, {advert.StreetTypeId} {advert.StreetName}, дом №{advert.HouseNumber}, кв. №{advert.ApartmentNumber}")))
                .ForMember(advertDTO => advertDTO.TagList, config => config.MapFrom(advert => advert.AdvertTags));
            //.ForMember(advertDTO => advertDTO.Favorited, config => config.MapFrom(advert => advert.AdvertFavorites));

            CreateMap<User, ProfileDTO>()
                .ForMember(
                    profileDTO => profileDTO.UserName,
                    config => config.MapFrom(user => string.Join(' ', user.FirstName, user.SecondName, user.ThirdName)));
        }
    }
}