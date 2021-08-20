using System;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyLease.WebAPI.ActionFilters {
    public class ValidateProfileExistsAttribute : IAsyncActionFilter {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateProfileExistsAttribute(IRepositoryManager repository, ILoggerManager logger) {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");

            User user = null;
            if (context.ActionArguments.TryGetValue("userId", out var userId)) {
                user = await _repository.User.GetUserAsync((Guid)userId, trackChanges).ConfigureAwait(false);
            }

            if (user == null) {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database");
                context.Result = new NotFoundResult();
            } else {
                context.HttpContext.Items.Add("user", user);
                await next().ConfigureAwait(false);
            }
        }
    }
}