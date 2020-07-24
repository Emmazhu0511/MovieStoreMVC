using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Response;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.MVC.Controllers
{
    public class MoviesController : Controller
    {

        private readonly IMovieService _movieService;
        private readonly IPurchaseService _purchaseService;
        private readonly IReviewService _reviewService;
        private readonly IUserFavoriteService _userFavoriteService;


        public MoviesController(IMovieService movieService, IPurchaseService purchaseService, IReviewService reviewService, IUserFavoriteService userFavoriteService) {
            _movieService = movieService;
            _purchaseService = purchaseService;
            _reviewService = reviewService;
            _userFavoriteService = userFavoriteService;
        }

        [HttpGet]
        public async Task<IActionResult> Genre(int id)
        {
            var movies = await _movieService.GetMovieByGenreId(id);
            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id) {
            var userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
            var movie = await _movieService.GetMovieById(id);
            var review = await _reviewService.GetMovieReview(userId, id);
            var PurchaseResponse = new UserPurchaseResponseModel() {
                movie = movie,
                IsMoviePurchased = await _purchaseService.IsMoviePurchased(userId, id),
                IsMovieReviewed = await _reviewService.IsMovieReviewed(userId, id),
                review = review,
                IsMovieFavorited = await _userFavoriteService.IsMovieFavorited(userId,id)
            };

            return View(PurchaseResponse);
        }

        public async Task<IActionResult> TopMovies() {
            var movies = await _movieService.GetTop25RateMovies();
            return View(movies);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movie movie) {


            var item = await _movieService.CreateMovie(movie);
            return View();
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }
    }
}
