using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class MovieAuthorService
    {
        private readonly IRepository<MovieAuthor> _movieAuthorRepository;
        public MovieAuthorService(IRepository<MovieAuthor> movieAuthorRepository)
        {
            _movieAuthorRepository = movieAuthorRepository;
        }

        public async Task<List<MovieAuthor>> GetAllAuthor()
        {
            return await _movieAuthorRepository.GetAllAsync();
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
