using System.Linq.Expressions;

namespace WebApplication1.Repositories
{
    public interface IMovieBillRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, object>> include, Expression<Func<T, object>> include1, Expression<Func<T, T>> select);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetListByIdAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
