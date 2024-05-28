using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication1.Services
{
    public class MovieAuthorService
    {
        private readonly IMovieAuthorRepository<MovieAuthor> _movieAuthorRepository;
        public MovieAuthorService(IMovieAuthorRepository<MovieAuthor> movieAuthorRepository)
        {
            _movieAuthorRepository = movieAuthorRepository;
        }

        public async Task<PagedResult<MovieAuthor>> GetAllAuthor(int pageNumber)
        {
            if (pageNumber > 0)
            {


                int pageSize = 10;
                var auThor = await _movieAuthorRepository.GetAllAsync();
                var totalItems = auThor.Count();
                var items = auThor.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                var totalPage = (int)Math.Ceiling((double)totalItems / pageSize);
                return new PagedResult<MovieAuthor>
                {
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalItems = totalItems,
                    TotalPages = totalPage,
                    Items = items
                };
            }
            var author =  await _movieAuthorRepository.GetAllAsync();
            return new PagedResult<MovieAuthor>
            {
                Items = author,
            };
        }

        public async Task<MovieAuthor> GetAuthorById(int id)
        {
            var auThor = await _movieAuthorRepository.GetByIdAsync(x => x.AuthorId == id);
            return auThor;
        }

        public async Task AddAuthor(MovieAuthor movie)
        {
            await _movieAuthorRepository.AddAsync(movie);
        }

        public async Task UpdateAuthor(int id, MovieAuthor movieAuthor)
        {
            var _movieAuthor = await _movieAuthorRepository.GetByIdAsync(x => x.AuthorId == id);
            _movieAuthor.AuthorId = movieAuthor.AuthorId;
            _movieAuthor.AuthorName = movieAuthor.AuthorName;
            await _movieAuthorRepository.UpdateAsync(_movieAuthor);
        }

        public async Task DeleleAuthor(int id)
        {
            var auThor = await _movieAuthorRepository.GetByIdAsync(x => x.AuthorId == id);
            await _movieAuthorRepository.DeleteAsync(auThor);
        }
    }
}
