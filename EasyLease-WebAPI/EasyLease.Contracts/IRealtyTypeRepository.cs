using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLease.Entities.Models;

namespace EasyLease.Contracts {
    public interface IRealtyTypeRepository {
        Task<IEnumerable<RealtyType>> GetAllRealtyTypeAsync(bool trackChanges);
    }
}