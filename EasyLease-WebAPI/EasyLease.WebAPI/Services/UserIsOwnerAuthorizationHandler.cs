using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyLease.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace EasyLease.WebAPI.Services {
    public class UserIsOwnerAuthorizationHandler : AuthorizationHandler<UserIsOwnerRequirement> {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILoggerManager _logger;

        public UserIsOwnerAuthorizationHandler(IHttpContextAccessor httpContextAccessor, ILoggerManager logger) {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserIsOwnerRequirement requirement) {
            if (context.User.HasClaim(claim => claim.Type == ClaimTypes.NameIdentifier)) {
                Guid.TryParse(_httpContextAccessor.HttpContext.GetRouteValue("userId").ToString(), out Guid userId);
                Guid.TryParse(context.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier).Value, out Guid claimId);

                if (claimId == userId) {
                    context.Succeed(requirement);
                } else {
                    _logger.LogError($"Invalid user authorization. User ID: {userId} and Claim ID: {claimId} not equals");
                }
            }
            return Task.CompletedTask;
        }
    }

    public class UserIsOwnerRequirement : IAuthorizationRequirement {
    }
}