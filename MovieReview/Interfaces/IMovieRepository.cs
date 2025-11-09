using MovieReview.Models;

namespace MovieReview.Interfaces
{
    public interface IMovieRepository
    {
        ICollection<Movie> GetMovies();   
        Movie GetMovie(int id);
        Movie GetMovie(string name);
        decimal GetMovieRating(int moId);
        bool MovieExist(int moId);
    }
}
