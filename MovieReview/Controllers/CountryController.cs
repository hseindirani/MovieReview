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

            var country = _mapper.Map< CountryDto> (_countryRepository.GetCountryByStudio(studioId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(country);


        }


    }
}
