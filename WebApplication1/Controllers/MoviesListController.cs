using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class MoviesListController : ControllerBase
    {
        private readonly MoviesListService _moviesListService;
        public MoviesListController(MoviesListService movieListService)
        {
            _moviesListService = movieListService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoviesList>>> GetAllMoviesList()
        {
            try
            {
                var moviesList = await _moviesListService.GetAllMoviesList();
                return Ok(moviesList);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MoviesList>> GetMoviesListById(int id)
        {
            var moviesList = await _moviesListService.GetMoviesListById(id);
            if (moviesList == null)
            {
                return NotFound();
            }
            return Ok(moviesList);
        }
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<MoviesList>> GetMoviesListByCategory(int categoryId)
        {
            var moviesList = await _moviesListService.GetMoviesListByCategory(categoryId);
            if (moviesList == null)
            {
                return NotFound();
            }
            return Ok(moviesList);
        } 
    }
}
