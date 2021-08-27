using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EasyLease.Contracts;
using EasyLease.Entities.AppSettingsModels;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;
using EasyLease.WebAPI.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyLease.WebAPI.Controllers {
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationManager _authManager;

        public ProfileController(IRepositoryManager repository, IMapper mapper, IAuthenticationManager authManager) {
            _repository = repository;
            _mapper = mapper;
            _authManager = authManager;
        }

        [HttpGet("{userId}", Name = "GetProfileById")]
        [ServiceFilter(typeof(ValidateProfileExistsAttribute))]
        //===============================================================================
        public IActionResult GetProfileById(Guid userId) {
            var user = HttpContext.Items["user"] as User;

            var profileToReturn = _mapper.Map<ProfileDTO>(user);

            return Ok(profileToReturn);

        }

        [HttpGet("{userId}/adverts")]
        [ServiceFilter(typeof(ValidateProfileExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> GetAdvertsForUser(Guid userId) {
            var user = _authManager.TryGetUserId(HttpContext.User, out Guid authUserId)
                ? await _repository.User.GetUserWhitFavoriteAdvertsAsync(authUserId, trackChanges: false).ConfigureAwait(false)
                : null;

            var advertsForUser = await _repository.Advert.GetAdvertsForUserAsync(userId, trackChanges: false).ConfigureAwait(false);

            var favoriteAdverts = user?.FavoriteAdverts?.ToList();
            var advertsToReturn = _mapper.Map<IEnumerable<AdvertsDTO>>(advertsForUser, opt => opt.Items["favoriteAdverts"] = favoriteAdverts);

            return Ok(advertsToReturn);
        }

        [HttpPut("settings/avatar"), Authorize]
        [ServiceFilter(typeof(ValidationPhotoForUserAttribute))]
        //===============================================================================
        public async Task<IActionResult> UploadPhotoForUser(IFormFile avatar) {
            var user = await _authManager.GetAuthorizedUserAsync(HttpContext.User).ConfigureAwait(false);
            user.Avatar = HttpContext.Items["avatar"] as byte[];

            _repository.User.UpdateProfile(user);
            await _repository.SaveAsync().ConfigureAwait(false);

            var profileToReturn = _mapper.Map<ProfileDTO>(user);

            return CreatedAtRoute("GetProfileById", new { userId = profileToReturn.Id }, profileToReturn);
        }

        [HttpDelete("settings/avatar"), Authorize]
        //===============================================================================
        public async Task<IActionResult> DeletePhotoForUser() {
            var user = await _authManager.GetAuthorizedUserAsync(HttpContext.User).ConfigureAwait(false);
            user.Avatar = null;

            _repository.User.UpdateProfile(user);
            await _repository.SaveAsync().ConfigureAwait(false);

            var profileToReturn = _mapper.Map<ProfileDTO>(user);

            return CreatedAtRoute("GetProfileById", new { userId = profileToReturn.Id }, profileToReturn);
        }

        [HttpPut("settings"), Authorize]
        [ServiceFilter(typeof(ValidationProfileAttribute))]
        public async Task<IActionResult> UpdateProfile(ProfileUpdateDTO profileUpdateDTO) {
            var user = await _authManager.GetAuthorizedUserAsync(HttpContext.User).ConfigureAwait(false);

            _mapper.Map(profileUpdateDTO, user);

            _repository.User.UpdateProfile(user);
            await _repository.SaveAsync().ConfigureAwait(false);

            var profileToReturn = _mapper.Map<ProfileDTO>(user);

            return CreatedAtRoute("GetProfileById", new { userId = profileToReturn.Id }, profileToReturn);
        }

        // [HttpDelete("{id}")]
        // public ActionResult<Profile> DeleteProfileById(int id)
        // {
        //     return null;
        // }
    }
}