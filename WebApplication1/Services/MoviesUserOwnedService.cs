using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class MoviesUserOwnedService
    {
        private readonly IMoviesUserOwnedRepository<MoviesUserOwned> _repository;
        public MoviesUserOwnedService(IMoviesUserOwnedRepository<MoviesUserOwned> repository)
        {
            _repository = repository;
        }
        public async Task<List<MoviesUserOwned>> GetAll()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<List<MoviesUserOwned>> GetListById(int id)
        {
            var query = await _repository.GetListByIdAsync(
                x => x.AccountId == id,
                x => x.MovieList,
                movieOwned => new MoviesUserOwned
                {
                   MoviesUserOwnedId = movieOwned.MoviesUserOwnedId,
                   MovieListId = movieOwned.MovieListId,
                   Price = movieOwned.Price,
                   MovieList = new MoviesList
                   {
                       MovieListId = movieOwned.MovieList.MovieListId,
                       MovieListName = movieOwned.MovieList.MovieListName,
                       AvatarMovie = movieOwned.MovieList.AvatarMovie,
                       Deseribe = movieOwned.MovieList.Deseribe,
                   }

                });
            return query; 
        }
        public async Task AddMoviesUserOwned(MoviesUserOwned moviesUserOwned)
        {
            await _repository.AddAsync(moviesUserOwned);
        }
        public async Task UpdateMoviesUserOwned(int id, MoviesUserOwned moviesUserOwned)
        {
            var _moviesUserOwned = await _repository.GetByIdAsync(x => x.MoviesUserOwnedId == id);
            if (_moviesUserOwned != null)
            {
                _moviesUserOwned.MovieListId = moviesUserOwned.MovieListId;
                _moviesUserOwned.AccountId = moviesUserOwned.AccountId;
                _moviesUserOwned.Price = moviesUserOwned.Price;
                await _repository.UpdateAsync(_moviesUserOwned);
            }
        }
        public async Task DeleteMoviesUserOwned(int id)
        {
            var moviesUserOwned = await _repository.GetByIdAsync(x=>x.MoviesUserOwnedId ==id);
            await _repository.DeleteAsync(moviesUserOwned);
        }

    }
}
