using MovieReview.Data;
using MovieReview.Interfaces;
using MovieReview.Models;

namespace MovieReview.Repositories
{
    public class StudioRepository : IStudioRepository
    {
        private readonly DataContext _context;

        public StudioRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Movie> GetMovieByStudio(int studioId)
        {
            
              return _context.MovieStudios.Where(s =>s.Studio.Id == studioId).Select(m  => m.Movie).ToList();
        }

        public Studio GetStudio(int studioId)
        {
            return _context.Studios.Where(s => s.Id == studioId).FirstOrDefault();
        }

        public ICollection<Studio> GetStudioOfAMovie(int movId)
        {
            return _context.MovieStudios.Where(m => m.Movie.Id == movId).Select(s => s.Studio).ToList();
        }

        public ICollection<Studio> GetStudios()
        {
            return _context.Studios.OrderBy(s  => s.Id).ToList();
        }

        public bool StudioExists(int studioId)
        {
            return _context.Countries.Any(s => s.Id == studioId);
        }
    }
}
