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
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            string namePhoto = (string)context.ActionArguments["namePhoto"];

            Advert advert = null;
            if (context.ActionArguments.TryGetValue("advertId", out var advertId)) {
                advert = (Advert)context.HttpContext.Items["advert"] ??
                    await _repository.Advert.GetAdvertAsync((Guid)advertId, trackChanges).ConfigureAwait(false);
            }

            var trustedFileNameForDisplay = WebUtility.HtmlEncode(namePhoto);
            var namePhotoWithoutExtension = Path.GetFileNameWithoutExtension(namePhoto);

            ICollection<Image> images = advert.Images;

            if (images == null) {
                _logger.LogInfo($"Advert with id: {advertId} haven't photo");
                context.Result = new NotFoundResult();
            }

            Image image = images.FirstOrDefault(image => image.Name == namePhotoWithoutExtension);

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