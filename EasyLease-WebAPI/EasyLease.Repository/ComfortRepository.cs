using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;

namespace EasyLease.Repository {
    public class ComfortRepository : RepositoryBase<Comfort>, IComfortRepository {
        public ComfortRepository(RepositoryContext repositoryContext) : base(repositoryContext) {
        }
    }
}