using PokemonReviewApp.Data;
using PokemonReviewApp.Entities;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Repository
{
    public class CountryRepo : ICountry
    {
        private readonly PokemonDbContext _context;
        public CountryRepo(PokemonDbContext context)
        {
            _context = context;
        }
        public bool CountryExists(int id)
        {
            return _context.Countries.Any(p => p.Id == id);
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(p => p.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return _context.Owners.Where(p => p.Id == ownerId).Select(p=>p.Country).FirstOrDefault();
            
        }

        public ICollection<Owner> GetOwnerFromCountry(int countryId)
        {
            return _context.Owners.Where(p => p.Country.Id == countryId).ToList();
        }
        public bool CreateCountry(Country country)
        {
            _context.Add(country);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
