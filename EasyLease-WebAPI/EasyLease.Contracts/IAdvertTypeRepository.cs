using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLease.Entities.Models;

namespace EasyLease.Contracts {
    public interface IAdvertTypeRepository {
        Task<IEnumerable<AdvertType>> GetAllAdvertTypeAsync(bool trackChanges);
    }
}