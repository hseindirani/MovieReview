using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieReview.Dto;
using MovieReview.Interfaces;
using MovieReview.Models;
using MovieReview.Repositories;

namespace MovieReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IStudioRepository _studioRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public MovieController(IMovieRepository movieRepository,IStudioRepository studioRepository,IGenreRepository genreRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _studioRepository = studioRepository;
            _genreRepository = genreRepository;
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
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateMovie([FromQuery]int studioId,[FromQuery] int genreId,[FromBody] MovieDto movieCreate)
        {
            if (movieCreate == null)
                return BadRequest(ModelState);

            var movie = _movieRepository.GetMovies()
                        .Where(m => m.Name.Trim().ToUpper() == movieCreate.Name.TrimEnd().ToUpper())
                         .FirstOrDefault();
            if (movie != null)
            {
                ModelState.AddModelError("", "Movie already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var movieMap = _mapper.Map<Movie>(movieCreate);

            if (!_movieRepository.CreateMovie(studioId,genreId,movieMap))
            {
                ModelState.AddModelError("", "Something went wromg while saving ");
                return StatusCode(500, ModelState);

            }
            return Ok("Successfully created");


        }
        [HttpPut("{movieId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateMovie(int movieId,[FromQuery] int studioId,[FromQuery] int genreId, [FromBody] MovieDto updatedMovie)
        {
            if (updatedMovie == null)
                return BadRequest(ModelState);

            if (movieId != updatedMovie.Id)
                return BadRequest(ModelState);

            if (!_movieRepository.MovieExist(movieId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();
            var movieMap = _mapper.Map<Movie>(updatedMovie);

            if (!_movieRepository.UpdateMovie(studioId,genreId,movieMap))
            {

                ModelState.AddModelError("", "something went wrong updating movie!!");
                return StatusCode(500, ModelState);
            }
            return NoContent();



        }


    }
}

    