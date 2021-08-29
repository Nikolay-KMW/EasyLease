using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLease.Entities.Models;

namespace EasyLease.Contracts {
    public interface IStreetTypeRepository {
        Task<IEnumerable<StreetType>> GetAllStreetTypeAsync(bool trackChanges);
    }
}