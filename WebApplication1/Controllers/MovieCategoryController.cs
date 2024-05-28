using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class MovieCategoryController : ControllerBase
    {
        private readonly MovieCategoryService _service;
        public MovieCategoryController(MovieCategoryService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieCategory>>> GetAll()
        {
            return await _service.GetAllCategory();
        }

    }
}
