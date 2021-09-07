using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EasyLease.Contracts;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;
using EasyLease.WebAPI.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyLease.WebAPI.Controllers {
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IAuthenticationManager _authManager;

        public AuthenticationController(ILoggerManager logger,
                                        IMapper mapper,
                                        IAuthenticationManager authManager) {
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
        }

        [HttpGet("user"), Authorize(Policy = "UserVisit")]
        //===============================================================================
        public async Task<IActionResult> GetAuthorizedUser() {
            var user = await _authManager.GetAuthorizedUserAsync(HttpContext.User).ConfigureAwait(false);

            var userDTO = _mapper.Map<UserDTO>(user);

            return Ok(new { user = userDTO });
        }

        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationProfileAttribute))]
        //===============================================================================
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDTO userRegistrationDTO) {
            var user = _mapper.Map<User>(userRegistrationDTO);
            var result = await _authManager.CreateUserAsync(user, userRegistrationDTO.Password).ConfigureAwait(false);

            if (!result.Succeeded) {
                foreach (var error in result.Errors) {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            await _authManager.AssignRolesAsync(user, new Collection<string> { "User" }).ConfigureAwait(false);

            var userDTO = _mapper.Map<UserDTO>(user);
            userDTO.Token = await _authManager.CreateTokenAsync(user).ConfigureAwait(false);

            return StatusCode(201, new { user = userDTO });
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationProfileAttribute))]
        //===============================================================================
        public async Task<IActionResult> AuthenticateUser([FromBody] UserAuthenticationDTO userAuthDTO) {
            var (user, validatorErrors) = await _authManager.GetAndValidateUserAsync(userAuthDTO).ConfigureAwait(false);

            if (validatorErrors.EmailIsNotValid) {
                _logger.LogWarn($"{nameof(AuthenticateUser)}: Authentication failed. Wrong user email.");
                ModelState.TryAddModelError(nameof(UserAuthenticationDTO.Email), "Wrong user email.");

                return BadRequest(ModelState);
                //return Unauthorized(BadRequest(ModelState));
            }
            if (validatorErrors.PasswordIsNotValid) {
                _logger.LogWarn($"{nameof(AuthenticateUser)}: Authentication failed. Wrong user password.");
                ModelState.TryAddModelError(nameof(UserAuthenticationDTO.Password), "Wrong user password.");

                await _authManager.AccessFailedCountAsync(user).ConfigureAwait(false);

                if (await _authManager.IsLockedOutAsync(user).ConfigureAwait(false)) {
                    return Unauthorized(new { ErrorMessage = $"Your account is locked out for {_authManager.DefaultLockoutTimeSpan.Minutes} minutes" });
                }

                return BadRequest(ModelState);
                //return Unauthorized(BadRequest(ModelState));
            }

            await _authManager.ResetAccessFailedCountAsync(user).ConfigureAwait(false);

            var userDTO = _mapper.Map<UserDTO>(user);
            userDTO.Token = await _authManager.CreateTokenAsync(user).ConfigureAwait(false);

            return Ok(new { user = userDTO });
        }
    }
}