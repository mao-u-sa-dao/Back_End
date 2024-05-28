using System.Linq.Expressions;

namespace WebApplication1.Repositories
{
    public interface ICommentRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetListByIdAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetByIncludeAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> include, Expression<Func<T, T>> select);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
