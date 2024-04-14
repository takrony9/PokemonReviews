using PokemonReviewApp.Data;
using PokemonReviewApp.Entities;
using PokemonReviewApp.Interfaces;
using System.Collections.Immutable;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepo : IReview
    {
        private readonly PokemonDbContext _context;
        public ReviewRepo(PokemonDbContext context)
        {
            _context = context;
        }
        public Review GetReview(int id)
        {
            return _context.Reviews.Where(p=>p.Id==id).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsOfPokemon(int id)
        {
            return _context.Reviews.Where(p=>p.Pokemon.Id==id).ToList();    
        }

        public bool ReviewExixts(int id)
        {
            return _context.Reviews.Any(p=>p.Id==id);
        }
    }
}
