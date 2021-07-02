using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public IActionResult UploadPhotoForUser(Guid userId, IFormFile avatar) {
            var profile = _repository.User.GetUser(userId, trackChanges: true);

            if (profile == null) {
                _logger.LogInfo($"Profile with id: {userId} doesn't exist in the database");
                return NotFound();
            }

            if (avatar == null) {
                _logger.LogError("Photo sent from client is null.");
                return BadRequest("Photo is empty");
            }

            using (var memoryStream = new MemoryStream()) {
                avatar.CopyTo(memoryStream);

                if (memoryStream.Length < _userProfileSettings.FileSizeLimitForAvatar) {
                    profile.Avatar = memoryStream.ToArray();

                    _repository.User.UpdateProfile(profile);
                    _repository.Save();
                } else {
                    _logger.LogError($"Photo sent from client is more than {_userProfileSettings.FileSizeLimitForAvatar * 8 / 1000}Kb.");
                    return BadRequest($"Photo {avatar.FileName} is more than {_userProfileSettings.FileSizeLimitForAvatar * 8 / 1000}Kb.");
                }
            }

            // foreach (var photo in photos) {
            //     if (photo.Length > 0) {


            //         var fileExtension = Path.GetExtension(photo.FileName).ToLower();

            //         if (!_fileStorageSettings.AllowedExtensions.Any(ext => ext == fileExtension)) {
            //             _logger.LogError($"Photo sent from client has extension {fileExtension}");
            //             return BadRequest($"Photo {photo.FileName} must has one of the extensions: { string.Join(',', _fileStorageSettings.AllowedExtensions)}.");
            //         }

            //         var generatedFileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            //         var filePath = Path.Combine(path, $"{generatedFileName}{fileExtension}");

            //         using (var stream = System.IO.File.Create(filePath)) {
            //             photo.CopyTo(stream);
            //         }

            //         var filePathForDB = filePath.Substring(filePath.LastIndexOf(_fileStorageSettings.PathInWebRoot));

            //         images.Add(new Image() { Name = generatedFileName, Path = filePathForDB });
            //     }
            // }


            var profileToReturn = _mapper.Map<ProfileDTO>(profile);

            return CreatedAtRoute("GetProfileById", new { userId = profileToReturn.Id }, profileToReturn);
        }

        // [HttpPost("")]
        // public ActionResult<Profile> PostProfile(Profile model)
        // {
        //     return null;
        // }

        // [HttpPut("{id}")]
        // public IActionResult PutProfile(int id, Profile model)
        // {
        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public ActionResult<Profile> DeleteProfileById(int id)
        // {
        //     return null;
        // }
    }
}