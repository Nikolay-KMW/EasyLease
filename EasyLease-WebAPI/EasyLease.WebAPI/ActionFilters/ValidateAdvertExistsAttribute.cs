using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities.Models;
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

            Advert advert = null;
            if (context.ActionArguments.TryGetValue("advertId", out var advertId)) {
                advert = (Advert)context.HttpContext.Items["advert"] ??
                    await _repository.Advert.GetAdvertAsync((Guid)advertId, trackChanges).ConfigureAwait(false);
            }

            if (advert == null) {
                _logger.LogInfo($"Advert with id: {advertId} doesn't exist in the database");
                context.Result = new NotFoundResult();
            } else {
                context.HttpContext.Items.TryAdd("advert", advert);
                await next().ConfigureAwait(false);
            }
        }
    }
}