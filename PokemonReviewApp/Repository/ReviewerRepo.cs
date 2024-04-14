using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Entities;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Repository
{
    public class ReviewerRepo : IReviewer
    {
        private readonly PokemonDbContext _context;
        public ReviewerRepo(PokemonDbContext context)
        {
            _context = context;
        }
        public Reviewer GetReviewer(int id)
        {
            return _context.Reviewers.Where(r => r.Id == id).Include(p=>p.Reviews).FirstOrDefault();
        }

        public ICollection<Review> GetReviewsByReviewer(int id)
        {
            return _context.Reviews.Where(p=>p.Reviewer.Id == id).ToList(); 
        }

        public bool ReviewerExists(int id)
        {
            return _context.Reviews.Any(review => review.Id == id);
        }

        public ICollection<Reviewer> Reviewers()
        {
            return _context.Reviewers.ToList();
        }
    }
}
