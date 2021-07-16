using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using EasyLease.Contracts;
using EasyLease.Entities.AppSettingsModels;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;
using EasyLease.WebAPI.ActionFilters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EasyLease.WebAPI.Controllers {
    [Route("api/adverts")]
    [ApiController]
    public class AdvertsController : ControllerBase {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly FileStorageSettings _fileStorageSettings;
        private readonly GeneralSettings _generalSettings;

        public AdvertsController(IRepositoryManager repository,
                                 ILoggerManager logger,
                                 FileStorageSettings fileStorageSettings,
                                 GeneralSettings generalSettings,
                                 IMapper mapper) {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _fileStorageSettings = fileStorageSettings;
            _generalSettings = generalSettings;
        }

        [HttpGet]
        //===============================================================================
        public async Task<IActionResult> GetAdverts() {
            var adverts = await _repository.Advert.GetAllAdvertsAsync(trackChanges: false).ConfigureAwait(false);
            var advertsDTO = _mapper.Map<IEnumerable<AdvertsDTO>>(adverts);

            return Ok(advertsDTO);
        }

        [HttpGet("{advertId}", Name = "GetAdvertById")]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        //===============================================================================
        public IActionResult GetAdvertById(Guid advertId) {
            var advert = HttpContext.Items["advert"] as Advert;

            var advertDTO = _mapper.Map<AdvertDTO>(advert);
            return Ok(advertDTO);
        }

        [HttpPost("new/{userId}")]
        [ServiceFilter(typeof(ValidationAdvertAttribute))]
        [ServiceFilter(typeof(ValidateProfileExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> CreateAdvertForUser(Guid userId, [FromBody] AdvertCreationDTO advertCreationDTO) {
            // if (advertCreationDTO == null) {
            //     _logger.LogError("AdvertCreationDTO object sent from client is null.");
            //     return BadRequest("Advert object is null");
            // }

            // var user = await _repository.User.GetUserAsync(userId, trackChanges: false).ConfigureAwait(false);

            // if (user == null) {
            //     _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
            //     return NotFound();
            // }

            // DateTime currenDateTime = DateTime.UtcNow.AddHours(_generalSettings.HoursOffsetForUkraine);

            // if (advertCreationDTO.StartOfLease < currenDateTime) {
            //     _logger.LogInfo($"The date start of lease set {advertCreationDTO.StartOfLease} is earlier than now {currenDateTime}.");
            //     ModelState.AddModelError(nameof(advertCreationDTO.StartOfLease), "The date start of lease is invalid");
            // }

            // if (advertCreationDTO.EndOfLease != null) {
            //     if (advertCreationDTO.EndOfLease < advertCreationDTO.StartOfLease) {
            //         _logger.LogInfo($"The date end of lease set {advertCreationDTO.EndOfLease} is earlier than the date start of lease {advertCreationDTO.StartOfLease}.");
            //         ModelState.AddModelError(nameof(advertCreationDTO.EndOfLease), "The date end of lease is invalid");
            //     }
            // }

            // if (!ModelState.IsValid) {
            //     _logger.LogError("Invalid model state for the AdvertCreationDTO object");
            //     return UnprocessableEntity(ModelState);
            // }

            var user = HttpContext.Items["user"] as User;
            var advert = _mapper.Map<Advert>(advertCreationDTO);

            await _repository.Tag.AddTagsAsync(advert.AdvertTags).ConfigureAwait(false);

            _repository.Advert.CreateAdvertForUser(userId, advert);
            await _repository.SaveAsync().ConfigureAwait(false);

            var advertToReturn = _mapper.Map<AdvertDTO>(advert);
            advertToReturn.Author = _mapper.Map<ProfileDTO>(user);

            return CreatedAtRoute("GetAdvertById", new { advertId = advertToReturn.Id }, advertToReturn);
        }

        [HttpPut("new/photos/{advertId}")]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> UploadPhotoForAdvert(Guid advertId, List<IFormFile> photos) {
            if (photos.Count == 0) {
                _logger.LogError("Photo sent from client is null.");
                return BadRequest("Photo is null");
            }

            if (photos.Count > _fileStorageSettings.NumberOfFilesLimit) {
                _logger.LogError($"Number of photos sent from client is more than {_fileStorageSettings.NumberOfFilesLimit}.");
                return BadRequest($"Number of photos is more than {_fileStorageSettings.NumberOfFilesLimit}.");
            }

            // var advert = await _repository.Advert.GetAdvertAsync(advertId, trackChanges: true).ConfigureAwait(false);

            // if (advert == null) {
            //     _logger.LogInfo($"Advert with id: {advertId} doesn't exist in the database");
            //     return NotFound();
            // }

            var advert = HttpContext.Items["advert"] as Advert;

            string path = _fileStorageSettings.FullPath + "\\" + advert.Id.ToString();

            ICollection<Image> images = new Collection<Image>();

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists) {
                dirInfo.Create();
            }

            var files = dirInfo.GetFiles();
            if (files != null) {
                foreach (var file in files) {
                    file.Delete();
                }
            }

            foreach (var photo in photos) {
                var trustedFileNameForDisplay = WebUtility.HtmlEncode(photo.FileName);

                if (photo.Length > 0) {
                    if (photo.Length > _fileStorageSettings.FileSizeLimit) {
                        _logger.LogError($"Photo sent from client is more than {_fileStorageSettings.FileSizeLimit / (1024 * 1024)} Mb.");
                        return BadRequest($"Photo {trustedFileNameForDisplay} is more than {_fileStorageSettings.FileSizeLimit / (1024 * 1024)} Mb.");
                    }

                    var fileExtension = Path.GetExtension(photo.FileName).ToLower();

                    if (!_fileStorageSettings.AllowedExtensions.Any(ext => ext == fileExtension)) {
                        _logger.LogError($"Photo sent from client has extension {fileExtension}");
                        return BadRequest($"Photo {trustedFileNameForDisplay} must has one of the extensions: { string.Join(',', _fileStorageSettings.AllowedExtensions)}.");
                    }

                    using (var reader = new BinaryReader(photo.OpenReadStream())) {
                        var signatures = _fileStorageSettings.FileSignature[fileExtension];
                        var headerfile = reader.ReadBytes(signatures.Max(m => m.Length));

                        bool isValidSignature = signatures.Any(signature => headerfile.Take(signature.Length).SequenceEqual(signature));

                        if (!isValidSignature) {
                            _logger.LogError($"Photo sent from client has not valid signature: {fileExtension}");
                            return BadRequest($"Photo {trustedFileNameForDisplay} must has one of the extensions.: { string.Join(',', _fileStorageSettings.AllowedExtensions)}.");
                        }
                    }

                    var generatedFileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
                    var filePath = Path.Combine(path, $"{generatedFileName}{fileExtension}");

                    using (var stream = System.IO.File.Create(filePath)) {
                        await photo.CopyToAsync(stream).ConfigureAwait(false);
                    }

                    var filePathForDB = filePath[filePath.LastIndexOf(_fileStorageSettings.PathInWebRoot)..];

                    images.Add(new Image() { Name = generatedFileName, Path = filePathForDB });
                }
            }

            advert.Images = images;

            _repository.Advert.UpdateAdvert(advert);
            await _repository.SaveAsync().ConfigureAwait(false);

            var advertToReturn = _mapper.Map<AdvertDTO>(advert);

            return CreatedAtRoute("GetAdvertById", new { advertId = advertToReturn.Id }, advertToReturn);
        }

        [HttpPut("delete/photo/{advertId}/{namePhoto}")]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> DeletePhotoForAdvert(Guid advertId, string namePhoto) {
            // var advert = await _repository.Advert.GetAdvertAsync(advertId, trackChanges: true).ConfigureAwait(false);

            // if (advert == null) {
            //     _logger.LogInfo($"Advert with id: {advertId} doesn't exist in the database");
            //     return NotFound();
            // }

            var advert = HttpContext.Items["advert"] as Advert;

            var trustedFileNameForDisplay = WebUtility.HtmlEncode(namePhoto);
            var namePhotoWithoutExtension = Path.GetFileNameWithoutExtension(namePhoto);

            ICollection<Image> images = advert.Images;

            if (images == null) {
                _logger.LogInfo($"Advert with id: {advertId} haven't photo");
                return NotFound();
            }

            Image image = images.FirstOrDefault(image => image.Name == namePhotoWithoutExtension);

            if (image == null) {
                _logger.LogInfo($"Photo {trustedFileNameForDisplay} not found");
                return NotFound();
            }

            string path = _fileStorageSettings.WebRootPath + image.Path;

            if (System.IO.File.Exists(path)) {
                System.IO.File.Delete(path);
            }

            if (!images.Remove(image)) {
                _logger.LogError("Image not remove from images collection.");
                return BadRequest($"Photo {trustedFileNameForDisplay} not remove.");
            }
            advert.Images = images;

            _repository.Advert.UpdateAdvert(advert);
            await _repository.SaveAsync().ConfigureAwait(false);

            var advertToReturn = _mapper.Map<AdvertDTO>(advert);

            return CreatedAtRoute("GetAdvertById", new { advertId = advertToReturn.Id }, advertToReturn);
        }

        [HttpPut("update/{advertId}")]
        [ServiceFilter(typeof(ValidationAdvertAttribute))]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        public async Task<IActionResult> UpdateAdvertForUser(Guid advertId, [FromBody] AdvertUpdateDTO advertUpdateDTO) {
            // if (advertUpdateDTO == null) {
            //     _logger.LogError("AdvertUpdateDTO object sent from client is null.");
            //     return BadRequest("Advert object is null");
            // }

            // if (!ModelState.IsValid) {
            //     _logger.LogError("Invalid model state for the AdvertUpdateDTO object");
            //     return UnprocessableEntity(ModelState);
            // }

            // var advert = await _repository.Advert.GetAdvertAsync(advertId, trackChanges: true).ConfigureAwait(false);

            // if (advert == null) {
            //     _logger.LogInfo($"Advert with id: {advertId} doesn't exist in the database");
            //     return NotFound();
            // }

            // DateTime currenDateTime = DateTime.UtcNow.AddHours(_generalSettings.HoursOffsetForUkraine);

            // if (advertUpdateDTO.StartOfLease < currenDateTime) {
            //     _logger.LogInfo($"The date start of lease set {advertUpdateDTO.StartOfLease} is earlier than now {currenDateTime}.");
            //     return BadRequest("The date start of lease is invalid");
            // }

            // if (advertUpdateDTO.EndOfLease != null) {
            //     if (advertUpdateDTO.EndOfLease < advertUpdateDTO.StartOfLease) {
            //         _logger.LogInfo($"The date end of lease set {advertUpdateDTO.EndOfLease} is earlier than the date start of lease {advertUpdateDTO.StartOfLease}.");
            //         return BadRequest("The date end of lease is invalid");
            //     }
            // }

            var advert = HttpContext.Items["advert"] as Advert;
            _mapper.Map(advertUpdateDTO, advert);

            await _repository.Tag.AddTagsAsync(advert.AdvertTags).ConfigureAwait(false);

            _repository.Advert.UpdateAdvert(advert);
            await _repository.SaveAsync().ConfigureAwait(false);

            var advertToReturn = _mapper.Map<AdvertDTO>(advert);

            return CreatedAtRoute("GetAdvertById", new { advertId = advertToReturn.Id }, advertToReturn);
        }

        [HttpDelete("{advertId}")]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> DeleteAdvertById(Guid advertId) {
            // var advert = await _repository.Advert.GetAdvertAsync(advertId, trackChanges: false).ConfigureAwait(false);

            // if (advert == null) {
            //     _logger.LogInfo($"Advert with id: {advertId} doesn't exist in the database");
            //     return NotFound();
            // }

            var advert = HttpContext.Items["advert"] as Advert;

            Image image = advert.Images?.FirstOrDefault();

            if (image != null) {
                string path = _fileStorageSettings.WebRootPath + image.Path[..image.Path.IndexOf(image.Name)];

                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (dirInfo.Exists) {
                    dirInfo.Delete(true);
                }
            }

            _repository.Advert.DeleteAdvert(advert);
            await _repository.SaveAsync().ConfigureAwait(false);

            return NoContent();
        }
    }
}