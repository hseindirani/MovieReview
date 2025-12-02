using MovieReview.Models;

namespace MovieReview.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewsOfAMovie(int movId);
        bool ReviewExists(int reviewId);
        bool CreateReview(Review review);
        bool Save();


    }
}
