using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace EasyLease.Contracts {
    public interface IAuthenticationManager {
        User GetUser { get; }
        TimeSpan DefaultLockoutTimeSpan { get; }
        Task<IdentityResult> CreateUserAsync(User userAuth, string password);
        Task<IdentityResult> AssignRolesToUserAsync(IEnumerable<string> roles);
        Task<bool> ValidateUserAsync(UserAuthenticationDTO userAuth);
        Task<IdentityResult> AccessFailedCountAsync();
        Task<bool> IsLockedOutAsync();
        Task<IdentityResult> ResetAccessFailedCountAsync();
        Task<string> CreateTokenAsync();
    }
}