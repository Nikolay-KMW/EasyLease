using System;
using System.Collections.Generic;
using EasyLease.Entities.Models;

namespace EasyLease.Contracts {
    public interface IAdvertRepository {
        IEnumerable<Advert> GetAllAdverts(bool trackChanges);
        Advert GetAdvert(Guid advertId, bool trackChanges);
        IEnumerable<Advert> GetAdvertsForUser(Guid userId, bool trackChanges);
        void CreateAdvertForUser(Guid userId, Advert advert);
        void UpdateAdvertPhotoForUser(Advert advert);
    }
}