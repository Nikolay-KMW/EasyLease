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
                context.Result = new BadRequestObjectResult($"Photo is null. Controller: {controller}, action: {action}");
                return;
            }

            if (photos.Count > _fileStorageSettings.NumberOfFilesLimit) {
                _logger.LogError($"Number of photos sent from client is more than {_fileStorageSettings.NumberOfFilesLimit}. Controller: {controller}, action: { action}");
                context.Result = new BadRequestObjectResult($"Number of photos is more than {_fileStorageSettings.NumberOfFilesLimit}.");
                return;
            }

            foreach (var photo in photos) {
                var trustedFileNameForDisplay = WebUtility.HtmlEncode(photo.FileName);

                if (photo.Length > 0) {
                    if (photo.Length > _fileStorageSettings.FileSizeLimit) {
                        _logger.LogError($"Photo sent from client is more than {_fileStorageSettings.FileSizeLimit / (1024 * 1024)} Mb.");
                        context.Result = new BadRequestObjectResult($"Photo {trustedFileNameForDisplay} is more than {_fileStorageSettings.FileSizeLimit / (1024 * 1024)} Mb.");
                        return;
                    }

                    var fileExtension = Path.GetExtension(photo.FileName).ToLower();

                    if (!_fileStorageSettings.AllowedExtensions.Any(ext => ext == fileExtension)) {
                        _logger.LogError($"Photo sent from client has extension {fileExtension}");
                        context.Result = new BadRequestObjectResult($"Photo {trustedFileNameForDisplay} must has one of the extensions: { string.Join(',', _fileStorageSettings.AllowedExtensions)}.");
                        return;
                    }

                    using (var reader = new BinaryReader(photo.OpenReadStream())) {
                        var signatures = _fileStorageSettings.FileSignature[fileExtension];
                        var headerfile = reader.ReadBytes(signatures.Max(m => m.Length));

                        bool isValidSignature = signatures.Any(signature => headerfile.Take(signature.Length).SequenceEqual(signature));

                        if (!isValidSignature) {
                            _logger.LogError($"Photo sent from client has not valid signature: {fileExtension}");
                            context.Result = new BadRequestObjectResult($"Photo {trustedFileNameForDisplay} must has one of the extensions.: { string.Join(',', _fileStorageSettings.AllowedExtensions)}.");
                            return;
                        }
                    }
                }
            }

            // if (!context.ModelState.IsValid) {
            //     _logger.LogError($"Invalid model state for the object. Controller: {controller}, action: {action}");
            //     context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            // }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}