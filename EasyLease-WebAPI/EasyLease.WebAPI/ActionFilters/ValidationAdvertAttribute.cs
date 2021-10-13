using System;
using System.Linq;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities.AppSettingsModels;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;
using EasyLease.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyLease.WebAPI.ActionFilters {
    public class ValidationAdvertAttribute : IAsyncActionFilter {
        private readonly GeneralSettings _generalSettings;
        private readonly ILoggerManager _logger;
        private readonly AdditionalDataService _additionalDataService;

        public ValidationAdvertAttribute(GeneralSettings generalSettings, AdditionalDataService additionalDataService, ILoggerManager logger) {
            _generalSettings = generalSettings;
            _logger = logger;
            _additionalDataService = additionalDataService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            var advertDTO = (AdvertManipulationDTO)context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("DTO")).Value;

            if (advertDTO == null) {
                _logger.LogError($"Advert sent from client is null. Controller: {controller}, action: { action}");
                context.Result = new BadRequestObjectResult($"Advert is null. Controller: {controller}, action: {action}");
                return;
            }

            var additionalDataForAdvert = await _additionalDataService.GetAdditionalDataForAdvertAsync().ConfigureAwait(false);

            if (!additionalDataForAdvert.RealtyType.Contains(advertDTO.RealtyType)) {
                _logger.LogError($"The realty type ({advertDTO.RealtyType}) is invalid.");
                context.ModelState.AddModelError(nameof(advertDTO.RealtyType), "The realty type is invalid.");
            }

            if (!additionalDataForAdvert.SettlementType.Contains(advertDTO.SettlementType)) {
                _logger.LogError($"The Settlement type ({advertDTO.SettlementType}) is invalid.");
                context.ModelState.AddModelError(nameof(advertDTO.SettlementType), "The Settlement type is invalid.");
            }

            if (!additionalDataForAdvert.StreetType.Contains(advertDTO.StreetType)) {
                _logger.LogError($"The Street type ({advertDTO.StreetType}) is invalid.");
                context.ModelState.AddModelError(nameof(advertDTO.StreetType), "The Street type is invalid.");
            }

            var region = Array.Find(additionalDataForAdvert.Locations, location => location.Region == advertDTO.Region);

            if (region != null) {
                if (!region.District.Contains(advertDTO.District)) {
                    _logger.LogError($"The District ({advertDTO.District}) is invalid.");
                    context.ModelState.AddModelError(nameof(advertDTO.District), "The District is invalid.");
                }
            } else {
                _logger.LogError($"The Region ({advertDTO.Region}) is invalid.");
                context.ModelState.AddModelError(nameof(advertDTO.Region), "The Region is invalid.");
            }

            if (!Enum.TryParse<PriceType>(advertDTO.PriceType, ignoreCase: true, out PriceType enumValue)) {
                _logger.LogError($"Enum value {advertDTO.PriceType} is not valid for the PriceType.");
                context.ModelState.AddModelError(nameof(advertDTO.PriceType), "The type of price is invalid.");
            }

            DateTime currenDateTime = DateTime.UtcNow.AddHours(_generalSettings.HoursOffsetForUkraine);

            if (advertDTO.StartOfLease < currenDateTime) {
                _logger.LogError($"The date start of lease set {advertDTO.StartOfLease} is earlier than now {currenDateTime}.");
                context.ModelState.AddModelError(nameof(advertDTO.StartOfLease), "The date start of lease is invalid.");
            }

            if (advertDTO.EndOfLease != null) {
                if (advertDTO.EndOfLease <= advertDTO.StartOfLease) {
                    _logger.LogError($"The date end of lease set {advertDTO.EndOfLease} is earlier than the date start of lease {advertDTO.StartOfLease}.");
                    context.ModelState.AddModelError(nameof(advertDTO.EndOfLease), "The date end of lease is invalid");
                }
            }

            if (!context.ModelState.IsValid) {
                _logger.LogError($"Invalid model state for the object. Controller: {controller}, action: {action}");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            } else {
                await next().ConfigureAwait(false);
            }
        }
    }
}