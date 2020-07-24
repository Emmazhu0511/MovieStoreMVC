using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Infrastructure.Data;

namespace MovieStore.Infrastructure.Repository
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly MovieStoreDBContext _dbContext;

        public EfRepository(MovieStoreDBContext dbContext) {
            _dbContext = dbContext;

        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null) {
                return await _dbContext.Set<T>().Where(filter).CountAsync();
            }
            return await _dbContext.Set<T>().CountAsync();
           
        }

        public async Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null) {
                return await _dbContext.Set<T>().Where(filter).AnyAsync();
            }
            return await _dbContext.Set<T>().AnyAsync();
        }

        public async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();

        }

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
