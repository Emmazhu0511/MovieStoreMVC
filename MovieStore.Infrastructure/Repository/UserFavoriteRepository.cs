using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Infrastructure.Repository
{
    public class UserFavoriteRepository:EfRepository<Favorite>, IUserFavoriteRepository
    {
        public UserFavoriteRepository(MovieStoreDBContext dbContext) : base(dbContext)
        {

        }

        public async Task DeleteFavorite(int userId, int movieId)
        {
            var favorite = await _dbContext.Favorites.Where(f => f.userId == userId && f.MovieId == movieId).FirstOrDefaultAsync();
            _dbContext.Favorites.Remove(favorite);
            await _dbContext.SaveChangesAsync();
          
        }

        public async Task<IEnumerable<Movie>> GetFavoriteMovies(int userId)
        {
            var movies = await _dbContext.Favorites.Include(m => m.Movie).Where(f => f.userId == userId)
                .Select(m => m.Movie).ToListAsync();
            return movies;
        }

        //public async Task<IEnumerable<Favorite>> GetFavoritsByUserId(int userId)
        //{
        //    var favorites = await _dbContext.Favorites.Where(f=> f.userId == userId).ToListAsync();
        //    return favorites;
        //}
    }
}
