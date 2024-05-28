using System.Linq.Expressions;

namespace WebApplication1.Repositories
{
    public interface IMoviesListRepository<T> where T : class
    {
        Task<List<T>> AdminGetAllAsync(Expression<Func<T, object>> include, Expression<Func<T, object>> include1, Expression<Func<T, T>> select);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetMovielistByIdAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> include, Expression<Func<T, object>> include1, Expression<Func<T, T>> select);
        Task<List<T>> GetListByCategoryAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
