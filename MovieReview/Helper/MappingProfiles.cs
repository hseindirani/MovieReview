using AutoMapper;
using MovieReview.Dto;
using MovieReview.Models;

namespace MovieReview.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Movie, MovieDto>();
            CreateMap<Genre,GenreDTO>();
            CreateMap<Country,CountryDto>();
        }
    }
}
