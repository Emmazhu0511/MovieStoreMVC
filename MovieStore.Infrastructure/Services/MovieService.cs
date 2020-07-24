using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Infrastructure.Services
{
    public class MovieService: IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository) {

            _movieRepository = movieRepository;
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            return await _movieRepository.AddAsync(movie);
        }

        public async Task<Movie> GetMovieById(int d)
        {
            return await _movieRepository.GetByIdAsync(d);
        }

        public async Task<int> GetMoviesCount(string title)
        {
            return await _movieRepository.GetMoviesCount(title);
        }

        public async Task<IEnumerable<Movie>> GetTop25HighestRevenueMovies()
        {
            return await _movieRepository.GetTop25HighestRevenueMovies();
        }

        public async Task<IEnumerable<Movie>> GetTop25RateMovies()
        {
            return await _movieRepository.GetTop25RateMovies();
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            return await _movieRepository.UpdateAsync(movie);
        }

        public async Task<IEnumerable<Movie>> GetMovieByGenreId(int id)
        {
            return await _movieRepository.GetMovieByGenreId(id);
        }
    }
}
