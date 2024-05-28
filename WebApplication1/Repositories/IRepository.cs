using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApplication1.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetListByIdAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByWhereAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> predicate1);
        Task<List<T>> GetByIncludeAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> include, Expression<Func<T, T>> select);
        Task<List<T>> GetListByCategoryAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
