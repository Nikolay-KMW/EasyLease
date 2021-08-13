using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using EasyLease.Contracts;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;
using EasyLease.WebAPI.ActionFilters;
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

        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationProfileAttribute))]
        //===============================================================================
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDTO userRegistration) {
            var user = _mapper.Map<User>(userRegistration);
            var result = await _authManager.CreateUserAsync(user, userRegistration.Password).ConfigureAwait(false);

            if (!result.Succeeded) {
                foreach (var error in result.Errors) {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            await _authManager.AssignRolesToUserAsync(new Collection<string> { "User" }).ConfigureAwait(false);

            var userDTO = _mapper.Map<UserDTO>(_authManager.GetUser);
            userDTO.Token = await _authManager.CreateTokenAsync().ConfigureAwait(false);

            return StatusCode(201, userDTO);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationProfileAttribute))]
        //===============================================================================
        public async Task<IActionResult> AuthenticateUser([FromBody] UserAuthenticationDTO userAuth) {
            if (!await _authManager.ValidateUserAsync(userAuth).ConfigureAwait(false)) {
                _logger.LogWarn($"{nameof(AuthenticateUser)}: Authentication failed. Wrong user email and (or) password.");
                ModelState.TryAddModelError(nameof(UserAuthenticationDTO.Email), "Wrong user email and (or) password.");
                ModelState.TryAddModelError(nameof(UserAuthenticationDTO.Password), "Wrong user email and (or) password.");

                await _authManager.AccessFailedCountAsync().ConfigureAwait(false);

                if (await _authManager.IsLockedOutAsync().ConfigureAwait(false)) {
                    return Unauthorized(new { ErrorMessage = $"Your account is locked out for {_authManager.DefaultLockoutTimeSpan.Minutes} minutes" });
                }

                return BadRequest(ModelState);
                //return Unauthorized(BadRequest(ModelState));
            }

            await _authManager.ResetAccessFailedCountAsync().ConfigureAwait(false);

            var userDTO = _mapper.Map<UserDTO>(_authManager.GetUser);
            userDTO.Token = await _authManager.CreateTokenAsync().ConfigureAwait(false);

            return Ok(userDTO);
        }
    }
}