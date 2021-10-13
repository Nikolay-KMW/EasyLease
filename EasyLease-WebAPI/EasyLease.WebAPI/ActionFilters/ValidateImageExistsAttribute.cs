using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyLease.WebAPI.ActionFilters {
    public class ValidateImageExistsAttribute : IAsyncActionFilter {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;

        public ValidateImageExistsAttribute(IRepositoryManager repository, ILoggerManager logger) {
            _logger = logger;
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            string namePhoto = context.HttpContext.Request.Query["namePhoto"].ToString();

            Advert advert = null;
            if (context.ActionArguments.TryGetValue("advertId", out var advertId)) {
                advert = (Advert)context.HttpContext.Items["advert"] ??
                    await _repository.Advert.GetAdvertAsync((Guid)advertId, trackChanges: false).ConfigureAwait(false);
            }

            var trustedFileNameForDisplay = WebUtility.HtmlEncode(namePhoto);

            ICollection<Image> images = advert.Images;

            if (images == null) {
                _logger.LogInfo($"Advert with id: {advertId} haven't photo");
                context.Result = new NotFoundResult();
            }

            Image image = images.FirstOrDefault(image => image.Name == namePhoto);

            if (image == null) {
                _logger.LogInfo($"Photo {trustedFileNameForDisplay} not found");
                context.Result = new NotFoundResult();
            } else {
                context.HttpContext.Items.Add("image", image);
                await next().ConfigureAwait(false);
            }
        }
    }
}