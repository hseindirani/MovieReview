using MovieReview.Models;

namespace MovieReview.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int reviewerId);
        ICollection<Review> GetReviewsByAReviewer(int reviewerId); 
        bool ReviewerExists(int reviewerId);
    }
}
