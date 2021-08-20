using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyLease.Repository {
    public class AdvertRepository : RepositoryBase<Advert>, IAdvertRepository {
        public AdvertRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Advert>> GetAllAdvertsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .Include(advert => advert.Images)
            .Include(advert => advert.AdvertComforts)
            .Include(advert => advert.AdvertTags)
            .OrderBy(advert => advert.CreatedAd).ToListAsync().ConfigureAwait(false);

        public async Task<Advert> GetAdvertAsync(Guid advertId, bool trackChanges) =>
            await FindByCondition(advert => advert.Id.Equals(advertId), trackChanges)
            .Include(advert => advert.Images)
            .Include(advert => advert.AdvertComforts)
            .Include(advert => advert.AdvertTags)
            .Include(advert => advert.Author)
            .SingleOrDefaultAsync().ConfigureAwait(false);

        public async Task<IEnumerable<Advert>> GetAdvertsForUserAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(advert => advert.UserId.Equals(userId), trackChanges)
            .Include(advert => advert.Images)
            .Include(advert => advert.AdvertComforts)
            .Include(advert => advert.AdvertTags)
            .OrderBy(advert => advert.CreatedAd).ToListAsync().ConfigureAwait(false);

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