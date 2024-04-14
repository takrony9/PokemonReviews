using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Entities;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategory _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryController(ICategory categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepo.GetCategories());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult GetCategory(int id)
        {
            if (!_categoryRepo.CategoryExists(id))
            {
                return NotFound();
            }
            var category = _mapper.Map<CategoryDto>(_categoryRepo.GetCategory(id));
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }
        [HttpGet("/pokemon/{categoryId}")]
        [ProducesResponseType(200)]
        public IActionResult GetPokemonsByCategory(int categoryId)
        {
            if (!_categoryRepo.CategoryExists(categoryId))
            {
                return NotFound();
            }
            var pokemons = _mapper.Map<List<PokemonDto>>(_categoryRepo.GetPokemonsByCategory(categoryId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemons);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            var category = _categoryRepo.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if (!_categoryRepo.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public IActionResult UpdateCategory(int id,[FromBody] CategoryDto categoryUpdate)
        {
            if (categoryUpdate == null)
                return BadRequest(ModelState);

            if (id != categoryUpdate.Id)
                return BadRequest(ModelState);

            if (!_categoryRepo.CategoryExists(id)) return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var categoryMap = _mapper.Map<Category>(categoryUpdate);

            if (!_categoryRepo.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryRepo.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var categoryToDelete = _categoryRepo.GetCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepo.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }


    }
}
