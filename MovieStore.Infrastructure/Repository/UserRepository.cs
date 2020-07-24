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
    public class UserRepository : EfRepository<User>, IUserRepository

    {
        public UserRepository(MovieStoreDBContext dbContext) :base(dbContext) { 
        
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
