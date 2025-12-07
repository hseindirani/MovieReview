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
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {

            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries);
        }
        [HttpGet("{countryID}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryID)
        {
            if (!_countryRepository.CountryExist(countryID))
                return NotFound();
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryID));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(country);

        }
        [HttpGet("studios/{studioId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryOfAStudio(int studioId)
        {

            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByStudio(studioId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(country);


        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
        {
            if (countryCreate == null)
                return BadRequest(ModelState);
            var country = _countryRepository.GetCountries()
                        .Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.TrimEnd().ToUpper())
                         .FirstOrDefault();
            if (country != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var countryMap = _mapper.Map<Country>(countryCreate);
            if (!_countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wromg while saving ");
                return StatusCode(500, ModelState);

            }
            return Ok("Successfully created");
        }

            [HttpPut("{countryId}")]
            [ProducesResponseType(400)]
            [ProducesResponseType(204)]
            [ProducesResponseType(404)]

            public IActionResult UpdateCountrt(int countryId, [FromBody] CountryDto updatedcountry)
            {
                if (updatedcountry == null)
                    return BadRequest(ModelState);

                if (countryId != updatedcountry.Id)
                    return BadRequest(ModelState);

                if (!_countryRepository.CountryExist(countryId))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest();
                var countryMap = _mapper.Map<Country>(updatedcountry);

                if (!_countryRepository.UpdateCountry(countryMap))
                {

                    ModelState.AddModelError("", "something went wrong updating country!!");
                    return StatusCode(500, ModelState);
                }
                return NoContent();



            }


        }
    }