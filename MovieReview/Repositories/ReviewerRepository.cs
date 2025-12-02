using Microsoft.EntityFrameworkCore;
using MovieReview.Data;
using MovieReview.Interfaces;
using MovieReview.Models;

namespace MovieReview.Repositories
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }
        public Reviewer GetReviewer(int reviewerId)
        {
            //return _context.Reviewers.Where(r => r.Id == reviewerId).Include(e => e.Id).FirstOrDefault();
            return _context.Reviewers.Include(r => r.Reviews)   
       .FirstOrDefault(r => r.Id == reviewerId);
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewsByAReviewer(int reviewerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _context.Reviewers.Any(r => reviewerId == r.Id);
        }
    }
}
