using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyLease.Repository {
    public class SettlementTypeRepository : RepositoryBase<SettlementType>, ISettlementTypeRepository {
        public SettlementTypeRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<SettlementType>> GetAllSettlementTypeAsync(bool trackChanges) =>
             await FindAll(trackChanges).ToListAsync().ConfigureAwait(false);
    }
}