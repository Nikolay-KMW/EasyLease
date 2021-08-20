using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities.AppSettingsModels;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EasyLease.WebAPI.Utilities {
    public class AuthenticationManager : IAuthenticationManager {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public TimeSpan DefaultLockoutTimeSpan { get; }

        public AuthenticationManager(UserManager<User> userManager, IConfiguration configuration) {
            _userManager = userManager;
            _configuration = configuration;
            DefaultLockoutTimeSpan = _userManager.Options.Lockout.DefaultLockoutTimeSpan;
        }

        public bool TryGetUserId(ClaimsPrincipal claimsPrincipal, out Guid userId) {
            return Guid.TryParse(_userManager.GetUserId(claimsPrincipal), out userId);
        }

        public Task<User> GetAuthorizedUserAsync(ClaimsPrincipal claimsPrincipal) {
            return _userManager.GetUserAsync(claimsPrincipal);
        }

        // public async Task<User> GetUserFullAsync(ClaimsPrincipal claimsPrincipal, bool trackChanges) {
        //     User receivedUser = await _userManager.GetUserAsync(claimsPrincipal).ConfigureAwait(false);

        //     if (receivedUser == null) {
        //         return null;
        //     }

        //     return !trackChanges
        //      ? await _userManager.Users.Where(user => user.Id.Equals(receivedUser.Id)).AsNoTracking().Include(user => user.AdvertFavorites).SingleOrDefaultAsync().ConfigureAwait(false)
        //      : await _userManager.Users.Where(user => user.Id.Equals(receivedUser.Id)).Include(user => user.AdvertFavorites).SingleOrDefaultAsync().ConfigureAwait(false);
        // }

        public Task<IdentityResult> CreateUserAsync(User user, string password) {
            return _userManager.CreateAsync(user, password);
        }

        public Task<IdentityResult> UpdateUserAsync(User user) {
            return _userManager.UpdateAsync(user);
        }

        public async Task<(User user, (bool EmailIsNotValid, bool PasswordIsNotValid) validatorErrors)> GetAndValidateUserAsync(UserAuthenticationDTO userAuthDTO) {
            var user = await _userManager.FindByEmailAsync(userAuthDTO.Email).ConfigureAwait(false);
            var passwordIsValid = await _userManager.CheckPasswordAsync(user, userAuthDTO.Password).ConfigureAwait(false);

            return (user, (user == null, !passwordIsValid));
        }

        public Task<IdentityResult> AssignRolesAsync(User user, IEnumerable<string> roles) {
            return _userManager.AddToRolesAsync(user, roles);
        }

        public Task<IdentityResult> AccessFailedCountAsync(User user) {
            return _userManager.AccessFailedAsync(user);
        }

        public Task<bool> IsLockedOutAsync(User user) {
            return _userManager.IsLockedOutAsync(user);
        }

        public Task<IdentityResult> ResetAccessFailedCountAsync(User user) {
            return _userManager.ResetAccessFailedCountAsync(user);
        }

        public Task<string> CreateTokenAsync(User user) {
            return CreateJwtToken(user);
        }

        private async Task<string> CreateJwtToken(User user) {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user).ConfigureAwait(false);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials() {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(User user) {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), new Claim(ClaimTypes.Email, user.Email) };
            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            foreach (var role in roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims) {
            JwtSettings jwtSettings = new JwtSettings();
            _configuration.GetSection("JwtSettings").Bind(jwtSettings);

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings.ValidIssuer,
                audience: jwtSettings.ValidAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.Expires),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    }
}