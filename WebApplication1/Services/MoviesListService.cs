using WebApplication1.Repositories;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
namespace WebApplication1.Services
{
    public class MoviesListService
    {       
        private readonly IMoviesListRepository<MoviesList> _moviesListRepository;
        public MoviesListService(IMoviesListRepository<MoviesList> moviesListRepository)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
        {
            _moviesListRepository = moviesListRepository;
        }
        public async Task<PagedResult<MoviesList>> GetAllMoviesListAdmin(int pageNumber)
        {
            int pageSize = 10;
            var movieList = await _moviesListRepository.AdminGetAllAsync(
                include: x => x.Category,
                include1: x => x.Author,
                select: movieList => new MoviesList
                {
                    MovieListId = movieList.MovieListId,
                    MovieListName = movieList.MovieListName,
                    Deseribe = movieList.Deseribe,
                    AvatarMovie = movieList.AvatarMovie,
                    Price = movieList.Price,
                    CategoryId = movieList.CategoryId,
                    AuthorId = movieList.AuthorId,
                    Category = new MovieCategory
                    {
                        CategoryId = movieList.Category.CategoryId,
                        CategoryName = movieList.Category.CategoryName,

                    },
                    Author = new MovieAuthor
                    {
                        AuthorId = movieList.Author.AuthorId,
                        AuthorName = movieList.Author.AuthorName,
                    }
                }
                ); 
            var totalItems = movieList.Count();
            var items = movieList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var totalPage = (int)Math.Ceiling((double)totalItems / pageSize);
            return new PagedResult<MoviesList>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPage,
                Items = items
            };
        }
        public async Task<List<MoviesList>> GetAllMoviesList()
        {
            return await _moviesListRepository.GetAllAsync();
        }
        public async Task<MoviesList> GetMoviesListById(int id)
        {
            var query = await _moviesListRepository.GetMovielistByIdAsync(
                x => x.MovieListId == id,
                x => x.Author,
                x => x.Category,
                moviesList => new MoviesList
                {
                    MovieListId = moviesList.MovieListId,
                    MovieListName = moviesList.MovieListName,
                    Deseribe = moviesList.Deseribe,
                    AvatarMovie = moviesList.AvatarMovie,
                    Price = moviesList.Price,
                    Author = new MovieAuthor
                    {
                        AuthorId = moviesList.Author.AuthorId,
                        AuthorName = moviesList.Author.AuthorName,
                    },
                    Category = new MovieCategory
                    {
                        CategoryId = moviesList.Category.CategoryId,
                        CategoryName = moviesList.Category.CategoryName,
                    }
                });
            return query;
        }
        public async Task<List<MoviesList>> GetMoviesListByCategory(int id)
        {
            var moviesList = await _moviesListRepository.GetListByCategoryAsync(x => x.CategoryId == id);
            return moviesList;
        }

        public async Task AddMoviesList(MoviesList moviesList)
        {
            await _moviesListRepository.AddAsync(moviesList);
        }
        public async Task UpdateMoviesList( MoviesList moviesList)
        {
            var _moviesList = await _moviesListRepository.GetByIdAsync(x => x.MovieListId == moviesList.MovieListId);
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
