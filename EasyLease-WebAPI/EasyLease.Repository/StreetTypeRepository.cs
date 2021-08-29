using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyLease.Repository {
    public class StreetTypeRepository : RepositoryBase<StreetType>, IStreetTypeRepository {
        public StreetTypeRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<StreetType>> GetAllStreetTypeAsync(bool trackChanges) =>
             await FindAll(trackChanges).ToListAsync().ConfigureAwait(false);
    }
}
