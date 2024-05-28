﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class CommentRepository<T> : ICommentRepository<T> where T : class
    {
        private readonly OnlineMoviesContext _context;
        public CommentRepository(OnlineMoviesContext context)
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
        public async Task<List<T>> GetListByIdAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }
        public async Task<List<T>> GetByIncludeAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> include, Expression<Func<T, T>> select)
        {
            return await _context.Set<T>()
                                 .Where(predicate)
                                 .Include(include)
                                 .Select(select) // Sử dụng Select với kiểu cụ thể là T
                                 .ToListAsync();
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
