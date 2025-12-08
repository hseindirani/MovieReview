using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieReview.Dto;
using MovieReview.Interfaces;
using MovieReview.Models;
using MovieReview.Repositories;

namespace MovieReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreController(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        public IActionResult GetMovies()
        {
            //var movies = _movieRepository.GetMovies();
            var genres = _mapper.Map<List<GenreDTO>>(_genreRepository.GetGenres());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(genres);
        }
        [HttpGet("{genreId}")]
        [ProducesResponseType(200, Type = typeof(Genre))]
        [ProducesResponseType(400)]
        public IActionResult GetGenreById(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
                return NotFound();
            var genre = _mapper.Map<GenreDTO>(_genreRepository.GetGenreById(genreId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(genre);
        }
        [HttpGet("movie/{genreId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        [ProducesResponseType(400)]
        public IActionResult GetMoviesByGenre(int genreId)
        {

            var movies = _mapper.Map<List<MovieDto>>(_genreRepository.GetMoviesByGenre(genreId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(movies);


        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateGenre([FromBody] GenreDTO genreCreate)
        {
            if (genreCreate == null)
                return BadRequest(ModelState);
            var genre = _genreRepository.GetGenres()
                        .Where(c => c.Name.Trim().ToUpper() == genreCreate.Name.TrimEnd().ToUpper())
                         .FirstOrDefault();
            if (genre != null)
            {
                ModelState.AddModelError("", "Genre alread exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var genreMap = _mapper.Map<Genre>(genreCreate);
            if (!_genreRepository.CreateGenre(genreMap))
            {
                ModelState.AddModelError("", "Something went wromg while saving ");
                return StatusCode(500, ModelState);

            }
            return Ok("Successfully created");


        }
        [HttpPut("{genreId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGenre(int genreId, [FromBody] GenreDTO updatedGenre)
        {
            if (updatedGenre == null)
                return BadRequest(ModelState);

            if (genreId != updatedGenre.Id)
                return BadRequest(ModelState);

            if (!_genreRepository.GenreExists(genreId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();
            var genreMap = _mapper.Map<Genre>(updatedGenre);

            if (!_genreRepository.UpdateGenre(genreMap))
            {

                ModelState.AddModelError("", "something went wrong updating genre!!");
                return StatusCode(500, ModelState);
            }
            return NoContent();



        }
        [HttpDelete("{genreId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(204)]

        public IActionResult DeleteGenre(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
            {
                return NotFound();
            }
            var GenreToDeelete = _genreRepository.GetGenreById(genreId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_genreRepository.DeleteGenre(GenreToDeelete))
            {
                ModelState.AddModelError("", "something went wrong when deleting genre!!");
            }
            return NoContent();



        }
    }
}