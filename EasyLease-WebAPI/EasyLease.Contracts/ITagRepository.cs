using System.Collections.Generic;
using EasyLease.Entities.Models;

namespace EasyLease.Contracts {
    public interface ITagRepository {
        void AddTags(ICollection<AdvertTag> tags);
    }
}