using System;
using System.Collections.Generic;
using System.Linq;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;

namespace EasyLease.Repository {
    public class TagRepository : RepositoryBase<Tag>, ITagRepository {
        public TagRepository(RepositoryContext repositoryContext) : base(repositoryContext) {
        }

        public IEnumerable<Tag> AllTags(bool trackChanges) =>
            FindAll(trackChanges).ToList();

        public void AddTags(ICollection<AdvertTag> tags) {
            var existingTagsId = AllTags(trackChanges: false).Select(tag => tag.Id);

            foreach (var tag in tags) {
                if (!existingTagsId.Contains(tag.TagId)) {
                    Create(new Tag() { Id = tag.TagId });
                }
            }
        }
    }
}