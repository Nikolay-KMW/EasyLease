using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;
using EasyLease.Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace EasyLease.Repository {
    public class AdvertRepository : RepositoryBase<Advert>, IAdvertRepository {
        public AdvertRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<PagedList<Advert>> GetAllAdvertsAsync(AdvertParameters advertParameters, bool trackChanges) {
            var adverts = await FindAll(trackChanges)
            .Include(advert => advert.Images)
            .Include(advert => advert.AdvertComforts)
            .Include(advert => advert.AdvertTags)
            .OrderBy(advert => advert.CreatedAd)
            .Skip(advertParameters.PageOffset)
            .Take(advertParameters.PageSize)
            .ToListAsync().ConfigureAwait(false);

            var count = await FindAll(trackChanges: false).CountAsync().ConfigureAwait(false);

            return new PagedList<Advert>(adverts, count, advertParameters.PageNumber, advertParameters.PageSize);
        }

        public async Task<Advert> GetAdvertAsync(Guid advertId, bool trackChanges) =>
            await FindByCondition(advert => advert.Id.Equals(advertId), trackChanges)
            .Include(advert => advert.Images)
            .Include(advert => advert.AdvertComforts)
            .Include(advert => advert.AdvertTags)
            .Include(advert => advert.Author)
            .SingleOrDefaultAsync().ConfigureAwait(false);

        public async Task<PagedList<Advert>> GetAdvertsPostedUserAsync(Guid userId, AdvertParameters advertParameters, bool trackChanges) {
            var adverts = await FindByCondition(advert => advert.UserId.Equals(userId), trackChanges)
            .Include(advert => advert.Images)
            .Include(advert => advert.AdvertComforts)
            .Include(advert => advert.AdvertTags)
            .OrderBy(advert => advert.CreatedAd)
            .Skip(advertParameters.PageOffset)
            .Take(advertParameters.PageSize)
            .ToListAsync().ConfigureAwait(false);

            var count = await FindByCondition(advert => advert.UserId.Equals(userId), trackChanges: false).CountAsync().ConfigureAwait(false);

            return new PagedList<Advert>(adverts, count, advertParameters.PageNumber, advertParameters.PageSize);
        }

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