using MovieReview.Models;

namespace MovieReview.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByStudio(int studioId);
        bool CountryExist(int id);
        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);
        bool Save();

    }
}
