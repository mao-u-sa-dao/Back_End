using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class MovieService
    {
        private readonly IMovieRepository<Movie> _movieRepository;
        public MovieService(IMovieRepository<Movie> movieRepository)
        {  
            _movieRepository = movieRepository; 
        }
        public async Task<List<Movie>> GetMovieByMovieListId(int id)
        {
            var movies = await _movieRepository.GetListByIdAsync(
                x => x.MovieListId == id
            );
            var movieSort = movies.OrderBy(x => x.MoviePart).ToList();
            return movies;
        }
        public async Task<Movie> GetMovieByPart(int movieListId, int movieId)
        {
            var movie = await _movieRepository.GetByWhereAsync(x => x.MovieListId == movieListId, p => p.MovieId == movieId);
            return movie;
        }

    }
}
