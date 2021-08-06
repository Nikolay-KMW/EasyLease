using System.Collections;
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
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;

        public AuthenticationController(ILoggerManager logger,
                                        IMapper mapper,
                                        UserManager<User> userManager,
                                        IAuthenticationManager authManager) {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
        }

        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationProfileAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDTO userRegistration) {
            var user = _mapper.Map<User>(userRegistration);
            var result = await _userManager.CreateAsync(user, userRegistration.Password).ConfigureAwait(false);

            if (!result.Succeeded) {
                foreach (var error in result.Errors) {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            await _userManager.AddToRolesAsync(user, new Collection<string> { "User" }).ConfigureAwait(false);
            return StatusCode(201);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationProfileAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationDTO user) {
            if (!await _authManager.ValidateUser(user).ConfigureAwait(false)) {
                _logger.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password.");
                return Unauthorized();
            }

            return Ok(new { Token = await _authManager.CreateToken().ConfigureAwait(false) });
        }
    }
}