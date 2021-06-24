using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyLease.Contracts;
using EasyLease.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace EasyLease.WebAPI.Controllers {
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ProfileController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper) {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
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