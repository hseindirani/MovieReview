using MovieReview.Models;

namespace MovieReview.Dto
{
    public class StudioDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
    }
}
public class CreateStudioDto
{
   
    public string Name { get; set; }
    public int CountryId { get; set; }


}
