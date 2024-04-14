using PokemonReviewApp.Entities;

namespace PokemonReviewApp.Interfaces
{
    public interface IOwner
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int ownerId);
        ICollection<Owner> GetOwnerOfPokemaon(int pokemonId);
        ICollection<Pokemon> GetPokemonByOwner(int ownerId);
        bool OwnerExists(int ownerId);

        bool CreateOwner(Owner owner);
        bool UpdateOwner(Owner owner);
        bool Save();
    }
}
