using System;
using System.Threading.Tasks;
using EasyLease.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyLease.WebAPI.ActionFilters {
    public class ValidateAdvertExistsAttribute : IAsyncActionFilter {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateAdvertExistsAttribute(IRepositoryManager repository, ILoggerManager logger) {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var advertId = (Guid)context.ActionArguments["advertId"];

            var advert = await _repository.Advert.GetAdvertAsync(advertId, trackChanges).ConfigureAwait(false);

            if (advert == null) {
                _logger.LogInfo($"Advert with id: {advertId} doesn't exist in the database");
                context.Result = new NotFoundResult();
            } else {
                context.HttpContext.Items.Add("advert", advert);
                await next().ConfigureAwait(false);
            }
        }
    }
}