using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using EasyLease.Contracts;
using EasyLease.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyLease.WebAPI.ActionFilters {
    public class ValidateImageExistsAttribute : IActionFilter {
        private readonly ILoggerManager _logger;
        public ValidateImageExistsAttribute(ILoggerManager logger) {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context) {
            var advert = context.HttpContext.Items["advert"] as Advert;

            var advertId = (Guid)context.ActionArguments["advertId"];
            string namePhoto = (string)context.ActionArguments["namePhoto"];

            var trustedFileNameForDisplay = WebUtility.HtmlEncode(namePhoto);
            var namePhotoWithoutExtension = Path.GetFileNameWithoutExtension(namePhoto);

            ICollection<Image> images = advert.Images;

            if (images == null) {
                _logger.LogInfo($"Advert with id: {advertId} haven't photo");
                context.Result = new NotFoundResult();
                return;
            }

            Image image = images.FirstOrDefault(image => image.Name == namePhotoWithoutExtension);

            if (image == null) {
                _logger.LogInfo($"Photo {trustedFileNameForDisplay} not found");
                context.Result = new NotFoundResult();
            } else {
                context.HttpContext.Items.Add("image", image);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}