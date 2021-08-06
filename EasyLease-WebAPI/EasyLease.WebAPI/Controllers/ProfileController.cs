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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyLease.WebAPI.Controllers {
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public ProfileController(IRepositoryManager repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{userId}", Name = "GetProfileById")]
        [ServiceFilter(typeof(ValidateProfileExistsAttribute))]
        //===============================================================================
        public IActionResult GetProfileById(Guid userId) {
            var user = HttpContext.Items["user"] as User;

            var profileDTO = _mapper.Map<ProfileDTO>(user);
            return Ok(profileDTO);

        }

        [HttpGet("{userId}/adverts")]
        [ServiceFilter(typeof(ValidateProfileExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> GetAdvertsForUser(Guid userId) {
            var advertsForUser = await _repository.Advert.GetAdvertsForUserAsync(userId, trackChanges: false).ConfigureAwait(false);

            var advertsDTO = _mapper.Map<IEnumerable<AdvertsDTO>>(advertsForUser);
            return Ok(advertsDTO);
        }

        [HttpPut("settings/avatar/{userId}")]
        [ServiceFilter(typeof(ValidateProfileExistsAttribute))]
        [ServiceFilter(typeof(ValidationPhotoForUserAttribute))]
        //===============================================================================
        public async Task<IActionResult> UploadPhotoForUser(Guid userId, IFormFile avatar) {
            var user = HttpContext.Items["user"] as User;
            user.Avatar = HttpContext.Items["avatar"] as byte[];

            _repository.User.UpdateProfile(user);
            await _repository.SaveAsync().ConfigureAwait(false);

            var profileToReturn = _mapper.Map<ProfileDTO>(user);

            return CreatedAtRoute("GetProfileById", new { userId = profileToReturn.Id }, profileToReturn);
        }

        [HttpDelete("settings/avatar/{userId}")]
        [ServiceFilter(typeof(ValidateProfileExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> DeletePhotoForUser(Guid userId) {
            var user = HttpContext.Items["user"] as User;

            user.Avatar = null;

            _repository.User.UpdateProfile(user);
            await _repository.SaveAsync().ConfigureAwait(false);

            var profileToReturn = _mapper.Map<ProfileDTO>(user);

            return CreatedAtRoute("GetProfileById", new { userId = profileToReturn.Id }, profileToReturn);
        }

        // [HttpPost("")]
        // public ActionResult<Profile> PostProfile(Profile model)
        // {
        //     return null;
        // }

        [HttpPut("settings/{userId}")]
        [ServiceFilter(typeof(ValidationProfileAttribute))]
        [ServiceFilter(typeof(ValidateProfileExistsAttribute))]
        public async Task<IActionResult> UpdateProfile(Guid userId, ProfileUpdateDTO profileUpdateDTO) {
            var user = HttpContext.Items["user"] as User;

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