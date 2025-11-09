using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieReview.Interfaces;
using MovieReview.Models;

namespace MovieReview.Repositories
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movierepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movierepository = movieRepository;

        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        public IActionResult GetMovies()
        {
            var movies = _movierepository.GetMovies();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(movies);
        
        }
    }
}
