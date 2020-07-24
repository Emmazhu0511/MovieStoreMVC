using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IUserFavoriteService
    {
        Task<bool> IsMovieFavorited(int userId, int movieId);

        Task<Favorite> AddFavorite(UserFavoriteRequestModel userFavoriteRequestModel);


        Task<IEnumerable<Movie>> GetFavoriteMovies(int userId);

        Task DeleteFavorite(int userId, int movieId);
    }
}
