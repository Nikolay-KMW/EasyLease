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

        public AuthenticationManager(UserManager<User> userManager, IConfiguration configuration) {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<bool> ValidateUser(UserAuthenticationDTO userAuth) {
            _user = await _userManager.FindByNameAsync(userAuth.UserName).ConfigureAwait(false);

            return _user != null && await _userManager.CheckPasswordAsync(_user, userAuth.Password).ConfigureAwait(false);
        }

        public async Task<string> CreateToken() {
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
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, _user.UserName) };
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