using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyLease.Contracts;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;
using EasyLease.WebAPI.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyLease.WebAPI.Controllers {
    [Route("api/adverts")]
    [ApiController]
    public class AdvertsController : ControllerBase {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IAuthenticationManager _authManager;

        public AdvertsController(IRepositoryManager repository,
                                 IFileStorageManager fileStorageManager,
                                 IMapper mapper,
                                 IAuthenticationManager authManager) {
            _repository = repository;
            _mapper = mapper;
            _fileStorageManager = fileStorageManager;
            _authManager = authManager;
        }

        [HttpGet]
        //===============================================================================
        public async Task<IActionResult> GetAdverts() {
            // var user = await _authManager.GetUserFullAsync(HttpContext.User, trackChanges: false).ConfigureAwait(false);
            // var favoriteAdverts = user?.AdvertFavorites?.ToList();

            var authUser = await _authManager.GetAuthorizedUserAsync(HttpContext.User).ConfigureAwait(false);

            User user = authUser != null
                ? await _repository.User.GetUserWhitAdFavoritesAsync(authUser.Id, trackChanges: false).ConfigureAwait(false)
                : null;

            var favoriteAdverts = user?.AdvertFavorites?.ToList();

            var adverts = await _repository.Advert.GetAllAdvertsAsync(trackChanges: false).ConfigureAwait(false);

            var advertsDTO = _mapper.Map<IEnumerable<AdvertsDTO>>(adverts, opt => opt.Items["favoriteAdverts"] = favoriteAdverts);

            return Ok(new { adverts = advertsDTO });
        }

        [HttpGet("{advertId}", Name = "GetAdvertById")]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> GetAdvertById(Guid advertId) {
            // var user = await _authManager.GetUserFullAsync(HttpContext.User, trackChanges: false).ConfigureAwait(false);
            // var favoriteAdverts = user?.AdvertFavorites?.ToList();

            var authUser = await _authManager.GetAuthorizedUserAsync(HttpContext.User).ConfigureAwait(false);

            User user = authUser != null
                ? await _repository.User.GetUserWhitAdFavoritesAsync(authUser.Id, trackChanges: false).ConfigureAwait(false)
                : null;

            var favoriteAdverts = user?.AdvertFavorites?.ToList();

            var advert = HttpContext.Items["advert"] as Advert;

            var advertDTO = _mapper.Map<AdvertDTO>(advert, opt => opt.Items["favoriteAdverts"] = favoriteAdverts);
            return Ok(advertDTO);
        }

        [HttpPost("new"), Authorize]
        [ServiceFilter(typeof(ValidationAdvertAttribute))]
        //===============================================================================
        public async Task<IActionResult> CreateAdvertForUser([FromBody] AdvertCreationDTO advertCreationDTO) {
            //var user = await _authManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            User user = _authManager.TryGetUserId(HttpContext.User, out Guid userId)
                ? await _repository.User.GetUserWhitAdFavoritesAsync(userId, trackChanges: false).ConfigureAwait(false)
                : null;

            var favoriteAdverts = user?.AdvertFavorites?.ToList();

            var advert = _mapper.Map<Advert>(advertCreationDTO);

            await _repository.Tag.AddTagsAsync(advert.AdvertTags).ConfigureAwait(false);

            _repository.Advert.CreateAdvertForUser(user.Id, advert);
            await _repository.SaveAsync().ConfigureAwait(false);

            var advertToReturn = _mapper.Map<AdvertDTO>(advert, opt => opt.Items["favoriteAdverts"] = favoriteAdverts);
            advertToReturn.Author = _mapper.Map<ProfileDTO>(user);

            return CreatedAtRoute("GetAdvertById", new { advertId = advertToReturn.Id }, advertToReturn);
        }

        [HttpPut("new/photos/{advertId}"), Authorize(Policy = "UserIsOwnerAdvert")]
        [ServiceFilter(typeof(ValidationPhotoForAdvertAttribute))]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> UploadPhotoForAdvert(Guid advertId, List<IFormFile> photos) {
            // var user = await _authManager.GetUserFullAsync(HttpContext.User, trackChanges: false).ConfigureAwait(false);
            // var favoriteAdverts = user?.AdvertFavorites?.ToList();

            User user = _authManager.TryGetUserId(HttpContext.User, out Guid userId)
                ? await _repository.User.GetUserWhitAdFavoritesAsync(userId, trackChanges: false).ConfigureAwait(false)
                : null;

            var favoriteAdverts = user?.AdvertFavorites?.ToList();

            var advert = HttpContext.Items["advert"] as Advert;

            advert.Images = await _fileStorageManager.SavePhotoByIdAsync<IFormFile>(advertId, photos).ConfigureAwait(false);

            _repository.Advert.UpdateAdvert(advert);
            await _repository.SaveAsync().ConfigureAwait(false);

            var advertToReturn = _mapper.Map<AdvertDTO>(advert, opt => opt.Items["favoriteAdverts"] = favoriteAdverts);

            return CreatedAtRoute("GetAdvertById", new { advertId = advertToReturn.Id }, advertToReturn);
        }

        [HttpPut("delete/photo/{advertId}/{namePhoto}"), Authorize(Policy = "UserIsOwnerAdvert")]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        [ServiceFilter(typeof(ValidateImageExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> DeletePhotoForAdvert(Guid advertId, string namePhoto) {
            var advert = HttpContext.Items["advert"] as Advert;
            var image = HttpContext.Items["image"] as Image;

            User user = _authManager.TryGetUserId(HttpContext.User, out Guid userId)
                ? await _repository.User.GetUserWhitAdFavoritesAsync(userId, trackChanges: false).ConfigureAwait(false)
                : null;

            var favoriteAdverts = user?.AdvertFavorites?.ToList();

            _fileStorageManager.DeletePhotoByPath(image.Path);

            advert.Images.Remove(image);

            _repository.Advert.UpdateAdvert(advert);
            await _repository.SaveAsync().ConfigureAwait(false);

            var advertToReturn = _mapper.Map<AdvertDTO>(advert, opt => opt.Items["favoriteAdverts"] = favoriteAdverts);

            return CreatedAtRoute("GetAdvertById", new { advertId = advertToReturn.Id }, advertToReturn);
        }

        [HttpPut("update/{advertId}"), Authorize(Policy = "UserIsOwnerAdvert")]
        [ServiceFilter(typeof(ValidationAdvertAttribute))]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> UpdateAdvertForUser(Guid advertId, [FromBody] AdvertUpdateDTO advertUpdateDTO) {
            var advert = HttpContext.Items["advert"] as Advert;
            _mapper.Map(advertUpdateDTO, advert);

            User user = _authManager.TryGetUserId(HttpContext.User, out Guid userId)
                ? await _repository.User.GetUserWhitAdFavoritesAsync(userId, trackChanges: false).ConfigureAwait(false)
                : null;

            var favoriteAdverts = user?.AdvertFavorites?.ToList();

            await _repository.Tag.AddTagsAsync(advert.AdvertTags).ConfigureAwait(false);

            _repository.Advert.UpdateAdvert(advert);
            await _repository.SaveAsync().ConfigureAwait(false);

            var advertToReturn = _mapper.Map<AdvertDTO>(advert, opt => opt.Items["favoriteAdverts"] = favoriteAdverts);

            return CreatedAtRoute("GetAdvertById", new { advertId = advertToReturn.Id }, advertToReturn);
        }

        [HttpDelete("{advertId}"), Authorize(Policy = "UserIsOwnerAdvert")]
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

        [HttpPost("{advertId}/favorite"), Authorize]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> AddAdvertToFavorites(Guid advertId) {
            // var user = await _authManager.GetUserFullAsync(HttpContext.User, trackChanges: true).ConfigureAwait(false);
            var advert = HttpContext.Items["advert"] as Advert;

            User user = _authManager.TryGetUserId(HttpContext.User, out Guid userId)
                ? await _repository.User.GetUserWhitAdFavoritesAsync(userId, trackChanges: false).ConfigureAwait(false)
                : null;

            user.AdvertFavorites.Add(new AdvertFavorite { AdvertId = advert.Id, UserId = user.Id });

            _repository.User.AddAdvertToFavorites(user); // TODO !!!!!!!!!!!!!!!!!
            await _repository.SaveAsync().ConfigureAwait(false);

            var favoriteAdverts = user?.AdvertFavorites?.ToList();

            var advertToReturn = _mapper.Map<AdvertDTO>(advert, opt => opt.Items["favoriteAdverts"] = favoriteAdverts);

            return CreatedAtRoute("GetAdvertById", new { advertId = advertToReturn.Id }, advertToReturn);
        }
    }
}