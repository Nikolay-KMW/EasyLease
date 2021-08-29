using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyLease.Repository {
    public class LocationRepository : RepositoryBase<Location>, ILocationRepository {
        public LocationRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Location>> GetAllLocationAsync(bool trackChanges) =>
             await FindAll(trackChanges).OrderBy(Location => Location.Region).ToListAsync().ConfigureAwait(false);
    }
}