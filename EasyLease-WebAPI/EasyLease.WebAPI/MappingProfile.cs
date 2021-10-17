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

        private string ArrayBytesToBase64String(byte[] source) =>
            source != null ? Convert.ToBase64String(source, Base64FormattingOptions.InsertLineBreaks) : null;

        private string BuildFullUserName(User user) =>
            string.Join(' ', user.FirstName, user.SecondName, user.ThirdName);

        private string BuildFullAddress(Advert advert) =>
            string.Format($"{advert.Region} обл., {advert.District} р-н., {advert.SettlementTypeId} {advert.SettlementName}, {advert.StreetTypeId} {advert.StreetName}, дом №{advert.HouseNumber}, кв. №{advert.ApartmentNumber}");

        private DateTime? ConvertToTimeZone(DateTime? sourceDataTime) =>
            sourceDataTime.HasValue ? (DateTime?)DateTime.SpecifyKind(sourceDataTime.Value, DateTimeKind.Unspecified).AddHours(hoursOffset) : null;

        private DateTime? ConvertToUTC(DateTime? sourceDataTime) =>
            sourceDataTime.HasValue ? (DateTime?)DateTime.SpecifyKind(sourceDataTime.Value, DateTimeKind.Unspecified).AddHours(-hoursOffset) : null;

        private bool SetFavorite(Advert advert, ResolutionContext context) {
            if (context.Options.Items["favoriteAdverts"] is List<FavoriteAdvert> favoriteAdverts) {
                foreach (var favoriteAdvert in favoriteAdverts) {
                    if (favoriteAdvert.AdvertId == advert.Id) {
                        return true;
                    }
                }
            }
            return false;
        }

        private AdvertLocationDTO[] GetAdvertLocationDTO(IEnumerable<Location> locations) {
            List<AdvertLocationDTO> locationsDTO = new List<AdvertLocationDTO>();

            string region = null;
            foreach (var location in locations) {
                if (region != location.Region) {
                    locationsDTO.Add(new AdvertLocationDTO() { Region = location.Region });
                    region = location.Region;
                }
                if (region == location.Region) {
                    locationsDTO.Last().District.Add(location.District);
                }
            }

            return locationsDTO.ToArray<AdvertLocationDTO>();
        }

        public MappingProfile(GeneralSettings generalSettings) {
            hoursOffset = generalSettings.HoursOffsetForUkraine;

            CreateMap<Advert, AdvertsDTO>()
                .ForMember(advertsDTO => advertsDTO.FullAddress, config => config.MapFrom(advert => BuildFullAddress(advert)))
                .ForMember(advertsDTO => advertsDTO.RealtyType, config => config.MapFrom(advert => advert.RealtyTypeId))
                .ForMember(advertsDTO => advertsDTO.CreatedAd, config => config.MapFrom(advert => ConvertToTimeZone(advert.CreatedAd)))
                .ForMember(advertsDTO => advertsDTO.Image, config => config.MapFrom(advert => advert.Images.FirstOrDefault()))
                .ForMember(advertsDTO => advertsDTO.Slug, config => config.MapFrom(advert => advert.Id.ToString()))
                .ForMember(advertsDTO => advertsDTO.ComfortList, config => config.MapFrom(advert => advert.AdvertComforts.Select(advertComforts => advertComforts.ComfortId)))
                .ForMember(advertsDTO => advertsDTO.TagList, config => config.MapFrom(advert => advert.AdvertTags.Select(advertTags => advertTags.TagId)))
                .ForMember(advertsDTO => advertsDTO.Favorited, config => config.MapFrom((advert, advertsDTO, _, context) => SetFavorite(advert, context)));

            CreateMap<Advert, AdvertDTO>()
                .ForMember(advertDTO => advertDTO.RealtyType, config => config.MapFrom(advert => advert.RealtyTypeId))
                .ForMember(advertDTO => advertDTO.SettlementType, config => config.MapFrom(advert => advert.SettlementTypeId))
                .ForMember(advertDTO => advertDTO.StreetType, config => config.MapFrom(advert => advert.StreetTypeId))
                .ForMember(advertDTO => advertDTO.CreatedAd, config => config.MapFrom(advert => ConvertToTimeZone(advert.CreatedAd)))
                .ForMember(advertDTO => advertDTO.UpdatedAd, config => config.MapFrom(advert => ConvertToTimeZone(advert.UpdatedAd)))
                .ForMember(advertDTO => advertDTO.StartOfLease, config => config.MapFrom(advert => ConvertToTimeZone(advert.StartOfLease)))
                .ForMember(advertDTO => advertDTO.EndOfLease, config => config.MapFrom(advert => ConvertToTimeZone(advert.EndOfLease)))
                .ForMember(advertDTO => advertDTO.Slug, config => config.MapFrom(advert => advert.Id.ToString()))
                .ForMember(advertDTO => advertDTO.ComfortList, config => config.MapFrom(advert => advert.AdvertComforts.Select(advertComforts => advertComforts.ComfortId)))
                .ForMember(advertDTO => advertDTO.TagList, config => config.MapFrom(advert => advert.AdvertTags.Select(advertTags => advertTags.TagId)))
                .ForMember(advertDTO => advertDTO.Favorited, config => config.MapFrom((advert, advertsDTO, _, context) => SetFavorite(advert, context)));

            CreateMap<Image, ImageDTO>()
                .ForMember(imageDTO => imageDTO.Path, config => config.MapFrom(image => image.Path.Replace("\\", "/")));

            CreateMap<AdvertCreationDTO, Advert>()
                .ForMember(advert => advert.StartOfLease, config => config.MapFrom(advertCreationDTO => ConvertToUTC(advertCreationDTO.StartOfLease)))
                .ForMember(advert => advert.EndOfLease, config => config.MapFrom(advertCreationDTO => ConvertToUTC(advertCreationDTO.EndOfLease)))
                .ForMember(advert => advert.RealtyTypeId, config => config.MapFrom(advertCreationDTO => advertCreationDTO.RealtyType))
                .ForMember(advert => advert.RealtyType, config => config.Ignore())
                .ForMember(advert => advert.SettlementTypeId, config => config.MapFrom(advertCreationDTO => advertCreationDTO.SettlementType))
                .ForMember(advert => advert.SettlementType, config => config.Ignore())
                .ForMember(advert => advert.StreetTypeId, config => config.MapFrom(advertCreationDTO => advertCreationDTO.StreetType))
                .ForMember(advert => advert.StreetType, config => config.Ignore())
                .ForMember(advert => advert.AdvertComforts, config => config.MapFrom(advertCreationDTO => advertCreationDTO.ComfortList.Select(comfort => new AdvertComfort() { ComfortId = comfort })))
                .ForMember(advert => advert.AdvertTags, config => config.MapFrom(advertCreationDTO => advertCreationDTO.TagList.Select(tag => new AdvertTag() { TagId = tag, CreatedTag = DateTime.UtcNow })));

            CreateMap<AdvertUpdateDTO, Advert>()
                .ForMember(advert => advert.StartOfLease, config => config.MapFrom(advertUpdateDTO => ConvertToUTC(advertUpdateDTO.StartOfLease)))
                .ForMember(advert => advert.EndOfLease, config => config.MapFrom(advertUpdateDTO => ConvertToUTC(advertUpdateDTO.EndOfLease)))
                .ForMember(advert => advert.RealtyTypeId, config => config.MapFrom(advertUpdateDTO => advertUpdateDTO.RealtyType))
                .ForMember(advert => advert.RealtyType, config => config.Ignore())
                .ForMember(advert => advert.SettlementTypeId, config => config.MapFrom(advertUpdateDTO => advertUpdateDTO.SettlementType))
                .ForMember(advert => advert.SettlementType, config => config.Ignore())
                .ForMember(advert => advert.StreetTypeId, config => config.MapFrom(advertUpdateDTO => advertUpdateDTO.StreetType))
                .ForMember(advert => advert.StreetType, config => config.Ignore())
                .ForMember(advert => advert.AdvertComforts, config => config.MapFrom(advertUpdateDTO => advertUpdateDTO.ComfortList.Select(comfort => new AdvertComfort() { ComfortId = comfort })))
                .ForMember(advert => advert.AdvertTags, config => config.MapFrom(advertUpdateDTO => advertUpdateDTO.TagList.Select(tag => new AdvertTag() { TagId = tag, CreatedTag = DateTime.UtcNow })));

            CreateMap<AdvertAdditionalDataIDTO, AdvertAdditionalDataDTO>()
                .ForMember(additionalDataDTO => additionalDataDTO.RealtyType, config => config.MapFrom(additionalDataIDTO => additionalDataIDTO.RealtyType.Select(realtyType => realtyType.Id)))
                .ForMember(additionalDataDTO => additionalDataDTO.SettlementType, config => config.MapFrom(additionalDataIDTO => additionalDataIDTO.SettlementType.Select(settlementType => settlementType.Id)))
                .ForMember(additionalDataDTO => additionalDataDTO.StreetType, config => config.MapFrom(additionalDataIDTO => additionalDataIDTO.StreetType.Select(streetType => streetType.Id)))
                .ForMember(additionalDataDTO => additionalDataDTO.Locations, config => config.MapFrom(additionalDataIDTO => GetAdvertLocationDTO(additionalDataIDTO.Locations)))
                .ForMember(additionalDataDTO => additionalDataDTO.Comforts, config => config.MapFrom(additionalDataIDTO => additionalDataIDTO.Comforts.Select(comfort => comfort.Id)));

            CreateMap<User, ProfileDTO>()
                .ForMember(profileDTO => profileDTO.UserName, config => config.MapFrom(user => BuildFullUserName(user)))
                .ForMember(profileDTO => profileDTO.Bio, config => config.MapFrom(user => user.Biography))
                .ForMember(profileDTO => profileDTO.CreatedUser, config => config.MapFrom(user => user.CreatedUser.ToShortDateString()))
                .ForMember(profileDTO => profileDTO.VisitedUser, config => config.MapFrom(user => ConvertToTimeZone(user.VisitedUser)))
                .ForMember(profileDTO => profileDTO.Image, config => config.MapFrom(user => ArrayBytesToBase64String(user.Avatar)));

            CreateMap<User, UserDTO>()
                .ForMember(userDTO => userDTO.UserName, config => config.MapFrom(user => BuildFullUserName(user)))
                .ForMember(userDTO => userDTO.Bio, config => config.MapFrom(user => user.Biography))
                .ForMember(userDTO => userDTO.VisitedUser, config => config.MapFrom(user => ConvertToTimeZone(user.VisitedUser)))
                .ForMember(userDTO => userDTO.Image, config => config.MapFrom(user => ArrayBytesToBase64String(user.Avatar)));

            CreateMap<ProfileUpdateDTO, User>()
                .ForMember(user => user.Biography, config => config.MapFrom(profileUpdateDTO => profileUpdateDTO.Bio));

            CreateMap<UserRegistrationDTO, User>()
                .ForMember(user => user.FirstName, config => config.MapFrom(userRegistrationDTO => userRegistrationDTO.UserName))
                .ForMember(user => user.CreatedUser, config => config.MapFrom(_ => DateTime.UtcNow))
                .ForMember(user => user.VisitedUser, config => config.MapFrom(_ => DateTime.UtcNow));
        }
    }
}