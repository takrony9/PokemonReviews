using PokemonReviewApp.Entities;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonRepo
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        bool PokemonExists(int id);
        bool CreatePokemon(int ownerId,int categoryId,Pokemon pokemon);
        bool Save();
    }
}
