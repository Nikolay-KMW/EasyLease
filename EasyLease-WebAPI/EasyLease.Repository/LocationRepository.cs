using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;

namespace EasyLease.Repository {
    public class LocationRepository : RepositoryBase<Location>, ILocationRepository {
        public LocationRepository(RepositoryContext repositoryContext) : base(repositoryContext) {
        }
    }
}