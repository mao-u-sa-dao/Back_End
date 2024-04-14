using System.Linq.Expressions;

namespace WebApplication1.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetListByIdAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> includ);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetListByCategoryAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
