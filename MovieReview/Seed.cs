using System;
using System.Collections.Generic;
using System.Linq;
using MovieReview.Data;
using MovieReview.Models;

namespace MovieReview
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            // only seed if empty
            if (!_context.Movies.Any())
            {
                var usa = new Country { Name = "USA" };
                var uk = new Country { Name = "UK" };

                var warner = new Studio { Name = "Warner Bros", Country = usa };
                var universal = new Studio { Name = "Universal Pictures", Country = uk };

                var action = new Genre { Name = "Action" };
                var drama = new Genre { Name = "Drama" };

                var reviewer1 = new Reviewer { FirstName = "Huss", LastName = "Dirani" };
                var reviewer2 = new Reviewer { FirstName = "Sara", LastName = "Anders" };

                var movie1 = new Movie
                {
                    // your table has Name, not Title
                    Name = "The Dark Knight",
                    // your table has ReleasedDate (with d)
                    ReleasedDate = new DateTime(2008, 7, 18),
                    MovieGenres = new List<MovieGenre>
                    {
                        new MovieGenre { Genre = action }
                    },
                    MovieStudios = new List<MovieStudio>
                    {
                        new MovieStudio { Studio = warner }
                    },
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            Title = "Great movie",
                            Text = "Best superhero movie.",
                            Rating = 5,
                            Reviewer = reviewer1
                        },
                        new Review
                        {
                            Title = "Nice",
                            Text = "Great acting.",
                            Rating = 4,
                            Reviewer = reviewer2
                        }
                    }
                };

                var movie2 = new Movie
                {
                    Name = "Inception",
                    ReleasedDate = new DateTime(2010, 7, 16),
                    MovieGenres = new List<MovieGenre>
                    {
                        new MovieGenre { Genre = drama }
                    },
                    MovieStudios = new List<MovieStudio>
                    {
                        new MovieStudio { Studio = universal }
                    },
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            Title = "Mind blowing",
                            Text = "Story was amazing.",
                            Rating = 5,
                            Reviewer = reviewer1
                        }
                    }
                };

                _context.Movies.AddRange(movie1, movie2);
                _context.SaveChanges();
            }
        }
    }
}
