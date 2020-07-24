using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository) {

            _reviewRepository = reviewRepository;
        }


        public async Task<Review> CreateReview(UserReviewRequestModel userReviewRequestModel)
        {

            if (await IsMovieReviewed(userReviewRequestModel.UserId, userReviewRequestModel.MovieId) == true) {

                throw new Exception("You have already reviewed this movie");
            }

            var review = new Review() { 
            
                MovieId = userReviewRequestModel.MovieId,
                UserId = userReviewRequestModel.UserId,
                Rating = userReviewRequestModel.Rating,
                ReviewText = userReviewRequestModel.ReviewText
            
            };

            var createReview = await _reviewRepository.AddAsync(review);

            return review;
        }

        public async Task<IEnumerable<Review>> GetAllReviews(int userId)
        {
            return await _reviewRepository.GetAllReviews(userId);

        }

        public async Task<Review> GetMovieReview(int userId, int movieId)
        {
            return await _reviewRepository.GetMovieReview(userId, movieId);
        }

        public async Task<bool> IsMovieReviewed(int userId, int movieId)
        {
            return await _reviewRepository.GetExistsAsync(r => r.UserId == userId && r.MovieId == movieId);
        }
    }
}
