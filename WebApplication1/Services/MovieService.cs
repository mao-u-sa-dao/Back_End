using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class MovieService
    {
        private readonly IRepository<Movie> _movieRepository;
        public MovieService(IRepository<Movie> movieRepository)
        {  
            _movieRepository = movieRepository; 
        }
        public async Task<List<Movie>> GetMovieByMovieListId(int id)
        {
            var movies = await _movieRepository.GetListByIdAsync(
                x => x.MovieListId == id,
                x => x.MovieListIdNavigation
            );
            return movies;
        }

    }
}
