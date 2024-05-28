using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers.AdminController
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class AdminListMovieController : ControllerBase
    {
        private readonly MoviesListService _moviesListService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminListMovieController(MoviesListService movieListService, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _moviesListService = movieListService;
        }
        [HttpGet("page={pageNumber}")]
        public async Task<ActionResult<IEnumerable<MoviesList>>> GetAllMoviesList(int pageNumber)
        {
            try
            {
                var moviesList = await _moviesListService.GetAllMoviesListAdmin(pageNumber);
                return Ok(moviesList);
            }
            catch (Exception ex)
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

        [HttpPost]
        public async Task<ActionResult> AddMoviesList([FromForm] MoviesList moviesList, [FromForm] IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("no image");
            }
            // lấy tên
            var fileName = Path.GetFileName(file.FileName);
            //var fileName = $"{Path.GetFileNameWithoutExtension(originalFileName)}_{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            // lưu vào thư mục của proj
            // thư mục con(thư mục chứa ảnh)
            var subFolderName = "List-Movies-Avatar";
            var mainFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
            var filePath = Path.Combine(mainFolderPath, subFolderName, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            try
            {
                await _moviesListService.AddMoviesList(moviesList);
                return Ok(moviesList);
            }
            catch (Exception ex)
            {
                return BadRequest("loi ");
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateMoviesList([FromForm] MoviesList moviesList, [FromForm(Name = "file")] IFormFile? file)
        {
            try
            {
                // Nếu người dùng không gửi ảnh mới, sử dụng ảnh cũ đã có
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var subFolderName = "List-Movies-Avatar";
                    var mainFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    var filePath = Path.Combine(mainFolderPath, subFolderName, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    moviesList.AvatarMovie = fileName; // Cập nhật tên file ảnh mới vào MoviesList
                }

                // Gọi service để cập nhật thông tin moviesList
                await _moviesListService.UpdateMoviesList(moviesList);
                return Ok(moviesList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMoviesList(int id)
        {
            try
            {
                await _moviesListService.DeleteMoviesList(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
