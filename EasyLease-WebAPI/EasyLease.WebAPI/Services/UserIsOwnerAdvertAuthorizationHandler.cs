using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace EasyLease.WebAPI.Services {
    public class UserIsOwnerAdvertAuthorizationHandler : AuthorizationHandler<UserIsOwnerAdvertRequirement> {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;

        public UserIsOwnerAdvertAuthorizationHandler(IHttpContextAccessor httpContextAccessor, IRepositoryManager repository, ILoggerManager logger) {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _logger = logger ?? throw new ArgumentNullException(nameof(repository));
            _repository = repository ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserIsOwnerAdvertRequirement requirement) {
            var trackChanges = _httpContextAccessor.HttpContext.Request.Method.Equals("PUT");
            var isParsedAdvertId = Guid.TryParse(_httpContextAccessor.HttpContext.GetRouteValue("advertId").ToString(), out Guid advertId);

            Advert advert = null;
            if (context.User.HasClaim(claim => claim.Type == ClaimTypes.NameIdentifier)) {
                Guid.TryParse(context.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier).Value, out Guid userTokenId);

                if (isParsedAdvertId) {
                    advert = await _repository.Advert.GetAdvertAsync(advertId, trackChanges).ConfigureAwait(false);
                }

                if (advert != null && advert.UserId == userTokenId) {
                    _httpContextAccessor.HttpContext.Items.Add("advert", advert);
                    context.Succeed(requirement);
                } else {
                    _logger.LogError($"Invalid user authorization - {typeof(UserIsOwnerAdvertRequirement)}. User by ID:{userTokenId} is not the owner of the Advert by ID:{advertId}");
                }
            }
            return;
        }
    }

    public class UserIsOwnerAdvertRequirement : IAuthorizationRequirement {
    }
}