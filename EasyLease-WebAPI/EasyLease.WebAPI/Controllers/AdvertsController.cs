using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace EasyLease.WebAPI.Controllers {
    [Route("api/advers")]
    [ApiController]
    public class AdvertsController : ControllerBase {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public AdvertsController(IRepositoryManager repository, ILoggerManager logger) {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult GetAdverts() {
            try {
                var adverts = _repository.Advert.GetAllAdverts(trackChanges: false);

                var advertsDTO = adverts.Select(c => new AdvertDTO {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    CreatedAd = c.CreatedAd,
                    UpdatedAd = c.UpdatedAd,
                    Slug = c.Slug,
                    //TagList = c.AdvertTags.ToArray<string>(),
                    Favorited = false
                }).ToList();

                return Ok(advertsDTO);

            } catch (Exception exc) {
                _logger.LogError($"Something went wrong in the {nameof(GetAdverts)} action {exc}");
                return StatusCode(500, "Internal server error");
            }
        }

        // [HttpGet("{id}")]
        // public ActionResult<Advert> GetAdvertById(int id) {
        //     return null;
        // }

        // [HttpPost("")]
        // public ActionResult<Advert> PostAdvert(Advert model) {
        //     return null;
        // }

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