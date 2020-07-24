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
    public class ReviewRepository : EfRepository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieStoreDBContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Review>> GetAllReviews(int userId)
        {
            var reviews = await _dbContext.Reviews.Where(r => r.UserId == userId).ToListAsync();
            return reviews;
        }

        public async Task<Review> GetMovieReview(int userId, int movieId)
        {
            var review = await _dbContext.Reviews.Where(r => r.UserId == userId && r.MovieId == movieId).FirstOrDefaultAsync();
            return review;
        }
    }
}
