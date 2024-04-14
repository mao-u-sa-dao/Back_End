using WebApplication1.Repositories;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
namespace WebApplication1.Services
{
    public class MoviesListService
    {
        private readonly IRepository<MoviesList> _moviesListRepository;
        public MoviesListService(IRepository<MoviesList> moviesListRepository)
        {
            _moviesListRepository = moviesListRepository;
        }
        public async Task<List<MoviesList>> GetAllMoviesList()
        {
            return await _moviesListRepository.GetAllAsync();
        }
        public async Task<MoviesList> GetMoviesListById(int id)
        {
            var moviesList = await _moviesListRepository.GetByIdAsync(x => x.MovieListId == id);
            return moviesList;
        }
        public async Task<List<MoviesList>> GetMoviesListByCategory(int id)
        {
            var moviesList = await _moviesListRepository.GetListByCategoryAsync(x => x.CategoryId == id);
            return moviesList.ToList();
        }

        public async Task AddMoviesList(MoviesList moviesList)
        {
            await _moviesListRepository.AddAsync(moviesList);
        }
        public async Task UpdateMoviesList(int id, MoviesList moviesList)
        {
            var _moviesList = await _moviesListRepository.GetByIdAsync(x => x.MovieListId == id);
            _moviesList.MovieListId = moviesList.MovieListId;
            _moviesList.MovieListName = moviesList.MovieListName;
            _moviesList.Deseribe = moviesList.Deseribe;
            _moviesList.AvatarMovie = moviesList.AvatarMovie;
            _moviesList.Price = moviesList.Price;
            _moviesList.CategoryId = moviesList.CategoryId;
            _moviesList.AuthorId = moviesList.AuthorId;
            await _moviesListRepository.UpdateAsync(_moviesList);
        }
        public async Task DeleteMoviesList(int id)
        {
            var moviesList = await _moviesListRepository.GetByIdAsync(x => x.MovieListId == id);
            await _moviesListRepository.DeleteAsync(moviesList);
        }
    }
}
