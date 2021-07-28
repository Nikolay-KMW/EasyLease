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
        [ServiceFilter(typeof(ValidationPhotoForAdvertAttribute))]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> UploadPhotoForAdvert(Guid advertId, List<IFormFile> photos) {
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
                var fileExtension = Path.GetExtension(photo.FileName).ToLower();

                var generatedFileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
                var filePath = Path.Combine(path, $"{generatedFileName}{fileExtension}");

                using (var stream = System.IO.File.Create(filePath)) {
                    await photo.CopyToAsync(stream).ConfigureAwait(false);
                }

                var filePathForDB = filePath[filePath.LastIndexOf(_fileStorageSettings.PathInWebRoot)..];

                images.Add(new Image() { Name = generatedFileName, Path = filePathForDB });
            }

            advert.Images = images;

            _repository.Advert.UpdateAdvert(advert);
            await _repository.SaveAsync().ConfigureAwait(false);

            var advertToReturn = _mapper.Map<AdvertDTO>(advert);

            return CreatedAtRoute("GetAdvertById", new { advertId = advertToReturn.Id }, advertToReturn);
        }

        [HttpPut("delete/photo/{advertId}/{namePhoto}")]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        [ServiceFilter(typeof(ValidateImageExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> DeletePhotoForAdvert(Guid advertId, string namePhoto) {
            var advert = HttpContext.Items["advert"] as Advert;
            var image = HttpContext.Items["image"] as Image;

            ICollection<Image> images = advert.Images;

            string path = _fileStorageSettings.WebRootPath + image.Path;

            if (System.IO.File.Exists(path)) {
                System.IO.File.Delete(path);
            }

            images.Remove(image);
            advert.Images = images;

            _repository.Advert.UpdateAdvert(advert);
            await _repository.SaveAsync().ConfigureAwait(false);

            var advertToReturn = _mapper.Map<AdvertDTO>(advert);

            return CreatedAtRoute("GetAdvertById", new { advertId = advertToReturn.Id }, advertToReturn);
        }

        [HttpPut("update/{advertId}")]
        [ServiceFilter(typeof(ValidationAdvertAttribute))]
        [ServiceFilter(typeof(ValidateAdvertExistsAttribute))]
        //===============================================================================
        public async Task<IActionResult> UpdateAdvertForUser(Guid advertId, [FromBody] AdvertUpdateDTO advertUpdateDTO) {
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