using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyLease.Repository {
    public class RealtyTypeRepository : RepositoryBase<RealtyType>, IRealtyTypeRepository {
        public RealtyTypeRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<RealtyType>> GetAllRealtyTypeAsync(bool trackChanges) =>
             await FindAll(trackChanges).ToListAsync().ConfigureAwait(false);
    }
}