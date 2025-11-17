namespace MovieReview.Models
{
    public class Studio
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
        public ICollection<MovieStudio> MovieStudios { get; set; }

    }
}
