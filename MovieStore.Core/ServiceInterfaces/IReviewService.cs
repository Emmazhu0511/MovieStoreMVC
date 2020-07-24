using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IReviewService
    {

        Task<bool> IsMovieReviewed(int userId, int movieId);
        Task<Review> CreateReview(UserReviewRequestModel userReviewRequestModel);

        Task<IEnumerable<Review>> GetAllReviews(int userId);

        Task<Review> GetMovieReview(int userId, int movieId);
    }
}
