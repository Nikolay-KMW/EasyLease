using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities.AppSettingsModels;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog.LayoutRenderers;

namespace EasyLease.WebAPI.ActionFilters {
    public class ValidationPhotoForUserAttribute : IAsyncActionFilter {
        private readonly ILoggerManager _logger;
        private readonly UserProfileSettings _userProfileSettings;

        public ValidationPhotoForUserAttribute(UserProfileSettings userProfileSettings, ILoggerManager logger) {
            _logger = logger;
            _userProfileSettings = userProfileSettings;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            IFormFile avatar = (IFormFile)context.ActionArguments.SingleOrDefault(x => x.Key.Equals("avatar")).Value;

            if (avatar == null) {
                _logger.LogError($"Photo sent from client is null. Controller: {controller}, action: { action}");
                context.ModelState.AddModelError(nameof(avatar), $"Photo is empty. Controller: {controller}, action: {action}");
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }

            var trustedFileNameForDisplay = WebUtility.HtmlEncode(avatar.FileName);
            var fileExtension = Path.GetExtension(avatar.FileName).ToLower();

            if (!_userProfileSettings.AllowedExtensions.Any(ext => ext == fileExtension)) {
                _logger.LogError($"Photo sent from client has extension {fileExtension}");
                context.ModelState.AddModelError(nameof(avatar), $"Photo < {trustedFileNameForDisplay} > must has one of the extensions: { string.Join(',', _userProfileSettings.AllowedExtensions)}.");
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }

            using (var memoryStream = new MemoryStream()) {
                await avatar.CopyToAsync(memoryStream).ConfigureAwait(false);

                if (memoryStream.Length > _userProfileSettings.FileSizeLimitForAvatar) {
                    _logger.LogError($"Photo sent from client is more than {_userProfileSettings.FileSizeLimitForAvatar / 1024}KB.");
                    context.ModelState.AddModelError(nameof(avatar), $"Photo < {trustedFileNameForDisplay} > is more than {_userProfileSettings.FileSizeLimitForAvatar / 1024}KB.");
                    context.Result = new BadRequestObjectResult(context.ModelState);
                    return;
                }

                memoryStream.Position = 0;

                using (var reader = new BinaryReader(memoryStream)) {
                    var signatures = _userProfileSettings.FileSignature[fileExtension];
                    var headerfile = reader.ReadBytes(signatures.Max(m => m.Length));

                    bool isValidSignature = signatures.Any(signature => headerfile.Take(signature.Length).SequenceEqual(signature));

                    if (!isValidSignature) {
                        _logger.LogError($"Photo sent from client has not valid signature: {fileExtension}");
                        context.ModelState.AddModelError(nameof(avatar), $"Photo < {trustedFileNameForDisplay} > must has one of the extensions.: { string.Join(',', _userProfileSettings.AllowedExtensions)}.");
                        context.Result = new BadRequestObjectResult(context.ModelState);
                        return;
                    }
                }
                context.HttpContext.Items.Add("avatar", memoryStream.ToArray());
            }

            if (context.ModelState.IsValid) {
                await next().ConfigureAwait(false);
            }
        }
    }
}