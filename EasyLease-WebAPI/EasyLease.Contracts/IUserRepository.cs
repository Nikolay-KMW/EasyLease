using System;
using EasyLease.Entities.Models;

namespace EasyLease.Contracts {
    public interface IUserRepository {
        User GetUser(Guid userId, bool trackChanges);
    }
}