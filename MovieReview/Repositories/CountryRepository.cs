using AutoMapper;
using MovieReview.Data;
using MovieReview.Interfaces;
using MovieReview.Models;

namespace MovieReview.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CountryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        bool ICountryRepository.CountryExist(int id)
        {
           return _context.Countries.Any(c => c.Id == id);
        }

        ICollection<Country> ICountryRepository.GetCountries()
        {
           return _context.Countries.ToList();
        }

        Country ICountryRepository.GetCountry(int id)
        {
           return _context.Countries.Where(c => c.Id == id).FirstOrDefault();
        }

        Country ICountryRepository.GetCountryByStudio(int studioId)
        {
            return _context.Studios.Where(s => s.Id == studioId).Select(c => c.Country).FirstOrDefault();
        }
    }
}
