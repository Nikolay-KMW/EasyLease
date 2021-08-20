using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace EasyLease.Contracts {
    public interface IAuthenticationManager {
        TimeSpan DefaultLockoutTimeSpan { get; }
        bool TryGetUserId(ClaimsPrincipal claimsPrincipal, out Guid userId);
        Task<User> GetAuthorizedUserAsync(ClaimsPrincipal claimsPrincipal);

        // Task<User> GetUserFullAsync(ClaimsPrincipal claimsPrincipal, bool trackChanges);
        Task<IdentityResult> CreateUserAsync(User user, string password);
        Task<IdentityResult> UpdateUserAsync(User user);
        Task<IdentityResult> AssignRolesAsync(User user, IEnumerable<string> roles);
        Task<(User user, (bool EmailIsNotValid, bool PasswordIsNotValid) validatorErrors)> GetAndValidateUserAsync(UserAuthenticationDTO userAuthDTO);
        Task<IdentityResult> AccessFailedCountAsync(User user);
        Task<bool> IsLockedOutAsync(User user);
        Task<IdentityResult> ResetAccessFailedCountAsync(User user);
        Task<string> CreateTokenAsync(User user);
    }
}