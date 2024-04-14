using PokemonReviewApp.Data;
using PokemonReviewApp.Entities;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepo : IOwner
    {
        private readonly PokemonDbContext _context;
        public OwnerRepo(PokemonDbContext context)
        {
            _context = context;            
        }
        public Owner GetOwner(int ownerId)
        {
           return _context.Owners.Where(p=>p.Id==ownerId).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfPokemaon(int pokemonId)
        {
            return _context.PokemonOwners.Where(p=>p.Pokemon.Id==pokemonId).Select(p=>p.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return _context.PokemonOwners.Where(p => p.Owner.Id == ownerId).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(p=>p.Id == ownerId);
        }
        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }
        public bool UpdateOwner(Owner owner)
        {
            _context.Update(owner);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
