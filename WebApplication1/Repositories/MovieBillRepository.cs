using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class MovieBillRepository<T> : IMovieBillRepository<T> where T : class
    {
        private readonly OnlineMoviesContext _context;
        public MovieBillRepository(OnlineMoviesContext context)
        {
            _context = context;
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, object>> include, Expression<Func<T, object>> include1, Expression<Func<T, T>> select)
        {
            return await _context.Set<T>().Include(include).Include(include1).Select(select).ToListAsync();
        }
        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }
        public async Task<List<T>> GetListByIdAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
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
