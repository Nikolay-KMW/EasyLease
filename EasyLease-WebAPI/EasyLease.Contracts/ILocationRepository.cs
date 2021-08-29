using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLease.Entities.Models;

namespace EasyLease.Contracts {
    public interface ILocationRepository {
        Task<IEnumerable<Location>> GetAllLocationAsync(bool trackChanges);
    }
}