using MovieReview.Models;

namespace MovieReview.Interfaces
{
    public interface IStudioRepository
    {
        ICollection<Studio> GetStudios();
        Studio GetStudio(int studioId);
        ICollection<Studio> GetStudioOfAMovie(int movId);
        ICollection<Movie> GetMovieByStudio( int studioId);
        bool StudioExists(int studioId);
        bool CreateStudio(Studio studio);
        bool UpdateStudio(Studio studio);
        bool DeleteStudio(Studio studio);

        bool Save();

    }
}
