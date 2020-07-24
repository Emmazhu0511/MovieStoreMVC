
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
    public class GenreRepository : EfRepository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieStoreDBContext dbContext) :base(dbContext){ 
        
        }

        //select all genres
        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            var genres = await  _dbContext.Genres.Select(x => x).ToListAsync();
            return genres;

        }
    }
}
