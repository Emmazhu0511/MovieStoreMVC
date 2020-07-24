using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Infrastructure.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMovieService _movieService;

        public PurchaseService(IPurchaseRepository purchaseRepository, IMovieService movieService) {

            _purchaseRepository = purchaseRepository;
            _movieService = movieService;
        }

        public async Task<IEnumerable<Movie>> GetPurchases(int id)
        {
            return await _purchaseRepository.GetPurchases(id);
        }

        public async Task<bool> IsMoviePurchased(int userId, int movieId)
        {
        
            return await _purchaseRepository.GetExistsAsync(p => p.UserId == userId && p.MovieId == movieId);
        }

        public async Task<Purchase> PlaceOrder(UserPurchaseRequestModel userPurchaseRequestModel)
        {
            var movie = await _movieService.GetMovieById(userPurchaseRequestModel.MovieId);

            if (await IsMoviePurchased(userPurchaseRequestModel.UserId, userPurchaseRequestModel.MovieId) == true) {

                throw new Exception("You already bought this movie!");
                
            }
            if (await IsMoviePurchased(userPurchaseRequestModel.UserId, userPurchaseRequestModel.MovieId) == false)
            {
                

                var purchase = new Purchase()
                {

                    UserId = userPurchaseRequestModel.UserId,
                    PurchaseNumber = userPurchaseRequestModel.PurchaseNumber,
                    TotalPrice = movie.Price.Value,
                    PurchaseDateTime = userPurchaseRequestModel.PurchaseDateTime,
                    MovieId = userPurchaseRequestModel.MovieId
                };

                var createPurchase = await _purchaseRepository.AddAsync(purchase);


                //var response = new UserPurchaseResponseModel()
                //{
                //    Id = createPurchase.Id,
                //    UserId = createPurchase.UserId,
                //    PurchaseNumber = createPurchase.PurchaseNumber,
                //    TotalPrice = createPurchase.TotalPrice,
                //    PurchaseDateTime = createPurchase.PurchaseDateTime,
                //    MovieId = createPurchase.MovieId,
                //    movie = movie,
                //    IsMoviePurchased = true
                    

                //};
                return createPurchase;
            }
            return null;
        }


    }
}
