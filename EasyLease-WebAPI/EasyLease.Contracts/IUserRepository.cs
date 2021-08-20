using System;
using System.Threading.Tasks;
using EasyLease.Entities.Models;

namespace EasyLease.Contracts {
    public interface IUserRepository {
        Task<User> GetUserAsync(Guid userId, bool trackChanges);
        Task<User> GetUserWhitAdFavoritesAsync(Guid userId, bool trackChanges);
        void UpdateProfile(User user);
        void AddAdvertToFavorites(User user);
    }
}