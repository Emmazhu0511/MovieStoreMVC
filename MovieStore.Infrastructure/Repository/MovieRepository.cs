using MovieStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Infrastructure.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MovieStore.Infrastructure.Repository
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieStoreDBContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Movie>> GetMovieByGenreId(int id)
        {
            var movies = await _dbContext.MovieGenres.Include(g => g.Genre).Include(m => m.Movie).Where(g => g.GenreId == id)
                .Select(m => m.Movie).ToListAsync();

            return movies;

        }

        public async Task<int> GetMoviesCount(string title)
        {
            var count = await _dbContext.Movies.CountAsync(m => m.Title.Contains(title));
            return count;

        }

        public async Task<IEnumerable<Movie>> GetTop25HighestRevenueMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(25).ToListAsync();
            return movies;

        }

        public async Task<IEnumerable<Movie>> GetTop25RateMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(r => r.Reviews.Average(r => r.Rating)).Take(25).ToListAsync();

            //var movies = await _dbContext.Reviews.Include(m => m.Movie).GroupBy(r => r.MovieId).OrderByDescending(g => g.Average(r => r.Rating)).Select(m => new Movie
            //{
            //    Id = m.Key,
            //    Rating = m.Average(x => x.Rating),
                


            //}).Take(25).ToListAsync();

            return movies;
        }

        public override async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _dbContext.Movies.Include(mc => mc.MovieCasts).ThenInclude(c => c.Cast).
                Include(m => m.MovieGenres).ThenInclude(m => m.Genre).Include(t=>t.Trailers).FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null) return null;

            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).AverageAsync(r => r.Rating);

            if (movieRating > 0) movie.Rating = movieRating;

            return movie;
        }
    }
}
