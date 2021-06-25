using System;
using System.Collections.Generic;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.Models;

namespace EasyLease.Repository {
    public class TagRepository : RepositoryBase<Tag>, ITagRepository {
        public TagRepository(RepositoryContext repositoryContext) : base(repositoryContext) {
        }
        public void AddTags(ICollection<AdvertTag> tags) {

            foreach (var tag in tags) {
                tag.CreatedTag = DateTime.UtcNow;
                Create(tag.Tag);
            }
        }
    }
}