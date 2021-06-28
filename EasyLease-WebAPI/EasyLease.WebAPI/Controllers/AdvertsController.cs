using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using EasyLease.Contracts;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;
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
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly FileStorageSettings _fileStorageSettings;
        // private readonly long _fileSizeLimit;
        // private readonly string _targetFilePath;
        // private readonly string[] _allowedExtensions;

        public AdvertsController(IRepositoryManager repository,
                                 ILoggerManager logger,
                                 IWebHostEnvironment appEnvironment,
                                 FileStorageSettings fileStorageSettings,
                                 IMapper mapper) {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _appEnvironment = appEnvironment;
            _fileStorageSettings = fileStorageSettings;

            // _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
            // _targetFilePath = config.GetValue<string>("StoredFilesInWebRootPath");
            // _allowedExtensions = config.GetValue<string[]>("AllowedExtensions");
        }

        [HttpGet]
        public ActionResult<IEnumerable<AdvertDTO>> GetAdverts() {
            var adverts = _repository.Advert.GetAllAdverts(trackChanges: false);
            var advertsDTO = _mapper.Map<IEnumerable<AdvertsDTO>>(adverts);

            return Ok(advertsDTO);
        }

        [HttpGet("{advertId}", Name = "GetAdvertById")]
        public ActionResult<AdvertDTO> GetAdvertById(Guid advertId) {
            var advert = _repository.Advert.GetAdvert(advertId, trackChanges: false);

            if (advert == null) {
                _logger.LogInfo($"Advert with id: {advertId} doesn't exist in the database");
                return NotFound();
            } else {
                var advertDTO = _mapper.Map<AdvertDTO>(advert);
                return Ok(advertDTO);
            }
        }

        [HttpPost("new/{userId}")]
        public IActionResult CreateAdvertForUser(Guid userId, [FromBody] AdvertCreationDTO advertCreationDTO) {

            if (advertCreationDTO == null) {
                _logger.LogError("AdvertCreationDTO object sent from client is null.");
                return BadRequest("Advert object is null");
            }

            var user = _repository.User.GetUser(userId, trackChanges: false);

            if (user == null) {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var advert = _mapper.Map<Advert>(advertCreationDTO);

            _repository.Tag.AddTags(advert.AdvertTags);

            _repository.Advert.CreateAdvertForUser(userId, advert);
            _repository.Save();

            var advertToReturn = _mapper.Map<AdvertDTO>(advert);
            advertToReturn.Author = _mapper.Map<ProfileDTO>(user);

            return CreatedAtRoute("GetAdvertById", new { advertId = advertToReturn.Id }, advertToReturn);
        }

        [HttpPost("new/photos/{advertId}")]
        public IActionResult UploadPhotoForAdvert(Guid advertId, List<IFormFile> photos) {

            var advert = _repository.Advert.GetAdvert(advertId, trackChanges: true);

            if (advert == null) {
                _logger.LogInfo($"Advert with id: {advertId} doesn't exist in the database");
                return NotFound();
            }

            if (photos == null) {
                _logger.LogError("Photos sent from client is null.");
                return BadRequest("Photos object is null");
            }

            ICollection<Image> images = new Collection<Image>();

            string path = _appEnvironment.WebRootPath + _fileStorageSettings.PathInWebRoot + "\\" + advertId.ToString();

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
                if (photo.Length > 0) {
                    if (photo.Length > _fileStorageSettings.FileSizeLimit) {
                        _logger.LogError("Photo sent from client is more 2 Mb.");
                        return BadRequest($"Photo {photo.FileName} is more 2 Mb.");
                    }

                    var fileExtension = Path.GetExtension(photo.FileName).ToLower();

                    if (!_fileStorageSettings.AllowedExtensions.Any(ext => ext == fileExtension)) {
                        _logger.LogError($"Photo sent from client has extension {fileExtension}");
                        return BadRequest($"Photo {photo.FileName} must has one of the extensions: { string.Join(',', _fileStorageSettings.AllowedExtensions)}.");
                    }

                    var generatedFileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
                    var filePath = Path.Combine(path, $"{generatedFileName}{fileExtension}");

                    using (var stream = System.IO.File.Create(filePath)) {
                        photo.CopyTo(stream);
                    }

                    images.Add(new Image() { Name = generatedFileName, Path = filePath });
                }
            }

            advert.Images = images;

            _repository.Advert.UpdateAdvertForUser(advert);
            _repository.Save();

            var advertToReturn = _mapper.Map<AdvertDTO>(advert);
            //advertToReturn.Author = _mapper.Map<ProfileDTO>(user);

            return CreatedAtRoute("GetAdvertById", new { advertId = advertToReturn.Id }, advertToReturn);
        }

        // [HttpPut("{id}")]
        // public IActionResult PutAdvert(int id, Advert model) {
        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public ActionResult<Advert> DeleteAdvertById(int id) {
        //     return null;
        // }
    }
}