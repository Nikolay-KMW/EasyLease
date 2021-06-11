using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLease.Contracts;
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
                var companies = _repository.Advert.GetAllAdverts(trackChanges: false);
                return Ok(companies);
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