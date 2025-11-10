using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieReview.Dto;
using MovieReview.Interfaces;
using MovieReview.Models;

namespace MovieReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        public IActionResult GetMovies()
        {
            //var movies = _movieRepository.GetMovies();
            var movies = _mapper.Map<List<MovieDto>>(_movieRepository.GetMovies());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(movies);
        }
        [HttpGet("{moId}")]
        [ProducesResponseType(200, Type = typeof(Movie))]
        [ProducesResponseType(400)]
        public IActionResult GetMovie(int moId)
        {
            if (!_movieRepository.MovieExist(moId))
                return NotFound();
            var movie = _mapper.Map<MovieDto>(_movieRepository.GetMovie(moId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(movie);




        }
        [HttpGet("{moId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]

        public IActionResult GetMovieRating(int moId)
        {
            if(!_movieRepository.MovieExist(moId))
                return NotFound();
            var rating =_movieRepository.GetMovieRating(moId);
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(rating);


        }
    }
}
