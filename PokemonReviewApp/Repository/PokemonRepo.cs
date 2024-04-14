using PokemonReviewApp.Data;
using PokemonReviewApp.Entities;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepo : IPokemonRepo
    {
        private readonly PokemonDbContext _context;
        public PokemonRepo(PokemonDbContext context)
        {
            _context = context;
        }

        public ICollection<Pokemon> GetPokemons()
        {
           return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public  Pokemon GetPokemon(int id)
        {
            return  _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public bool PokemonExists(int id)
        {
            return _context.Pokemons.Any(p=>p.Id == id);
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };


            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon,
            };
            _context.Add(pokemonOwner);
            _context.Add(pokemonCategory);
            _context.Add(pokemon);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
