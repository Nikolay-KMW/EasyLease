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
using EasyLease.WebAPI.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EasyLease.WebAPI.Controllers {
    [Route("api/adverts")]
    [ApiController]
    public class AdvertsController : ControllerBase {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly FileStorage _fileStorage;

        public AdvertsController(IRepositoryManager repository,
                                 FileStorage fileStorage,
                                 IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
            _fileStorage = fileStorage;
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

            advert.Images = await _fileStorage.SavePhotoByIdAsync(advertId, photos).ConfigureAwait(false);

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

            _fileStorage.DeletePhotoByPath(image.Path);

            advert.Images.Remove(image);

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
                _fileStorage.DeletePhotosById(advertId);
            }

            _repository.Advert.DeleteAdvert(advert);
            await _repository.SaveAsync().ConfigureAwait(false);

            return NoContent();
        }
    }
}