using System;
using System.Linq;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyLease.Repository {
    public class UserRepository : RepositoryBase<User>, IUserRepository {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext) {
        }

        public async Task<User> GetUserAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(user => user.Id.Equals(userId), trackChanges).SingleOrDefaultAsync().ConfigureAwait(false);

        public async Task<User> GetUserWhitAdFavoritesAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(user => user.Id.Equals(userId), trackChanges).Include(user => user.AdvertFavorites).SingleOrDefaultAsync().ConfigureAwait(false);

        public void UpdateProfile(User user) {
            user.UpdatedUser = DateTime.UtcNow;
            Update(user);
        }

        public void AddAdvertToFavorites(User user) {
            Update(user);
        }
    }
}