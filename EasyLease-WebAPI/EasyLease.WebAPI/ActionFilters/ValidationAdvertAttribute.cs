using System;
using System.Linq;
using EasyLease.Contracts;
using EasyLease.Entities.AppSettingsModels;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyLease.WebAPI.ActionFilters {
    public class ValidationAdvertAttribute : IActionFilter {
        private readonly GeneralSettings _generalSettings;
        private readonly ILoggerManager _logger;
        public ValidationAdvertAttribute(GeneralSettings generalSettings, ILoggerManager logger) {
            _generalSettings = generalSettings;
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context) {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            var advertDTO = (AdvertManipulationDTO)context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("DTO")).Value;

            if (advertDTO == null) {
                _logger.LogError($"Advert sent from client is null. Controller: {controller}, action: { action}");
                context.Result = new BadRequestObjectResult($"Advert is null. Controller: {controller}, action: {action}");
                return;
            }

            DateTime currenDateTime = DateTime.UtcNow.AddHours(_generalSettings.HoursOffsetForUkraine);

            if (advertDTO.StartOfLease < currenDateTime) {
                _logger.LogInfo($"The date start of lease set {advertDTO.StartOfLease} is earlier than now {currenDateTime}.");
                context.ModelState.AddModelError(nameof(advertDTO.StartOfLease), "The date start of lease is invalid");
            }

            if (advertDTO.EndOfLease != null) {
                if (advertDTO.EndOfLease < advertDTO.StartOfLease) {
                    _logger.LogInfo($"The date end of lease set {advertDTO.EndOfLease} is earlier than the date start of lease {advertDTO.StartOfLease}.");
                    context.ModelState.AddModelError(nameof(advertDTO.EndOfLease), "The date end of lease is invalid");
                }
            }

            // if (!ModelState.IsValid) {
            //     _logger.LogError("Invalid model state for the AdvertCreationDTO object");
            //     return UnprocessableEntity(ModelState);
            // }

            if (!context.ModelState.IsValid) {
                _logger.LogError($"Invalid model state for the object. Controller: {controller}, action: {action}");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}