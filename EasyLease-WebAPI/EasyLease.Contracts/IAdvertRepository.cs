using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLease.Entities.Models;

namespace EasyLease.Contracts {
    public interface IAdvertRepository {
        Task<IEnumerable<Advert>> GetAllAdvertsAsync(bool trackChanges);
        Task<Advert> GetAdvertAsync(Guid advertId, bool trackChanges);
        Task<IEnumerable<Advert>> GetAdvertsForUserAsync(Guid userId, bool trackChanges);
        void CreateAdvertForUser(Guid userId, Advert advert);
        void UpdateAdvert(Advert advert);
        void DeleteAdvert(Advert advert);
    }
}