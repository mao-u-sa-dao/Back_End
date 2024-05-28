using System.Linq.Expressions;

namespace WebApplication1.Repositories
{
    public interface IAccountRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetUserAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByNameAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByEmailAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
