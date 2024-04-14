using PokemonReviewApp.Entities;

namespace PokemonReviewApp.Interfaces
{
    public interface ICountry
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByOwner(int ownerId);
        ICollection<Owner> GetOwnerFromCountry(int countryId);
        bool CountryExists(int id);
        bool CreateCountry(Country country);
        bool Save();
    }
}
