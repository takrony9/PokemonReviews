using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Entities;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountry _countryRepo;
        private readonly IMapper _mapper;

        public CountryController(ICountry countryRepo, IMapper mapper)
        {
            _countryRepo = countryRepo;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepo.GetCountries());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(countries);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult GetCountry(int id)
        {
            if (!_countryRepo.CountryExists(id)) return NotFound();
            var country = _mapper.Map<CountryDto>(_countryRepo.GetCountry(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(country);
        }
        [HttpGet("owners/{ownerId}")]
        [ProducesResponseType(200)]
        public IActionResult GetCountryOfAnOwner(int ownerId)
        {
            
            var country = _mapper.Map<CountryDto>(_countryRepo.GetCountryByOwner(ownerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(country);
        }
        [HttpGet("country/{countryId}")]
        [ProducesResponseType(200)]
        public IActionResult GeOwnersFromCountry(int countryId)
        {

            var owners = _mapper.Map<OwnerDto>(_countryRepo.GetOwnerFromCountry(countryId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owners);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
        {
            if (countryCreate == null)
                return BadRequest(ModelState);

            var category = _countryRepo.GetCountries()
                .Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryMap = _mapper.Map<Country>(countryCreate);

            if (!_countryRepo.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
