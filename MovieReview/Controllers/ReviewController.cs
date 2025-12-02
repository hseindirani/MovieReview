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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }
        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();
            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(review);
        }
        [HttpGet("movies/{movId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfAMovie(int movId)
        {

            var review = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfAMovie(movId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(review);



        }
        //[HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]

        //public IActionResult CreateGenre([FromBody] GenreDTO genreCreate)
        //{
        //    if (genreCreate == null)
        //        return BadRequest(ModelState);
        //    var genre = _genreRepository.GetGenres()
        //                .Where(c => c.Name.Trim().ToUpper() == genreCreate.Name.TrimEnd().ToUpper())
        //                 .FirstOrDefault();
        //    if (genre != null)
        //    {
        //        ModelState.AddModelError("", "Genre alread exists");
        //        return StatusCode(422, ModelState);
        //    }
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var genreMap = _mapper.Map<Genre>(genreCreate);
        //    if (!_genreRepository.CreateGenre(genreMap))
        //    {
        //        ModelState.AddModelError("", "Something went wromg while saving ");
        //        return StatusCode(500, ModelState);

        //    }
        //    return Ok("Successfully created");


        //}
    }
    }
