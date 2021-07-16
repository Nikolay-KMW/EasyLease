using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyLease.Repository {
    public class TagRepository : RepositoryBase<Tag>, ITagRepository {
        public TagRepository(RepositoryContext repositoryContext) : base(repositoryContext) {
        }

        public async Task<IEnumerable<Tag>> AllTagsAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync().ConfigureAwait(false);

        public async Task AddTagsAsync(ICollection<AdvertTag> tags) {
            if (tags != null) {
                var existingTags = await AllTagsAsync(trackChanges: false).ConfigureAwait(false);

                var idsOfExistingTag = existingTags != null ? existingTags.Select(tag => tag.Id) : new List<string>();

                foreach (var tag in tags) {
                    if (!idsOfExistingTag.Contains(tag.TagId)) {
                        Create(new Tag() { Id = tag.TagId });
                    }
                }
            }
        }
    }
}