using System;
using System.Linq;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;
using EasyLease.Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace EasyLease.Repository {
    public class UserRepository : RepositoryBase<User>, IUserRepository {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext) {
        }

        public async Task<User> GetUserAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(user => user.Id.Equals(userId), trackChanges).SingleOrDefaultAsync().ConfigureAwait(false);

        public async Task<User> GetUserWhitFavoriteAdvertsAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(user => user.Id.Equals(userId), trackChanges).Include(user => user.FavoriteAdverts).SingleOrDefaultAsync().ConfigureAwait(false);

        public async Task<(PagedList<Advert>, User user)> GetFavoriteAdvertsForUserAsync(Guid userId, AdvertParameters advertParameters, bool trackChanges) {
            var user = await FindByCondition(user => user.Id.Equals(userId), trackChanges)
            .Include(user => user.FavoriteAdverts)
            .ThenInclude(favoriteAd => favoriteAd.Advert.Images)
            .Include(user => user.FavoriteAdverts)
            .ThenInclude(favoriteAd => favoriteAd.Advert.AdvertComforts)
            .Include(user => user.FavoriteAdverts)
            .ThenInclude(favoriteAd => favoriteAd.Advert.AdvertTags)
            .SingleOrDefaultAsync().ConfigureAwait(false);

            var adverts = user.FavoriteAdverts.Select(favoriteAd => favoriteAd.Advert).OrderBy(advert => advert.CreatedAd);

            return (PagedList<Advert>.ToPagedList(adverts,
                                                  advertParameters.PageNumber,
                                                  advertParameters.PageSize,
                                                  advertParameters.PageOffset), user);
        }

        public void UpdateProfile(User user) {
            user.UpdatedUser = DateTime.UtcNow;
            Update(user);
        }

        public void AddAdvertToFavorites(User user, Guid advertId) {
            bool advertExists = user.FavoriteAdverts.Any(favoriteAdvert => favoriteAdvert.AdvertId == advertId);

            if (!advertExists) {
                user.FavoriteAdverts.Add(new FavoriteAdvert { AdvertId = advertId, UserId = user.Id });
                Update(user);
            }
        }

        public void DeleteAdvertFromFavorites(User user, Guid advertId) {
            var advertToRemove = user.FavoriteAdverts.SingleOrDefault(favoriteAdvert => favoriteAdvert.AdvertId == advertId);

            if (advertToRemove != null) {
                user.FavoriteAdverts.Remove(advertToRemove);
                Update(user);
            }
        }
    }
}