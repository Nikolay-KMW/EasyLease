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
        //===============================================================================
        public ActionResult<ProfileDTO> GetProfileById(Guid userId) {
            var profile = _repository.User.GetUser(userId, trackChanges: false);

            if (profile == null) {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database");
                return NotFound();
            } else {
                var profileDTO = _mapper.Map<ProfileDTO>(profile);
                return Ok(profileDTO);
            }
        }

        [HttpGet("{userId}/adverts")]
        //===============================================================================
        public ActionResult<IEnumerable<AdvertDTO>> GetAdvertsForProfile(Guid userId) {
            var profile = _repository.User.GetUser(userId, trackChanges: false);

            if (profile == null) {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database");
                return NotFound();
            }

            var advertsForUser = _repository.Advert.GetAdvertsForUser(userId, trackChanges: false);

            var advertsDTO = _mapper.Map<IEnumerable<AdvertsDTO>>(advertsForUser);
            return Ok(advertsDTO);
        }

        [HttpPost("settings/avatar/{userId}")]
        //===============================================================================
        public IActionResult UploadPhotoForUser(Guid userId, IFormFile avatar) {
            if (avatar == null) {
                _logger.LogError("Photo sent from client is null.");
                return BadRequest("Photo is empty");
            }

            var profile = _repository.User.GetUser(userId, trackChanges: true);

            if (profile == null) {
                _logger.LogInfo($"Profile with id: {userId} doesn't exist in the database");
                return NotFound();
            }

            var trustedFileNameForDisplay = WebUtility.HtmlEncode(avatar.FileName);

            var fileExtension = Path.GetExtension(avatar.FileName).ToLower();

            if (!_userProfileSettings.AllowedExtensions.Any(ext => ext == fileExtension)) {
                _logger.LogError($"Photo sent from client has extension {fileExtension}");
                return BadRequest($"Photo {trustedFileNameForDisplay} must has one of the extensions: { string.Join(',', _userProfileSettings.AllowedExtensions)}.");
            }

            using (var memoryStream = new MemoryStream()) {
                avatar.CopyTo(memoryStream);

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

                profile.Avatar = memoryStream.ToArray();

                _repository.User.UpdateProfile(profile);
                _repository.Save();
            }

            var profileToReturn = _mapper.Map<ProfileDTO>(profile);

            return CreatedAtRoute("GetProfileById", new { userId = profileToReturn.Id }, profileToReturn);
        }

        [HttpDelete("settings/avatar/{userId}")]
        //===============================================================================
        public IActionResult DeletePhotoForUser(Guid userId) {
            var profile = _repository.User.GetUser(userId, trackChanges: true);

            if (profile == null) {
                _logger.LogInfo($"Profile with id: {userId} doesn't exist in the database");
                return NotFound();
            }

            if (profile.Avatar == null) {
                _logger.LogInfo($"User with id: {userId} haven't photo");
                return NotFound();
            }

            profile.Avatar = null;

            _repository.User.UpdateProfile(profile);
            _repository.Save();

            var profileToReturn = _mapper.Map<ProfileDTO>(profile);

            return CreatedAtRoute("GetProfileById", new { userId = profileToReturn.Id }, profileToReturn);
        }

        // [HttpPost("")]
        // public ActionResult<Profile> PostProfile(Profile model)
        // {
        //     return null;
        // }

        [HttpPut("settings/{userId}")]
        public IActionResult UpdateProfile(Guid userId, ProfileUpdateDTO profileUpdateDTO) {
            if (profileUpdateDTO == null) {
                _logger.LogError("ProfileUpdateDTO object sent from client is null.");
                return BadRequest("Profile object is null");
            }

            if (!ModelState.IsValid) {
                _logger.LogError("Invalid model state for the ProfileUpdateDTO object");
                return UnprocessableEntity(ModelState);
            }

            var profile = _repository.User.GetUser(userId, trackChanges: true);

            if (profile == null) {
                _logger.LogInfo($"Profile with id: {userId} doesn't exist in the database");
                return NotFound();
            }

            _mapper.Map(profileUpdateDTO, profile);

            _repository.User.UpdateProfile(profile);
            _repository.Save();

            var profileToReturn = _mapper.Map<ProfileDTO>(profile);

            return CreatedAtRoute("GetProfileById", new { userId = profileToReturn.Id }, profileToReturn);
        }

        // [HttpDelete("{id}")]
        // public ActionResult<Profile> DeleteProfileById(int id)
        // {
        //     return null;
        // }
    }
}