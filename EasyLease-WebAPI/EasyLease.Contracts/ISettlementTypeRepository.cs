using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLease.Entities.Models;

namespace EasyLease.Contracts {
    public interface ISettlementTypeRepository {
        Task<IEnumerable<SettlementType>> GetAllSettlementTypeAsync(bool trackChanges);
    }
}