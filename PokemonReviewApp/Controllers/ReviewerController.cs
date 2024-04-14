using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewer _reviewerRepo;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewer reviewerRepo,IMapper mapper)
        {
            _reviewerRepo = reviewerRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepo.Reviewers());
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(reviewers);
        }
        [HttpGet("{id}")]
        public IActionResult GetReviewer(int id)
        {
            if(!_reviewerRepo.ReviewerExists(id)) return NotFound();
            var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepo.GetReviewer(id));
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(reviewer);
        }
        [HttpGet("/{id}/reviews")]
        public IActionResult GetReviewsByReviewer(int id)
        {
            if (!_reviewerRepo.ReviewerExists(id)) return NotFound();
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewerRepo.GetReviewsByReviewer(id));
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(reviews);
        }
    }
}
