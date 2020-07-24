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
    public class PurchaseRepository: EfRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieStoreDBContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Movie>> GetPurchases(int id)
        {
            var movies = await _dbContext.Purchases.Include(m => m.Movie).Where(p => p.UserId == id)
                .Select(m => m.Movie).ToListAsync();
            return movies;
        }


        //public async Task<Purchase> GetPurchaseById(int userId, int movieId)
        //{
        //    return await _dbContext.Purchases.FirstOrDefaultAsync(p=> p.MovieId == movieId && p.UserId == userId);
        //}
    }
}
