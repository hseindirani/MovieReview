namespace MovieReview.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Studio> Studios { get; set; }
    }
}
