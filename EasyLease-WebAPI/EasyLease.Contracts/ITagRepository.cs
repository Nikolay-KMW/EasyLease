using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLease.Entities.Models;

namespace EasyLease.Contracts {
    public interface ITagRepository {
        Task<IEnumerable<Tag>> AllTagsAsync(bool trackChanges);
        Task AddTagsAsync(ICollection<AdvertTag> tags);
    }
}