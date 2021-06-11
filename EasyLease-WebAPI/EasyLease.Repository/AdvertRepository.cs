using System.Collections.Generic;
using System.Linq;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;

namespace EasyLease.Repository {
    public class AdvertRepository : RepositoryBase<Advert>, IAdvertRepository {
        public AdvertRepository(RepositoryContext repositoryContext) : base(repositoryContext) {
        }

        public IEnumerable<Advert> GetAllAdverts(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(c => c.Title).ToList();
    }
}