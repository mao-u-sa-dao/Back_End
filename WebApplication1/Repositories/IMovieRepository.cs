using System.Linq.Expressions;

namespace WebApplication1.Repositories
{
    public interface IMovieRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetListByIdAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByWhereAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> predicate1);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
