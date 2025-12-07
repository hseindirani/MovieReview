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
    public class StudioController : ControllerBase
    {
        private readonly IStudioRepository _studioRepository;
        private readonly IMapper _mapper;

        public StudioController(IStudioRepository studioRepository,IMapper mapper)
        {
            _studioRepository = studioRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Studio>))]
        public IActionResult GetCountries()
        {

            var studios = _mapper.Map<List<StudioDto>>(_studioRepository.GetStudios());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(studios);
        }
        [HttpGet("{studioId}")]
        [ProducesResponseType(200, Type = typeof(Studio))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int studioId)
        {
            if (!_studioRepository.StudioExists(studioId))
                return NotFound();
            var studio = _mapper.Map<StudioDto>(_studioRepository.GetStudio(studioId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(studio);

        }
        [HttpGet("movies/{studioId}")]
        [ProducesResponseType(200, Type = typeof(Studio))]
        [ProducesResponseType(400)]
        public IActionResult GetStudioOfAMovie(int movId)
        {

            var studio = _mapper.Map<StudioDto>(_studioRepository.GetStudioOfAMovie(movId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(studio);


        }
        [HttpGet("{studioId}/movie")]
        [ProducesResponseType(200, Type = typeof(Studio))]
        [ProducesResponseType(400)]
        public IActionResult GetMovieByStudio(int studioId)
        {
            if (!_studioRepository.StudioExists(studioId))
            {
                return NotFound();
            }

            var movie =_mapper.Map<List<MovieDto>>(_studioRepository.GetMovieByStudio(studioId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(movie);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateStudio([FromBody] CreateStudioDto studioCreate)
        {
            if (studioCreate == null)
                return BadRequest(ModelState);
            var studio = _studioRepository.GetStudios()
                        .Where(c => c.Name.Trim().ToUpper() == studioCreate.Name.TrimEnd().ToUpper())
                         .FirstOrDefault();
            if (studio != null)
            {
                ModelState.AddModelError("", "Studio already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var studioMap = _mapper.Map<Studio>(studioCreate);
            studioMap.CountryId = studioCreate.CountryId;
            if (!_studioRepository.CreateStudio(studioMap))
            {
                ModelState.AddModelError("", "Something went wromg while saving ");
                return StatusCode(500, ModelState);

            }
            return Ok("Successfully created");


        }
        [HttpPut("{studioId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateStudio(int studioId, [FromBody] StudioUpdateDto updatedStudio)
        {
            if (updatedStudio == null)
                return BadRequest(ModelState);

            if (studioId != updatedStudio.Id)
                return BadRequest(ModelState);

            if (!_studioRepository.StudioExists(studioId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();
            var studioMap = _mapper.Map<Studio>(updatedStudio);

            if (!_studioRepository.UpdateStudio(studioMap))
            {

                ModelState.AddModelError("", "something went wrong updating studio!!");
                return StatusCode(500, ModelState);
            }
            return NoContent();



        }


    }
}
