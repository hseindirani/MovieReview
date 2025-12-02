using MovieReview.Data;
using MovieReview.Interfaces;
using MovieReview.Models;
using System.Diagnostics.Metrics;
namespace MovieReview.Repositories

{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataContext _context;
        public MovieRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateMovie(int studioId, int genreId, Movie movie)
        {
            var movieStudioEntity =_context.Studios.Where(s => s.Id == studioId).FirstOrDefault();
            var genre = _context.Genres.Where(s => s.Id == genreId).FirstOrDefault();

            var movieStudio = new MovieStudio()
            {
                Studio = movieStudioEntity,
                Movie = movie,

            };

            _context.Add(movieStudio);

            var movieGenre = new MovieGenre()
            {
                Genre = genre,
                Movie = movie,
            };
            _context.Add(movieGenre);
            _context.Add(movie);
            return Save();
        }

        public Movie GetMovie(int id)
        {
            return _context.Movies.Where(p => p.Id == id).FirstOrDefault();
        }

        public Movie GetMovie(string name)
        {
            return _context.Movies.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetMovieRating(int moId)
        {
            var review = _context.Reviews.Where(p => p.Movie.Id == moId);
            if (review.Count() <= 0)
                return 0;
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }
        public ICollection<Movie> GetMovies()
        {
            return _context.Movies.OrderBy(p => p.Id).ToList();
        }

        public bool MovieExist(int moId)
        {
            return _context.Movies.Any(p => p.Id == moId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ?true : false;
        }
    }
}
