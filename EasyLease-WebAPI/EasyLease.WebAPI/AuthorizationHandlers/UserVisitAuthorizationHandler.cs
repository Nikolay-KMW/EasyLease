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
    public class UserVisitAuthorizationHandler : AuthorizationHandler<UserVisitRequirement> {
        private readonly IAuthenticationManager _authManager;

        public UserVisitAuthorizationHandler(IAuthenticationManager authManager) {
            _authManager = authManager ?? throw new ArgumentNullException(nameof(authManager));
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserVisitRequirement requirement) {
            var user = await _authManager.GetAuthorizedUserAsync(context.User).ConfigureAwait(false);

            if (user == null) {
                context.Fail();
                return;
            }

            user.VisitedUser = DateTime.UtcNow;
            await _authManager.UpdateUserAsync(user).ConfigureAwait(false);

            context.Succeed(requirement);
            return;
        }
    }

    public class UserVisitRequirement : IAuthorizationRequirement {
    }
}