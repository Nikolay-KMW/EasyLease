using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;

namespace EasyLease.Repository {
    public class TagRepository : RepositoryBase<Tag>, ITagRepository {
        public TagRepository(RepositoryContext repositoryContext) : base(repositoryContext) {
        }
    }
}