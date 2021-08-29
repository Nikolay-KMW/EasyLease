using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyLease.Repository {
    public class ComfortRepository : RepositoryBase<Comfort>, IComfortRepository {
        public ComfortRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Comfort>> GetAllComfortsAsync(bool trackChanges) =>
             await FindAll(trackChanges).ToListAsync().ConfigureAwait(false);
    }
}