using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers.AdminController
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class AdminMovieUserOwnedController : ControllerBase
    {
        private readonly MoviesUserOwnedService _service;
        public AdminMovieUserOwnedController(MoviesUserOwnedService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoviesUserOwned>>> GetAll()
        {
            return await _service.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<MoviesUserOwned>>> GetListMoviesUserOwnedByid(int id)
        {
            var moviesUserOwned = await _service.GetListById(id);
            return Ok(moviesUserOwned);
        }
        [HttpPost]
        public async Task<ActionResult> AddMovieUserOwned(MoviesUserOwned moviesUserOwned)
        {
            try
            {
                await _service.AddMoviesUserOwned(moviesUserOwned);
                return Ok(moviesUserOwned);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovieUserOwned(int id, MoviesUserOwned moviesUserOwned)
        {
            try
            {
                await _service.UpdateMoviesUserOwned(id, moviesUserOwned);
                return Ok(moviesUserOwned);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleleMovieUserOwned(int id)
        {
            try
            {
                await _service.DeleteMoviesUserOwned(id);
                return Ok("Delete success");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
