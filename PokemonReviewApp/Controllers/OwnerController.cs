using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Entities;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwner _ownerRepo;
        private readonly ICountry _countryRepo;
        private readonly IMapper _mapper;

        public OwnerController(IOwner ownerRepo,ICountry countryRepo,IMapper mapper)
        {
            _ownerRepo = ownerRepo;
            _countryRepo = countryRepo;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRepo.GetOwners());
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(owners);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult GetOwner(int id)
        {
            if (!_ownerRepo.OwnerExists(id)) return NotFound();
            var owner = _mapper.Map<OwnerDto>(_ownerRepo.GetOwner(id));
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(owner);
        }
        [HttpGet("/pokemon/{pokemonId}")]
        [ProducesResponseType(200)]
        public IActionResult GetOwnerOfPokemon(int pokemonId)
        {
            //if (!_ownerRepo.OwnerExists(pokemonId)) return NotFound();
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRepo.GetOwnerOfPokemaon(pokemonId));
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(owners);
        }
        [HttpGet("/owners/{ownerId}")]
        [ProducesResponseType(200)]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            if (!_ownerRepo.OwnerExists(ownerId)) return NotFound();
            var owners = _mapper.Map<List<PokemonDto>>(_ownerRepo.GetPokemonByOwner(ownerId));
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(owners);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreateOwner([FromQuery] int countryId,[FromBody] OwnerDto ownerCreate)
        {
            if (ownerCreate == null)
                return BadRequest(ModelState);

            var category = _ownerRepo.GetOwners()
                .Where(c => c.Name.Trim().ToUpper() == ownerCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var owner = _mapper.Map<Owner>(ownerCreate);
            owner.Country = _countryRepo.GetCountry(countryId);

            if (!_ownerRepo.CreateOwner(owner))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateOwner(int id,OwnerDto updatedOwner)
        {
            if (updatedOwner == null)
                return BadRequest(ModelState);

            if (id != updatedOwner.Id)
                return BadRequest(ModelState);

            if (!_ownerRepo.OwnerExists(id)) return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var owner = _mapper.Map<Owner>(updatedOwner);
            _ownerRepo.UpdateOwner(owner);
            return NoContent();
        }
    }
}
