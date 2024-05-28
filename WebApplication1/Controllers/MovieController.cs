using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;
        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            try
            {
                var movie = await _movieService.GetMovieByMovieListId(id);
                return Ok(movie);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{movieListId}/{movieId}")]
        public async Task<ActionResult<Movie>> GetMovieByPart(int movieListId, int movieId)
        {
            try
            {
                var movie = await _movieService.GetMovieByPart(movieListId, movieId);
                return Ok(movie);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
