using System;
using System.Linq;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;

namespace EasyLease.Repository {
    public class UserRepository : RepositoryBase<User>, IUserRepository {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext) {
        }

        public User GetUser(Guid userId, bool trackChanges) =>
            FindByCondition(user => user.Id.Equals(userId), trackChanges).SingleOrDefault();

        public void UpdateProfile(User user) {
            user.UpdatedUser = DateTime.UtcNow;
            Update(user);
        }
    }
}