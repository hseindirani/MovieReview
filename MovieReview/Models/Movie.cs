namespace MovieReview.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime RealiseDate { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<MovieStudio> MovieStudios { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
