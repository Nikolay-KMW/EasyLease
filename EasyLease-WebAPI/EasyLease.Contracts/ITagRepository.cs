using System.Collections.Generic;
using EasyLease.Entities.Models;

namespace EasyLease.Contracts {
    public interface ITagRepository {

        IEnumerable<Tag> AllTags(bool trackChanges);
        void AddTags(ICollection<AdvertTag> tags);
    }
}