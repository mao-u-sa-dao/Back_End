using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers.AdminController
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class AdminBillMovieController : ControllerBase
    {
        private readonly MovieBillService _movieBillService;
        public AdminBillMovieController(MovieBillService movieBillService)
        {
            _movieBillService = movieBillService;
        }
        [HttpGet("page={pageNumber}")]
        public async Task<ActionResult<IEnumerable<MoviesBill>>> GetAllBill(int pageNumber)
        {
            var bill = await _movieBillService.GetAllBIllAsync(pageNumber);
            return Ok(bill);
        }
        [HttpGet("{idUser}")]
        public async Task<ActionResult<IEnumerable<MoviesBill>>> GetListBillById(int idUser)
        {
            var bill = await _movieBillService.GetListBillByIdAccount(idUser);
            return Ok(bill);
        }
        [HttpPost]
        public async Task<ActionResult> AddBill(MoviesBill moviesBill)
        {
            try
            {
                await _movieBillService.AddMovieBill(moviesBill);
                return Ok(moviesBill);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
