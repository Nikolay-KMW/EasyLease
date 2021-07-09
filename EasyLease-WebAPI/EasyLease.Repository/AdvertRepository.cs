using System;
using System.Collections.Generic;
using System.Linq;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyLease.Repository {
    public class AdvertRepository : RepositoryBase<Advert>, IAdvertRepository {
        public AdvertRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<Advert> GetAllAdverts(bool trackChanges) =>
            FindAll(trackChanges)
            .Include(advert => advert.Images)
            .Include(advert => advert.AdvertComforts)
            .Include(advert => advert.AdvertTags)
            .Include(advert => advert.AdvertFavorites)
            .OrderBy(advert => advert.CreatedAd).ToList();

        public Advert GetAdvert(Guid advertId, bool trackChanges) =>
            FindByCondition(advert => advert.Id.Equals(advertId), trackChanges)
            .Include(advert => advert.Images)
            .Include(advert => advert.AdvertComforts)
            .Include(advert => advert.AdvertTags)
            .Include(advert => advert.AdvertFavorites)
            .Include(advert => advert.Author)
            .SingleOrDefault();

        public IEnumerable<Advert> GetAdvertsForUser(Guid userId, bool trackChanges) =>
            FindByCondition(advert => advert.UserId.Equals(userId), trackChanges)
            .Include(advert => advert.Images)
            .Include(advert => advert.AdvertComforts)
            .Include(advert => advert.AdvertTags)
            .Include(advert => advert.AdvertFavorites)
            .OrderBy(advert => advert.CreatedAd).ToList();

        public void CreateAdvertForUser(Guid userId, Advert advert) {
            advert.UserId = userId;
            advert.Status = Status.OnModeration;
            advert.CreatedAd = DateTime.UtcNow;
            Create(advert);
        }

        public void UpdateAdvert(Advert advert) {
            advert.UpdatedAd = DateTime.UtcNow;
            Update(advert);
        }

        public void DeleteAdvert(Advert advert) {
            Delete(advert);
        }
    }
}