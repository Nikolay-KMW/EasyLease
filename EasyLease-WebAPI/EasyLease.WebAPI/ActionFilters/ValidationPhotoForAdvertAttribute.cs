using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using EasyLease.Contracts;
using EasyLease.Entities.AppSettingsModels;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyLease.WebAPI.ActionFilters {
    public class ValidationPhotoForAdvertAttribute : IActionFilter {
        private readonly ILoggerManager _logger;
        private readonly FileStorageSettings _fileStorageSettings;

        public ValidationPhotoForAdvertAttribute(FileStorageSettings fileStorageSettings, ILoggerManager logger) {
            _logger = logger;
            _fileStorageSettings = fileStorageSettings;
        }
        public void OnActionExecuting(ActionExecutingContext context) {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            List<IFormFile> photos = (List<IFormFile>)context.ActionArguments["photos"];

            if (photos.Count == 0) {
                _logger.LogError($"Photo sent from client is null. Controller: {controller}, action: { action}");
                context.ModelState.AddModelError(nameof(photos), $"Photo is null. Controller: {controller}, action: {action}");
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }

            if (photos.Count > _fileStorageSettings.NumberOfFilesLimit) {
                _logger.LogError($"Number of photos sent from client is more than {_fileStorageSettings.NumberOfFilesLimit}. Controller: {controller}, action: { action}");
                context.ModelState.AddModelError(nameof(photos), $"Number of photos is more than {_fileStorageSettings.NumberOfFilesLimit}.");
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }

            foreach (var photo in photos) {
                var trustedFileNameForDisplay = WebUtility.HtmlEncode(photo.FileName);

                if (photo.Length == 0) {
                    _logger.LogError("Photo sent from client is empty.");
                    context.ModelState.AddModelError(nameof(photos), $"Photo < {trustedFileNameForDisplay} > is empty.");
                    context.Result = new BadRequestObjectResult(context.ModelState);
                    return;
                }

                if (photo.Length > _fileStorageSettings.FileSizeLimit) {
                    _logger.LogError($"Photo sent from client is more than {_fileStorageSettings.FileSizeLimit / (1024 * 1024)} Mb.");
                    context.ModelState.AddModelError(nameof(photos), $"Photo < {trustedFileNameForDisplay} > is more than {_fileStorageSettings.FileSizeLimit / (1024 * 1024)} Mb.");
                    context.Result = new BadRequestObjectResult(context.ModelState);
                    return;
                }

                var fileExtension = Path.GetExtension(photo.FileName).ToLower();

                if (!_fileStorageSettings.AllowedExtensions.Any(ext => ext == fileExtension)) {
                    _logger.LogError($"Photo sent from client has extension {fileExtension}");
                    context.ModelState.AddModelError(nameof(photos), $"Photo < {trustedFileNameForDisplay} > must has one of the extensions: { string.Join(',', _fileStorageSettings.AllowedExtensions)}.");
                    context.Result = new BadRequestObjectResult(context.ModelState);
                    return;
                }

                using (var reader = new BinaryReader(photo.OpenReadStream())) {
                    var signatures = _fileStorageSettings.FileSignature[fileExtension];
                    var headerfile = reader.ReadBytes(signatures.Max(m => m.Length));

                    bool isValidSignature = signatures.Any(signature => headerfile.Take(signature.Length).SequenceEqual(signature));

                    if (!isValidSignature) {
                        _logger.LogError($"Photo sent from client has not valid signature: {fileExtension}");
                        context.ModelState.AddModelError(nameof(photos), $"Photo < {trustedFileNameForDisplay} > must has one of the extensions.: { string.Join(',', _fileStorageSettings.AllowedExtensions)}.");
                        context.Result = new BadRequestObjectResult(context.ModelState);
                        return;
                    }
                }
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}