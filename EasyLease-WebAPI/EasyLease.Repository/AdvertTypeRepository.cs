using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyLease.Repository {
    public class AdvertTypeRepository : RepositoryBase<AdvertType>, IAdvertTypeRepository {
        public AdvertTypeRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<AdvertType>> GetAllAdvertTypeAsync(bool trackChanges) =>
             await FindAll(trackChanges).ToListAsync().ConfigureAwait(false);
    }
}