using MovieStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.RepositoryInterfaces
{
    public interface IUserFavoriteRepository: IAsyncRepository<Favorite>
    {
        Task<IEnumerable<Movie>> GetFavoriteMovies(int userId);

        Task DeleteFavorite(int userId, int movieId);
    }
}
