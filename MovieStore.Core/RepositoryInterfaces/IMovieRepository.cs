using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Core.Entities;

namespace MovieStore.Core.RepositoryInterfaces
{
   public  interface IMovieRepository: IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetTop25HighestRevenueMovies();

        Task<IEnumerable<Movie>> GetTop25RateMovies();

        Task<int> GetMoviesCount(string title);

        Task<IEnumerable<Movie>> GetMovieByGenreId(int id);



    }
}
