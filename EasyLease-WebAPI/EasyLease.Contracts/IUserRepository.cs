using System;
using System.Threading.Tasks;
using EasyLease.Entities.Models;
using EasyLease.Entities.RequestFeatures;

namespace EasyLease.Contracts {
    public interface IUserRepository {
        Task<User> GetUserAsync(Guid userId, bool trackChanges);
        Task<User> GetUserWhitFavoriteAdvertsAsync(Guid userId, bool trackChanges);
        Task<(PagedList<Advert>, User user)> GetFavoriteAdvertsForUserAsync(Guid userId, AdvertParameters advertParameters, bool trackChanges);
        void UpdateProfile(User user);
        void AddAdvertToFavorites(User user, Guid advertId);
        void DeleteAdvertFromFavorites(User user, Guid advertId);
    }
}