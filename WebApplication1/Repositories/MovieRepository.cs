using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class MovieRepository<T> : IMovieRepository<T> where T : class
    {
        private readonly OnlineMoviesContext _context;
        public MovieRepository(OnlineMoviesContext context)
        {
            _context = context;
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public async Task<T> GetByWhereAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> predicate1)
        {
            return _context.Set<T>().Where(predicate).Where(predicate1).FirstOrDefault();
        }
        public async Task<List<T>> GetListByIdAsync(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>()
                .Where(expression)
                .ToList();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
