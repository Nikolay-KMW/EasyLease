using System;
using System.Collections.Generic;
using System.Linq;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;

namespace EasyLease.Repository {
    public class AdvertRepository : RepositoryBase<Advert>, IAdvertRepository {
        public AdvertRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<Advert> GetAllAdverts(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(advert => advert.CreatedAd).ToList();

        public Advert GetAdvert(Guid advertId, bool trackChanges) =>
            FindByCondition(advert => advert.Id.Equals(advertId), trackChanges).SingleOrDefault();

        public IEnumerable<Advert> GetAdvertsForUser(Guid userId, bool trackChanges) =>
            FindByCondition(advert => advert.UserId.Equals(userId), trackChanges).OrderBy(advert => advert.CreatedAd);

        public void CreateAdvertsForUser(Guid userId, Advert advert) {
            advert.UserId = userId;
            advert.Status = Status.OnModeration;
            advert.CreatedAd = DateTime.UtcNow;
            Create(advert);
        }
    }
}