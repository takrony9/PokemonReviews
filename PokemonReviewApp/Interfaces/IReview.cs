using PokemonReviewApp.Entities;

namespace PokemonReviewApp.Interfaces
{
    public interface IReview
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        ICollection<Review> GetReviewsOfPokemon(int id);
        bool ReviewExixts(int id);
    }
}
