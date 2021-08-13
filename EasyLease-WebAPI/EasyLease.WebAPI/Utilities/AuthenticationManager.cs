using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities.AppSettingsModels;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EasyLease.WebAPI.Utilities {
    public class AuthenticationManager : IAuthenticationManager {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User _user;
        private readonly Task<IdentityResult> _cachedTaskNull = Task.FromResult<IdentityResult>(null);
        private readonly Task<bool> _cachedTaskFalse = Task.FromResult<bool>(false);

        public User GetUser => _user;
        public TimeSpan DefaultLockoutTimeSpan { get; }

        public AuthenticationManager(UserManager<User> userManager, IConfiguration configuration) {
            _userManager = userManager;
            _configuration = configuration;
            DefaultLockoutTimeSpan = _userManager.Options.Lockout.DefaultLockoutTimeSpan;
        }

        public async Task<IdentityResult> CreateUserAsync(User user, string password) {
            var result = await _userManager.CreateAsync(user, password).ConfigureAwait(false);
            _user = user;

            return result;
        }
        public async Task<bool> ValidateUserAsync(UserAuthenticationDTO userAuth) {
            _user = await _userManager.FindByEmailAsync(userAuth.Email).ConfigureAwait(false);

            return _user != null && await _userManager.CheckPasswordAsync(_user, userAuth.Password).ConfigureAwait(false);
        }

        public Task<IdentityResult> AssignRolesToUserAsync(IEnumerable<string> roles) {
            return _user != null
                ? _userManager.AddToRolesAsync(_user, roles)
                : _cachedTaskNull;
        }

        public Task<IdentityResult> AccessFailedCountAsync() {
            return _user != null
                ? _userManager.AccessFailedAsync(_user)
                : _cachedTaskNull;
        }

        public Task<bool> IsLockedOutAsync() {
            return _user != null
                ? _userManager.IsLockedOutAsync(_user)
                : _cachedTaskFalse;
        }

        public Task<IdentityResult> ResetAccessFailedCountAsync() {
            return _user != null
                ? _userManager.ResetAccessFailedCountAsync(_user)
                : _cachedTaskNull;
        }

        public Task<string> CreateTokenAsync() {
            return CreateJwtToken();
        }

        private async Task<string> CreateJwtToken() {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims().ConfigureAwait(false);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials() {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims() {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString()), new Claim(ClaimTypes.Email, _user.Email) };
            var roles = await _userManager.GetRolesAsync(_user).ConfigureAwait(false);

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