using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.Infrastructure.Services;
using MovieStore.MVC.Models;

namespace MovieStore.MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMovieService _movieService;

        public HomeController(IMovieService movieService) {

            _movieService = movieService;

        }
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetTop25HighestRevenueMovies();
            return View(movies);
        }

      
    }
}
