using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers.AdminController
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class AdminCategoryController : ControllerBase
    {
        private readonly MovieCategoryService _service;
        public AdminCategoryController(MovieCategoryService service)
        {
            _service = service;
        }
        [HttpGet("page={pageNumber}")]
        public async Task<ActionResult<IEnumerable<MovieCategory>>> GetAll(int pageNumber)
        {
            try
            {
                var category = await _service.GetAllCategoryAdmin(pageNumber);
                return Ok(category);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<MovieCategory>> Add(MovieCategory movieCategory)
        {
            try
            {
                await _service.AddCategory(movieCategory);
                return Ok(movieCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<MovieCategory>> Update(int id, MovieCategory movieCategory)
        {
            try
            {
                await _service.UpdateCategory(id, movieCategory);
                return Ok(movieCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
