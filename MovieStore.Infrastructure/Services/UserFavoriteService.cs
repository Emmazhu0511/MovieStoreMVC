using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Infrastructure.Services
{
    public class UserFavoriteService : IUserFavoriteService
    {

        private readonly IUserFavoriteRepository _userFavoriteRepository;

        public UserFavoriteService(IUserFavoriteRepository userFavoriteRepository) {
            _userFavoriteRepository = userFavoriteRepository;
        }

        //public async Task<IEnumerable<Favorite>> GetFavoritsByUserId(int userId) {

        //    return await _userFavoriteRepository.GetFavoritsByUserId(userId);
        //}


        public async Task<Favorite> AddFavorite(UserFavoriteRequestModel userFavoriteRequestModel)
        {
            if (await IsMovieFavorited(userFavoriteRequestModel.UserId,userFavoriteRequestModel.MovieId) == true)
            {

                throw new Exception("You added this movie as favorite");
            }

           Favorite favorite = new Favorite()
           {
            userId = userFavoriteRequestModel.UserId,
            MovieId = userFavoriteRequestModel.MovieId
            };

           var addFavorite = await _userFavoriteRepository.AddAsync(favorite);

        

             return addFavorite;
              
            }



        public async Task<bool> IsMovieFavorited(int userId, int movieId)
        {
            return await _userFavoriteRepository.GetExistsAsync(f => f.userId == userId && f.MovieId == movieId);
        }


        public async Task DeleteFavorite(int userId, int movieId) {

            await _userFavoriteRepository.DeleteFavorite(userId, movieId);
        }

        public async Task<IEnumerable<Movie>> GetFavoriteMovies(int userId)
        {
            return await _userFavoriteRepository.GetFavoriteMovies(userId);
        }
    }



}

