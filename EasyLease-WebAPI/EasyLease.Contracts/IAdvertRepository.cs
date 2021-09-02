using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLease.Entities.Models;
using EasyLease.Entities.RequestFeatures;

namespace EasyLease.Contracts {
    public interface IAdvertRepository {
        Task<PagedList<Advert>> GetAllAdvertsAsync(AdvertParameters advertParameters, bool trackChanges);
        Task<Advert> GetAdvertAsync(Guid advertId, bool trackChanges);
        Task<PagedList<Advert>> GetAdvertsPostedUserAsync(Guid userId, AdvertParameters advertParameters, bool trackChanges);
        void CreateAdvertForUser(Guid userId, Advert advert);
        void UpdateAdvert(Advert advert);
        void DeleteAdvert(Advert advert);
    }
}