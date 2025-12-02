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
            CreateMap<MovieDto, Movie>();
            CreateMap<Genre,GenreDTO>();
            CreateMap<GenreDTO, Genre>();
            CreateMap<Country,CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Studio,StudioDto>();
            CreateMap<CreateStudioDto, Studio>();
            CreateMap<Review,ReviewDto>();
            CreateMap<ReviewDto,Review>();
            CreateMap<Reviewer,ReviewerDto>();
        }
    }
}
