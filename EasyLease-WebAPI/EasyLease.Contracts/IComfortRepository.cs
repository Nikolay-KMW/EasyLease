using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLease.Entities.Models;

namespace EasyLease.Contracts {
    public interface IComfortRepository {
        Task<IEnumerable<Comfort>> GetAllComfortsAsync(bool trackChanges);
    }
}