using System.Collections.Generic;
using EasyLease.Entities.Models;

namespace EasyLease.Contracts {
    public interface IAdvertRepository {
        IEnumerable<Advert> GetAllAdverts(bool trackChanges);
    }
}