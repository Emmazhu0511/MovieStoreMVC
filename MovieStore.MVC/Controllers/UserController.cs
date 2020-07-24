using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Models.Request;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.MVC.Controllers
{
    public class UserController : Controller
    {
        private IPurchaseService _purchaseService;
        private IReviewService _reviewService;
        private IUserFavoriteService _userFavoriteService;
      

        public UserController(IPurchaseService purchaseService, IReviewService reviewService, IUserFavoriteService userFavoriteService) {

            _purchaseService = purchaseService;
            _reviewService = reviewService;
            _userFavoriteService = userFavoriteService;
          
        }


        [HttpPost]
        public async Task<IActionResult> Purchase(UserPurchaseRequestModel userPurchaseRequestModel)
        {
            userPurchaseRequestModel.UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);

            var createPurchase = await _purchaseService.PlaceOrder(userPurchaseRequestModel);
            return LocalRedirect("~/Movies/Details/" + userPurchaseRequestModel.MovieId);


        }

        [HttpGet]
        public async Task<IActionResult> Purchases() {
            int userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);

            var movies = await _purchaseService.GetPurchases(userId);

            return View(movies);
        }

        [HttpPost]
        public async Task<IActionResult> Review(UserReviewRequestModel userReviewRequestModel) {
            userReviewRequestModel.UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            var createReview = await _reviewService.CreateReview(userReviewRequestModel);
            return LocalRedirect("~/Movies/Details/" + userReviewRequestModel.MovieId);

        }


        [HttpGet]
        public async Task<IActionResult> Reviews() {

            int userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            var movies = await _reviewService.GetAllReviews(userId);
            return View(movies);

        }

        [HttpPost]
        public async Task<IActionResult> Favorite(UserFavoriteRequestModel userFavoriteRequestModel) {
            userFavoriteRequestModel.UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            var favorite = await _userFavoriteService.AddFavorite(userFavoriteRequestModel);

            return LocalRedirect("~/Movies/Details/" + userFavoriteRequestModel.MovieId);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFavorite(int userId, int movieId) {
             userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
             await _userFavoriteService.DeleteFavorite(userId, movieId);

            return LocalRedirect("~/Movies/Details/" + movieId);

        }

        [HttpGet]
        public async Task<IActionResult> FavoriteMovies(int userId) {
            userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            var movies = await _userFavoriteService.GetFavoriteMovies(userId);

            return View(movies);

        }
    }
}
