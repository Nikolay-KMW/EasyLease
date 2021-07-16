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
        private readonly ILoggerManager _logger;
        private readonly UserProfileSettings _userProfileSettings;
        private readonly IMapper _mapper;

        public ProfileController(IRepositoryManager repository,
                                 ILoggerManager logger,
                                 UserProfileSettings userProfileSettings,
                                 IMapper mapper) {
            _repository = repository;
            _logger = logger;
            _userProfileSettings = userProfileSettings;
            _mapper = mapper;
        }

        [HttpGet("{userId}", Name = "GetProfileById")]
        [ServiceFilter(typeof(ValidateProfileExistsAttribute))]
        //===============================================================================
        public IActionResult GetProfileById(Guid userId) {
            // var profile = await _repository.User.GetUserAsync(userId, trackChanges: false).ConfigureAwait(false);

            // if (profile == null) {
            //     _logger.LogInfo($"User with id: {userId} doesn't exist in the database");
            //     return NotFound();
            // } else {
            //     var profileDTO = _mapper.Map<ProfileDTO>(profile);
            //     return Ok(profileDTO);
            // }
            var user = HttpContext.Items["user"] as User;

            var profileDTO = _mapper.Map<ProfileDTO>(user);
            return Ok(profileDTO);

        }

        [HttpGet("{userId}/adverts")]
        [ServiceFilter(typeof(ValidateProfileExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> GetAdvertsForUser(Guid userId) {
            // var profile = await _repository.User.GetUserAsync(userId, trackChanges: false).ConfigureAwait(false);

            // if (profile == null) {
            //     _logger.LogInfo($"User with id: {userId} doesn't exist in the database");
            //     return NotFound();
            // }

            var advertsForUser = await _repository.Advert.GetAdvertsForUserAsync(userId, trackChanges: false).ConfigureAwait(false);

            var advertsDTO = _mapper.Map<IEnumerable<AdvertsDTO>>(advertsForUser);
            return Ok(advertsDTO);
        }

        [HttpPut("settings/avatar/{userId}")]
        [ServiceFilter(typeof(ValidateProfileExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> UploadPhotoForUser(Guid userId, IFormFile avatar) {
            if (avatar == null) {
                _logger.LogError("Photo sent from client is null.");
                return BadRequest("Photo is empty");
            }

            // var profile = await _repository.User.GetUserAsync(userId, trackChanges: true).ConfigureAwait(false);

            // if (profile == null) {
            //     _logger.LogInfo($"Profile with id: {userId} doesn't exist in the database");
            //     return NotFound();
            // }

            var user = HttpContext.Items["user"] as User;

            var trustedFileNameForDisplay = WebUtility.HtmlEncode(avatar.FileName);

            var fileExtension = Path.GetExtension(avatar.FileName).ToLower();

            if (!_userProfileSettings.AllowedExtensions.Any(ext => ext == fileExtension)) {
                _logger.LogError($"Photo sent from client has extension {fileExtension}");
                return BadRequest($"Photo {trustedFileNameForDisplay} must has one of the extensions: { string.Join(',', _userProfileSettings.AllowedExtensions)}.");
            }

            using (var memoryStream = new MemoryStream()) {
                await avatar.CopyToAsync(memoryStream).ConfigureAwait(false);

                if (memoryStream.Length > _userProfileSettings.FileSizeLimitForAvatar) {
                    _logger.LogError($"Photo sent from client is more than {_userProfileSettings.FileSizeLimitForAvatar / 1024}KB.");
                    return BadRequest($"Photo {trustedFileNameForDisplay} is more than {_userProfileSettings.FileSizeLimitForAvatar / 1024}KB.");
                }

                memoryStream.Position = 0;

                using (var reader = new BinaryReader(memoryStream)) {
                    var signatures = _userProfileSettings.FileSignature[fileExtension];
                    var headerfile = reader.ReadBytes(signatures.Max(m => m.Length));

                    bool isValidSignature = signatures.Any(signature => headerfile.Take(signature.Length).SequenceEqual(signature));

                    if (!isValidSignature) {
                        _logger.LogError($"Photo sent from client has not valid signature: {fileExtension}");
                        return BadRequest($"Photo {trustedFileNameForDisplay} must has one of the extensions.: { string.Join(',', _userProfileSettings.AllowedExtensions)}.");
                    }
                }

                user.Avatar = memoryStream.ToArray();

                _repository.User.UpdateProfile(user);
                await _repository.SaveAsync().ConfigureAwait(false);
            }

            var profileToReturn = _mapper.Map<ProfileDTO>(user);

            return CreatedAtRoute("GetProfileById", new { userId = profileToReturn.Id }, profileToReturn);
        }

        [HttpDelete("settings/avatar/{userId}")]
        [ServiceFilter(typeof(ValidateProfileExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> DeletePhotoForUser(Guid userId) {
            // var profile = await _repository.User.GetUserAsync(userId, trackChanges: true).ConfigureAwait(false);

            // if (profile == null) {
            //     _logger.LogInfo($"Profile with id: {userId} doesn't exist in the database");
            //     return NotFound();
            // }

            // if (profile.Avatar == null) {
            //     _logger.LogInfo($"User with id: {userId} haven't photo");
            //     return NotFound();
            // }

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
            // if (profileUpdateDTO == null) {
            //     _logger.LogError("ProfileUpdateDTO object sent from client is null.");
            //     return BadRequest("Profile object is null");
            // }

            // if (!ModelState.IsValid) {
            //     _logger.LogError("Invalid model state for the ProfileUpdateDTO object");
            //     return UnprocessableEntity(ModelState);
            // }

            // var profile = await _repository.User.GetUserAsync(userId, trackChanges: true).ConfigureAwait(false);

            // if (profile == null) {
            //     _logger.LogInfo($"Profile with id: {userId} doesn't exist in the database");
            //     return NotFound();
            // }

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