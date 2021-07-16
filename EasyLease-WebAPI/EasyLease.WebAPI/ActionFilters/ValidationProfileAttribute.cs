using System.Linq;
using EasyLease.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyLease.WebAPI.ActionFilters {
    public class ValidationProfileAttribute : IActionFilter {
        private readonly ILoggerManager _logger;
        public ValidationProfileAttribute(ILoggerManager logger) {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context) {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            var profileDTO = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("DTO")).Value;

            if (profileDTO == null) {
                _logger.LogError($"Profile sent from client is null. Controller: {controller}, action: { action}");
                context.Result = new BadRequestObjectResult($"Profile is null. Controller: {controller}, action: {action}");
                return;
            }

            if (!context.ModelState.IsValid) {
                _logger.LogError($"Invalid model state for the Profile. Controller: {controller}, action: {action}");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}