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
    public class PokemonController : Controller
    {
        private readonly IPokemonRepo _pokemonRepo;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepo pokemonRepo,IMapper mapper)
        {
            _pokemonRepo = pokemonRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetPokemons()
        {
            var pokemon = _mapper.Map<List< PokemonDto>>(_pokemonRepo.GetPokemons());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemon);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult GetPokemon(int id)
        {
            if(!_pokemonRepo.PokemonExists(id))
            {
                return NotFound();
            }
            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepo.GetPokemon(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemon);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
                return BadRequest(ModelState);

            var pokemon = _pokemonRepo.GetPokemons()
                .Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (pokemon != null)
            {
                ModelState.AddModelError("", "Pokemon already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var poke = _mapper.Map<Pokemon>(pokemon);

            if (!_pokemonRepo.CreatePokemon(ownerId,categoryId,poke))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
