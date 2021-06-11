using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;

namespace EasyLease.Repository {
    public class UserRepository : RepositoryBase<User>, IUserRepository {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext) {
        }
    }
}