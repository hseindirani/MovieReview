using MovieReview.Data;
using MovieReview.Interfaces;
using MovieReview.Models;

namespace MovieReview.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;

        public GenreRepository(DataContext context)
        {
            _context = context;
        }

        bool IGenreRepository.GenreExists(int id)
        {
           return _context.Genres.Any(c => c.Id == id);
        }

        Genre IGenreRepository.GetGenreById(int id)
        {
           return _context.Genres.Where(e => e.Id == id).FirstOrDefault();
        }

        ICollection<Genre> IGenreRepository.GetGenres()
        {
            return _context.Genres.ToList();
        }

        ICollection<Movie> IGenreRepository.GetMoviesByGenre(int genreId)
        {
         return _context.MovieGenres.Where(e =>e.GenreId == genreId).Select(c=> c.Movie).ToList();
        }
    }
}
