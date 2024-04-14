using PokemonReviewApp.Entities;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewer
    {
        ICollection<Reviewer> Reviewers();
        Reviewer GetReviewer(int id);
        ICollection<Review> GetReviewsByReviewer(int id);
        bool ReviewerExists(int id);
    }
}
