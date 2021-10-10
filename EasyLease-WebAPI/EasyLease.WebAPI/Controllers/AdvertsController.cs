using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EasyLease.Contracts;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;
using EasyLease.Entities.RequestFeatures;
using EasyLease.WebAPI.ActionFilters;
using EasyLease.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EasyLease.WebAPI.Controllers {
    [Route("api/realty")]
    [ApiController]
    public class AdvertsController : ControllerBase {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IAuthenticationManager _authManager;
        private readonly AdditionalDataService _additionalDataService;

        public AdvertsController(IRepositoryManager repository,
                                 IFileStorageManager fileStorageManager,
                                 IMapper mapper,
                                 IAuthenticationManager authManager,
                                 AdditionalDataService additionalDataService) {
            _repository = repository;
            _mapper = mapper;
            _fileStorageManager = fileStorageManager;
            _authManager = authManager;
            _additionalDataService = additionalDataService;
        }

        [HttpGet]
        //===============================================================================
        public async Task<IActionResult> GetAdverts([FromQuery] AdvertParameters advertParameters) {
            User user = await GetAuthorizedUserWhitFavoriteAdsAsync(HttpContext.User, trackChanges: false).ConfigureAwait(false);
            var adverts = await _repository.Advert.GetAllAdvertsAsync(advertParameters, trackChanges: false).ConfigureAwait(false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(adverts.MetaData));

            var advertsToReturn = BuildAdvertsDTOToReturn(adverts, user);

            return Ok(new { adverts = advertsToReturn });
        }

        [HttpGet("{advertId}", Name = "GetAdvertById")]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> GetAdvertById(Guid advertId) {
            User user = await GetAuthorizedUserWhitFavoriteAdsAsync(HttpContext.User, trackChanges: false).ConfigureAwait(false);
            var advert = HttpContext.Items["advert"] as Advert;

            var advertToReturn = BuildAdvertDTOToReturn(advert, user);

            return Ok(new { advert = advertToReturn });
        }

        [HttpGet("additional-data")]
        //===============================================================================
        public async Task<IActionResult> GetAdditionalDataForAdvert() {
            var additionalDataToReturn = await _additionalDataService.GetAdditionalDataForAdvertAsync().ConfigureAwait(false);

            return Ok(additionalDataToReturn);
        }

        [HttpPost, Authorize(Policy = "UserVisit")]
        [ServiceFilter(typeof(ValidationAdvertAttribute))]
        //===============================================================================
        public async Task<IActionResult> CreateAdvertForUser([FromBody] AdvertCreationDTO advertCreationDTO) {
            User user = await GetAuthorizedUserWhitFavoriteAdsAsync(HttpContext.User, trackChanges: false).ConfigureAwait(false);

            var advert = _mapper.Map<Advert>(advertCreationDTO);

            await _repository.Tag.AddTagsAsync(advert.AdvertTags).ConfigureAwait(false);

            _repository.Advert.CreateAdvertForUser(user.Id, advert);
            await _repository.SaveAsync().ConfigureAwait(false);

            var advertToReturn = BuildAdvertDTOToReturn(advert, user);
            advertToReturn.Author = _mapper.Map<ProfileDTO>(user);

            return CreatedAtRoute("GetAdvertById", new { advertId = advertToReturn.Id }, new { advert = advertToReturn });
        }

        [HttpPut("{advertId}"), Authorize(Policy = "UserVisit"), Authorize(Policy = "UserIsOwnerAdvert")]
        [ServiceFilter(typeof(ValidationAdvertAttribute))]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> UpdateAdvertForUser(Guid advertId, [FromBody] AdvertUpdateDTO advertUpdateDTO) {
            User user = await GetAuthorizedUserWhitFavoriteAdsAsync(HttpContext.User, trackChanges: false).ConfigureAwait(false);
            var advert = HttpContext.Items["advert"] as Advert;

            _mapper.Map(advertUpdateDTO, advert);

            await _repository.Tag.AddTagsAsync(advert.AdvertTags).ConfigureAwait(false);

            _repository.Advert.UpdateAdvert(advert);
            await _repository.SaveAsync().ConfigureAwait(false);

            var advertToReturn = BuildAdvertDTOToReturn(advert, user);

            return CreatedAtRoute("GetAdvertById", new { advertId = advertToReturn.Id }, new { advert = advertToReturn });
        }

        [HttpDelete("{advertId}"), Authorize(Policy = "UserVisit"), Authorize(Policy = "UserIsOwnerAdvert")]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> DeleteAdvertById(Guid advertId) {
            var advert = HttpContext.Items["advert"] as Advert;

            Image image = advert.Images?.FirstOrDefault();

            if (image != null) {
                _fileStorageManager.DeletePhotosById(advertId);
            }

            _repository.Advert.DeleteAdvert(advert);
            await _repository.SaveAsync().ConfigureAwait(false);

            return NoContent();
        }

        [HttpPut("{advertId}/photos"), Authorize(Policy = "UserVisit"), Authorize(Policy = "UserIsOwnerAdvert")]
        [ServiceFilter(typeof(ValidationPhotoForAdvertAttribute))]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> UploadPhotoForAdvert(Guid advertId, List<IFormFile> photos) {
            User user = await GetAuthorizedUserWhitFavoriteAdsAsync(HttpContext.User, trackChanges: false).ConfigureAwait(false);
            var advert = HttpContext.Items["advert"] as Advert;

            advert.Images = await _fileStorageManager.SavePhotoByIdAsync<IFormFile>(advertId, photos).ConfigureAwait(false);

            _repository.Advert.UpdateAdvert(advert);
            await _repository.SaveAsync().ConfigureAwait(false);

            var advertToReturn = BuildAdvertDTOToReturn(advert, user);

            return CreatedAtRoute("GetAdvertById", new { advertId = advertToReturn.Id }, new { advert = advertToReturn });
        }

        [HttpDelete("{advertId}/photos"), Authorize(Policy = "UserVisit"), Authorize(Policy = "UserIsOwnerAdvert")]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        [ServiceFilter(typeof(ValidateImageExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> DeletePhotoForAdvert(Guid advertId, [FromQuery] string namePhoto) {
            User user = await GetAuthorizedUserWhitFavoriteAdsAsync(HttpContext.User, trackChanges: false).ConfigureAwait(false);
            var advert = HttpContext.Items["advert"] as Advert;
            var image = HttpContext.Items["image"] as Image;

            _fileStorageManager.DeletePhotoByPath(image.Path);

            advert.Images.Remove(image);

            _repository.Advert.UpdateAdvert(advert);
            await _repository.SaveAsync().ConfigureAwait(false);

            var advertToReturn = BuildAdvertDTOToReturn(advert, user);

            return CreatedAtRoute("GetAdvertById", new { advertId = advertToReturn.Id }, new { advert = advertToReturn });
        }

        [HttpPost("{advertId}/favorite"), Authorize]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> AddAdvertToFavorites(Guid advertId) {
            User user = await GetAuthorizedUserWhitFavoriteAdsAsync(HttpContext.User, trackChanges: true).ConfigureAwait(false);
            var advert = HttpContext.Items["advert"] as Advert;

            _repository.User.AddAdvertToFavorites(user, advertId);
            await _repository.SaveAsync().ConfigureAwait(false);

            var advertToReturn = BuildAdvertDTOToReturn(advert, user);

            return Ok(new { advert = advertToReturn });
        }

        [HttpDelete("{advertId}/favorite"), Authorize]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> DeleteAdvertFromFavorites(Guid advertId) {
            User user = await GetAuthorizedUserWhitFavoriteAdsAsync(HttpContext.User, trackChanges: true).ConfigureAwait(false);
            var advert = HttpContext.Items["advert"] as Advert;

            _repository.User.DeleteAdvertFromFavorites(user, advertId);
            await _repository.SaveAsync().ConfigureAwait(false);

            var advertToReturn = BuildAdvertDTOToReturn(advert, user);

            return Ok(new { advert = advertToReturn });
        }

        private Task<User> GetAuthorizedUserWhitFavoriteAdsAsync(ClaimsPrincipal claimsPrincipal, bool trackChanges) {
            return _authManager.TryGetUserId(claimsPrincipal, out Guid userId)
                ? _repository.User.GetUserWhitFavoriteAdvertsAsync(userId, trackChanges: trackChanges)
                : Task.FromResult<User>(null);
        }

        private AdvertDTO BuildAdvertDTOToReturn(Advert advert, User user) {
            var favoriteAdverts = user?.FavoriteAdverts?.ToList();
            return _mapper.Map<AdvertDTO>(advert, opt => opt.Items["favoriteAdverts"] = favoriteAdverts);
        }

        private IEnumerable<AdvertsDTO> BuildAdvertsDTOToReturn(IEnumerable<Advert> adverts, User user) {
            var favoriteAdverts = user?.FavoriteAdverts?.ToList();
            return _mapper.Map<IEnumerable<AdvertsDTO>>(adverts, opt => opt.Items["favoriteAdverts"] = favoriteAdverts);
        }
    }
}