using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyLease.Contracts;
using EasyLease.Entities.DataTransferObjects;
using EasyLease.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyLease.WebAPI.Controllers {
    [Route("api/adverts")]
    [ApiController]
    public class AdvertsController : ControllerBase {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public AdvertsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper) {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AdvertDTO>> GetAdverts() {
            var adverts = _repository.Advert.GetAllAdverts(trackChanges: false);
            var advertsDTO = _mapper.Map<IEnumerable<AdvertDTO>>(adverts);

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

        [HttpPost("user/{userId}")]
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

            _repository.Advert.CreateAdvertsForUser(userId, advert);
            _repository.Save();

            var advertToReturn = _mapper.Map<AdvertDTO>(advert);
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