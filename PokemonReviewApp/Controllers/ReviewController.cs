using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReview _reviweRepo;
        private readonly IMapper _mapper;

        public ReviewController(IReview reviweRepo,IMapper mapper)
        {
            _reviweRepo = reviweRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviweRepo.GetReviews());
            if (!ModelState.IsValid) return BadRequest();
            return Ok(reviews);
        }
        [HttpGet("{id}")]
        public IActionResult GetReview(int id)
        {
            if (!_reviweRepo.ReviewExixts(id)) return NotFound();
            var review = _mapper.Map<List<ReviewDto>>(_reviweRepo.GetReview(id));
            if (!ModelState.IsValid) return BadRequest();
            return Ok(review);
        }
        [HttpGet("/{id}/pokemon")]
        public IActionResult GetReviewsOfPokemon(int id)
        {
            //if (!_reviweRepo.ReviewExixts(id)) return NotFound();
            var reviews = _mapper.Map<List<ReviewDto>>(_reviweRepo.GetReviewsOfPokemon(id));
            if (!ModelState.IsValid) return BadRequest();
            return Ok(reviews);
        }
    }
}
