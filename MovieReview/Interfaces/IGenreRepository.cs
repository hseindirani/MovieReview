using MovieReview.Models;

namespace MovieReview.Interfaces
{
    public interface IGenreRepository
    {
        ICollection<Genre> GetGenres();
        Genre GetGenreById(int id);
        ICollection<Movie> GetMoviesByGenre(int  genreId);
        bool GenreExists(int id);
        bool CreateGenre(Genre genre);
        bool Save();
    }

}
